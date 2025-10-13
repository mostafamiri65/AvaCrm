using AvaCrm.Application.DTOs.CustomerManagement.Customers;
using AvaCrm.Application.Pagination;
using AvaCrm.Application.Services;
using AvaCrm.Domain.Contracts.CustomerManagement;
using AvaCrm.Domain.Entities.CustomerManagement;

namespace AvaCrm.Application.Features.CustomerManagement.Tags
{
    public class CustomerTagService : BaseService, ICustomerTagService
	{
		private readonly ICustomerTagRepository _customerTagRepository;
		private readonly ICustomerRepository _customerRepository;
		private readonly ITagRepository _tagRepository;

		public CustomerTagService(
			IMapper mapper,
			ICustomerTagRepository customerTagRepository,
			ICustomerRepository customerRepository,
			ITagRepository tagRepository) : base(mapper)
		{
			_customerTagRepository = customerTagRepository;
			_customerRepository = customerRepository;
			_tagRepository = tagRepository;
		}

		public async Task<GlobalResponse<PaginatedResult<CustomerTagListDto>>> GetByCustomerId(long customerId, PaginationRequest request, CancellationToken cancellationToken = default)
		{
			try
			{
				var query = await _customerTagRepository.GetByCustomerId(customerId);

				var totalCount =  query.Count;

                var customerTags =  query
                    .Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

				var result = new PaginatedResult<CustomerTagListDto>
				{
					Items = _mapper.Map<List<CustomerTagListDto>>(customerTags),
					TotalCount = totalCount,
					PageNumber = request.PageNumber,
					PageSize = request.PageSize
				};

				return new GlobalResponse<PaginatedResult<CustomerTagListDto>>
				{
					StatusCode = 200,
					Data = result,
					Message = "عملیات موفق"
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<PaginatedResult<CustomerTagListDto>>
				{
					StatusCode = 500,
					Message = $"خطا در دریافت لیست تگ‌های مشتری: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<PaginatedResult<CustomerTagListDto>>> GetByTagId(int tagId, PaginationRequest request, CancellationToken cancellationToken = default)
		{
			try
			{
				var query =await _customerTagRepository.GetByTagId(tagId);

				var totalCount = query.Count;

				var customerTags =  query
					.Skip((request.PageNumber - 1) * request.PageSize)
					.Take(request.PageSize)
					.ToList();

				var result = new PaginatedResult<CustomerTagListDto>
				{
					Items = _mapper.Map<List<CustomerTagListDto>>(customerTags),
					TotalCount = totalCount,
					PageNumber = request.PageNumber,
					PageSize = request.PageSize
				};

				return new GlobalResponse<PaginatedResult<CustomerTagListDto>>
				{
					StatusCode = 200,
					Data = result,
					Message = "عملیات موفق"
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<PaginatedResult<CustomerTagListDto>>
				{
					StatusCode = 500,
					Message = $"خطا در دریافت لیست مشتریان تگ: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<CustomerTagListDto>> AddTagToCustomer(CustomerTagCreateDto createDto, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				// Verify customer exists
				var customer = await _customerRepository.GetById(createDto.CustomerId, cancellationToken);
				if (customer == null)
					return new GlobalResponse<CustomerTagListDto> { StatusCode = 404, Message = "مشتری یافت نشد" };

				// Verify tag exists
				var tag = await _tagRepository.GetById(createDto.TagId);
				if (tag == null)
					return new GlobalResponse<CustomerTagListDto> { StatusCode = 404, Message = "تگ یافت نشد" };

				// Check if relationship already exists
				var existing = await _customerTagRepository.GetByCustomerAndTag(createDto.CustomerId, createDto.TagId, cancellationToken);
				if (existing != null)
					return new GlobalResponse<CustomerTagListDto> { StatusCode = 400, Message = "این تگ قبلاً به مشتری اختصاص داده شده است" };

				var customerTag = new CustomerTag()
                {
                    CustomerId = createDto.CustomerId,
                    TagId = createDto.TagId
                };
				var created = await _customerTagRepository.CreateCustomerTag(customerTag);

				
				return new GlobalResponse<CustomerTagListDto> { StatusCode = 201, Data = null, Message = "تگ با موفقیت به مشتری اضافه شد" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<CustomerTagListDto> { StatusCode = 500, Message = $"خطا در اضافه کردن تگ به مشتری: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<ResponseResultGlobally>> RemoveTagFromCustomer(long customerId, int tagId, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				var customerTag = await _customerTagRepository.GetByCustomerAndTag(customerId, tagId, cancellationToken);
				if (customerTag == null)
					return new GlobalResponse<ResponseResultGlobally>
					{
						StatusCode = 404,
						Message = "ارتباط تگ با مشتری یافت نشد",
						Data = new ResponseResultGlobally { DoneSuccessfully = false }
					};

				await _customerTagRepository.RemoveCustomerTag(customerTag);

				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 200,
					Message = "تگ با موفقیت از مشتری حذف شد",
					Data = new ResponseResultGlobally { DoneSuccessfully = true }
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 500,
					Message = $"خطا در حذف تگ از مشتری: {ex.Message}",
					Data = new ResponseResultGlobally { DoneSuccessfully = false }
				};
			}
		}
	}
}