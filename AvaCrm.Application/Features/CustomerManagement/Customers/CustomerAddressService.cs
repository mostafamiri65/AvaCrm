using AvaCrm.Application.DTOs.CustomerManagement.CustomerAddresses;
using AvaCrm.Application.Pagination;
using AvaCrm.Application.Services;
using AvaCrm.Domain.Contracts.CustomerManagement;
using AvaCrm.Domain.Entities.CustomerManagement;
using Microsoft.EntityFrameworkCore;

namespace AvaCrm.Application.Features.CustomerManagement.Customers
{
	public class CustomerAddressService : BaseService, ICustomerAddressService
	{
		private readonly ICustomerAddressRepository _customerAddressRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly ICityRepository _cityRepository;
		public CustomerAddressService(
			IMapper mapper,
			ICustomerAddressRepository customerAddressRepository,
			ICustomerRepository customerRepository, ICityRepository cityRepository) : base(mapper)
		{
			_customerAddressRepository = customerAddressRepository;
			_customerRepository = customerRepository;
			_cityRepository = cityRepository;
		}

		public async Task<GlobalResponse<CustomerAddressListDto>> GetById(long id, CancellationToken cancellationToken = default)
		{
			try
			{
				var address = await _customerAddressRepository.GetById(id, cancellationToken);
				if (address == null)
					return new GlobalResponse<CustomerAddressListDto> { StatusCode = 404, Message = "آدرس یافت نشد" };

				var result = _mapper.Map<CustomerAddressListDto>(address);
				return new GlobalResponse<CustomerAddressListDto> { StatusCode = 200, Data = result, Message = "عملیات موفق" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<CustomerAddressListDto> { StatusCode = 500, Message = $"خطا در دریافت اطلاعات: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<PaginatedResult<CustomerAddressListDto>>> GetByCustomerId(long customerId, PaginationRequest request, CancellationToken cancellationToken = default)
		{
			try
            {
                var query = await _customerAddressRepository.GetByCustomerId(customerId,cancellationToken);

				// Apply search filter
				if (!string.IsNullOrWhiteSpace(request.SearchTerm))
				{
					query = query.Where(ca => ca.Street.Contains(request.SearchTerm) ||
											 ca.AdditionalInfo.Contains(request.SearchTerm)).ToList();
				}

				// Apply sorting
				query = request.SortDirection == "desc" ?
					query.OrderByDescending(ca => ca.CreationDate).ToList() :
					query.OrderBy(ca => ca.CreationDate).ToList();

				var totalCount = query.Count;

				var addresses =  query
					.Skip((request.PageNumber - 1) * request.PageSize)
					.Take(request.PageSize)
					.ToList();
                var list = _mapper.Map<List<CustomerAddressListDto>>(addresses);
               

				var result = new PaginatedResult<CustomerAddressListDto>
				{
					Items = _mapper.Map<List<CustomerAddressListDto>>(addresses),
					TotalCount = totalCount,
					PageNumber = request.PageNumber,
					PageSize = request.PageSize
				};

				return new GlobalResponse<PaginatedResult<CustomerAddressListDto>>
				{
					StatusCode = 200,
					Data = result,
					Message = "عملیات موفق"
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<PaginatedResult<CustomerAddressListDto>>
				{
					StatusCode = 500,
					Message = $"خطا در دریافت لیست آدرس‌ها: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<CustomerAddressListDto>> Create(CustomerAddressCreateDto createDto, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				// Verify customer exists
				var customer = await _customerRepository.GetById(createDto.CustomerId, cancellationToken);
				if (customer == null)
					return new GlobalResponse<CustomerAddressListDto> { StatusCode = 404, Message = "مشتری یافت نشد" };
				if (createDto.City != null && !await _cityRepository.IsExist(createDto.City, createDto.ProvinceId, 0))
				{
					var city = await _cityRepository.Create(new City()
					{
						Name = createDto.City,
						ProvinceId = createDto.ProvinceId
					});
					createDto.CityId = city.Id;
				}
				else
				{
					if (createDto.City != null)
					{
						var city = await _cityRepository.GetByName(createDto.City, createDto.ProvinceId);
						if (city != null)
						{
							createDto.CityId = city.Id;
						}
					}
				}

				var address = new CustomerAddress()
                {
					CustomerId = createDto.CustomerId,
					CountryId = createDto.CountryId,
                    ProvinceId = createDto.ProvinceId,
					CreatedBy = userId,
                    AdditionalInfo = createDto.AdditionalInfo,
                    CityId = createDto.CityId,
                    Street = createDto.Street
                };
				var created = await _customerAddressRepository.Create(address, userId);
				var result = _mapper.Map<CustomerAddressListDto>(created);

				return new GlobalResponse<CustomerAddressListDto> { StatusCode = 201, Data = result, Message = "آدرس با موفقیت ایجاد شد" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<CustomerAddressListDto> { StatusCode = 500, Message = $"خطا در ایجاد آدرس: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<CustomerAddressListDto>> Update(CustomerAddressUpdateDto updateDto, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				var existing = await _customerAddressRepository.GetById(updateDto.Id, cancellationToken);
				if (existing == null)
					return new GlobalResponse<CustomerAddressListDto> { StatusCode = 404, Message = "آدرس یافت نشد" };

				
                if (updateDto.City != null && await _cityRepository.IsExist(updateDto.City, updateDto.ProvinceId, 0))
                {
                    var city = await _cityRepository.Create(new City()
                    {
                        Name = updateDto.City,
                        ProvinceId = updateDto.ProvinceId
                    });
                    existing.CityId = city.Id;
                }
                else
                {
                    if (updateDto.City != null)
                    {
                        var city = await _cityRepository.GetByName(updateDto.City, updateDto.ProvinceId);
                        if (city != null)
                        {
                            existing.CityId = city.Id;
                        }
                    }
                }

                existing.CountryId = updateDto.CountryId;
                existing.ProvinceId = updateDto.ProvinceId;
                existing.CityId = updateDto.CityId;
                existing.Street = updateDto.Street;
                existing.AdditionalInfo = updateDto.AdditionalInfo;
				await _customerAddressRepository.Update(existing, userId, cancellationToken);
				var result = _mapper.Map<CustomerAddressListDto>(existing);

				return new GlobalResponse<CustomerAddressListDto> { StatusCode = 200, Data = result, Message = "آدرس با موفقیت بروزرسانی شد" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<CustomerAddressListDto> { StatusCode = 500, Message = $"خطا در بروزرسانی آدرس: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				var address = await _customerAddressRepository.GetById(id, cancellationToken);
				if (address == null)
					return new GlobalResponse<ResponseResultGlobally>
					{
						StatusCode = 404,
						Message = "آدرس یافت نشد",
						Data = new ResponseResultGlobally { DoneSuccessfully = false }
					};

				await _customerAddressRepository.Delete(id, userId);

				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 200,
					Message = "آدرس با موفقیت حذف شد",
					Data = new ResponseResultGlobally { DoneSuccessfully = true }
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 500,
					Message = $"خطا در حذف آدرس: {ex.Message}",
					Data = new ResponseResultGlobally { DoneSuccessfully = false }
				};
			}
		}
	}
}