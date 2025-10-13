using AvaCrm.Application.DTOs.CustomerManagement.OrganizationCustomers;
using AvaCrm.Application.Services;
using AvaCrm.Domain.Contracts.CustomerManagement;
using AvaCrm.Domain.Entities.CustomerManagement;

namespace AvaCrm.Application.Features.CustomerManagement.Customers
{
    public class OrganizationCustomerService : BaseService, IOrganizationCustomerService
	{
		private readonly IOrganizationCustomerRepository _organizationCustomerRepository;
		private readonly ICustomerRepository _customerRepository;

		public OrganizationCustomerService(
			IMapper mapper,
			IOrganizationCustomerRepository organizationCustomerRepository,
			ICustomerRepository customerRepository) : base(mapper)
		{
			_organizationCustomerRepository = organizationCustomerRepository;
			_customerRepository = customerRepository;
		}

		public async Task<GlobalResponse<OrganizationCustomerListDto>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default)
		{
			try
			{
				var organizationCustomer = await _organizationCustomerRepository.GetByCustomerId(customerId, cancellationToken);
				if (organizationCustomer == null)
					return new GlobalResponse<OrganizationCustomerListDto> { StatusCode = 404, Message = "مشتری حقوقی یافت نشد" };

				var result = _mapper.Map<OrganizationCustomerListDto>(organizationCustomer);
				return new GlobalResponse<OrganizationCustomerListDto> { StatusCode = 200, Data = result, Message = "عملیات موفق" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<OrganizationCustomerListDto> { StatusCode = 500, Message = $"خطا در دریافت اطلاعات: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<OrganizationCustomerListDto>> Create(OrganizationCustomerCreateDto createDto, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				// Verify customer exists
				var customer = await _customerRepository.GetById(createDto.CustomerId, cancellationToken);
				if (customer == null)
					return new GlobalResponse<OrganizationCustomerListDto> { StatusCode = 404, Message = "مشتری یافت نشد" };

				var organizationCustomer = _mapper.Map<OrganizationCustomer>(createDto);
				var created = await _organizationCustomerRepository.Create(organizationCustomer, userId);
				var result = _mapper.Map<OrganizationCustomerListDto>(created);

				return new GlobalResponse<OrganizationCustomerListDto> { StatusCode = 201, Data = result, Message = "مشتری حقوقی با موفقیت ایجاد شد" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<OrganizationCustomerListDto> { StatusCode = 500, Message = $"خطا در ایجاد مشتری حقوقی: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<OrganizationCustomerListDto>> Update(OrganizationCustomerUpdateDto updateDto, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				var existing = await _organizationCustomerRepository.GetById(updateDto.Id, cancellationToken);
				if (existing == null)
					return new GlobalResponse<OrganizationCustomerListDto> { StatusCode = 404, Message = "مشتری حقوقی یافت نشد" };

				_mapper.Map(updateDto, existing);
				await _organizationCustomerRepository.Update(existing, userId, cancellationToken);
				var result = _mapper.Map<OrganizationCustomerListDto>(existing);

				return new GlobalResponse<OrganizationCustomerListDto> { StatusCode = 200, Data = result, Message = "مشتری حقوقی با موفقیت بروزرسانی شد" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<OrganizationCustomerListDto> { StatusCode = 500, Message = $"خطا در بروزرسانی مشتری حقوقی: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				var organizationCustomer = await _organizationCustomerRepository.GetById(id, cancellationToken);
				if (organizationCustomer == null)
					return new GlobalResponse<ResponseResultGlobally>
					{
						StatusCode = 404,
						Message = "مشتری حقوقی یافت نشد",
						Data = new ResponseResultGlobally { DoneSuccessfully = false }
					};

				await _organizationCustomerRepository.Delete(id, userId);

				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 200,
					Message = "مشتری حقوقی با موفقیت حذف شد",
					Data = new ResponseResultGlobally { DoneSuccessfully = true }
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 500,
					Message = $"خطا در حذف مشتری حقوقی: {ex.Message}",
					Data = new ResponseResultGlobally { DoneSuccessfully = false }
				};
			}
		}

	}
}