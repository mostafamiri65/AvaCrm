using AvaCrm.Application.DTOs.CustomerManagement.ContactPersons;
using AvaCrm.Application.Pagination;
using AvaCrm.Application.Services;
using AvaCrm.Domain.Contracts.CustomerManagement;
using AvaCrm.Domain.Entities.CustomerManagement;
using Microsoft.EntityFrameworkCore;

namespace AvaCrm.Application.Features.CustomerManagement.Customers
{
    public class ContactPersonService : BaseService, IContactPersonService
	{
		private readonly IContactPersonRepository _contactPersonRepository;
		private readonly ICustomerRepository _customerRepository;

		public ContactPersonService(
			IMapper mapper,
			IContactPersonRepository contactPersonRepository,
			ICustomerRepository customerRepository) : base(mapper)
		{
			_contactPersonRepository = contactPersonRepository;
			_customerRepository = customerRepository;
		}

		public async Task<GlobalResponse<ContactPersonListDto>> GetById(long id, CancellationToken cancellationToken = default)
		{
			try
			{
				var contactPerson = await _contactPersonRepository.GetById(id, cancellationToken);
				if (contactPerson == null)
					return new GlobalResponse<ContactPersonListDto> { StatusCode = 404, Message = "شخص ارتباطی یافت نشد" };

				var result = _mapper.Map<ContactPersonListDto>(contactPerson);
				return new GlobalResponse<ContactPersonListDto> { StatusCode = 200, Data = result, Message = "عملیات موفق" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<ContactPersonListDto> { StatusCode = 500, Message = $"خطا در دریافت اطلاعات: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<PaginatedResult<ContactPersonListDto>>> GetByCustomerId(long customerId, PaginationRequest request, CancellationToken cancellationToken = default)
		{
			try
			{
				var query = _contactPersonRepository.GetAll().Where(cp => cp.CustomerId == customerId);

				// Apply search filter
				if (!string.IsNullOrWhiteSpace(request.SearchTerm))
				{
					query = query.Where(cp => cp.FullName.Contains(request.SearchTerm) ||
											 cp.JobTitle.Contains(request.SearchTerm) ||
											 cp.Email.Contains(request.SearchTerm));
				}

				// Apply sorting
				query = request.SortDirection == "desc" ?
					query.OrderByDescending(cp => cp.CreationDate) :
					query.OrderBy(cp => cp.CreationDate);

				var totalCount = await query.CountAsync(cancellationToken);

				var contactPersons = await query
					.Skip((request.PageNumber - 1) * request.PageSize)
					.Take(request.PageSize)
					.ToListAsync(cancellationToken);

				var result = new PaginatedResult<ContactPersonListDto>
				{
					Items = _mapper.Map<List<ContactPersonListDto>>(contactPersons),
					TotalCount = totalCount,
					PageNumber = request.PageNumber,
					PageSize = request.PageSize
				};

				return new GlobalResponse<PaginatedResult<ContactPersonListDto>>
				{
					StatusCode = 200,
					Data = result,
					Message = "عملیات موفق"
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<PaginatedResult<ContactPersonListDto>>
				{
					StatusCode = 500,
					Message = $"خطا در دریافت لیست افراد ارتباطی: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<ContactPersonListDto>> Create(ContactPersonCreateDto createDto, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				// Verify customer exists
				var customer = await _customerRepository.GetById(createDto.CustomerId, cancellationToken);
				if (customer == null)
					return new GlobalResponse<ContactPersonListDto> { StatusCode = 404, Message = "مشتری یافت نشد" };

				var contactPerson = _mapper.Map<ContactPerson>(createDto);
				var created = await _contactPersonRepository.Create(contactPerson, userId);
				var result = _mapper.Map<ContactPersonListDto>(created);

				return new GlobalResponse<ContactPersonListDto> { StatusCode = 201, Data = result, Message = "شخص ارتباطی با موفقیت ایجاد شد" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<ContactPersonListDto> { StatusCode = 500, Message = $"خطا در ایجاد شخص ارتباطی: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<ContactPersonListDto>> Update(ContactPersonUpdateDto updateDto, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				var existing = await _contactPersonRepository.GetById(updateDto.Id, cancellationToken);
				if (existing == null)
					return new GlobalResponse<ContactPersonListDto> { StatusCode = 404, Message = "شخص ارتباطی یافت نشد" };

				_mapper.Map(updateDto, existing);
				await _contactPersonRepository.Update(existing, userId, cancellationToken);
				var result = _mapper.Map<ContactPersonListDto>(existing);

				return new GlobalResponse<ContactPersonListDto> { StatusCode = 200, Data = result, Message = "شخص ارتباطی با موفقیت بروزرسانی شد" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<ContactPersonListDto> { StatusCode = 500, Message = $"خطا در بروزرسانی شخص ارتباطی: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				var contactPerson = await _contactPersonRepository.GetById(id, cancellationToken);
				if (contactPerson == null)
					return new GlobalResponse<ResponseResultGlobally>
					{
						StatusCode = 404,
						Message = "شخص ارتباطی یافت نشد",
						Data = new ResponseResultGlobally { DoneSuccessfully = false }
					};

				await _contactPersonRepository.Delete(id, userId);

				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 200,
					Message = "شخص ارتباطی با موفقیت حذف شد",
					Data = new ResponseResultGlobally { DoneSuccessfully = true }
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 500,
					Message = $"خطا در حذف شخص ارتباطی: {ex.Message}",
					Data = new ResponseResultGlobally { DoneSuccessfully = false }
				};
			}
		}
	}
}