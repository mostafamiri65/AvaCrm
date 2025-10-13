using AvaCrm.Application.DTOs.CustomerManagement.FollowUps;
using AvaCrm.Application.Pagination;
using AvaCrm.Application.Services;
using AvaCrm.Domain.Contracts.CustomerManagement;
using AvaCrm.Domain.Entities.CustomerManagement;
using Microsoft.EntityFrameworkCore;
namespace AvaCrm.Application.Features.CustomerManagement.Customers;

public class FollowUpService : BaseService, IFollowUpService
{
	private readonly IFollowUpRepository _followUpRepository;
	private readonly ICustomerRepository _customerRepository;

	public FollowUpService(
		IMapper mapper,
		IFollowUpRepository followUpRepository,
		ICustomerRepository customerRepository) : base(mapper)
	{
		_followUpRepository = followUpRepository;
		_customerRepository = customerRepository;
	}

	public async Task<GlobalResponse<FollowUpListDto>> GetById(long id, CancellationToken cancellationToken = default)
	{
		try
		{
			var followUp = await _followUpRepository.GetById(id, cancellationToken);
			if (followUp == null)
				return new GlobalResponse<FollowUpListDto> { StatusCode = 404, Message = "پیگیری یافت نشد" };

			var result = _mapper.Map<FollowUpListDto>(followUp);
			return new GlobalResponse<FollowUpListDto> { StatusCode = 200, Data = result, Message = "عملیات موفق" };
		}
		catch (Exception ex)
		{
			return new GlobalResponse<FollowUpListDto> { StatusCode = 500, Message = $"خطا در دریافت اطلاعات: {ex.Message}" };
		}
	}

	public async Task<GlobalResponse<PaginatedResult<FollowUpListDto>>> GetByCustomerId(long customerId, PaginationRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			var query = await _followUpRepository.GetByCustomerId(customerId, cancellationToken);

			// Apply search filter
			if (!string.IsNullOrWhiteSpace(request.SearchTerm))
			{
				query = query.Where(f => f.Description.Contains(request.SearchTerm) ||
										f.NextFollowUpDescription.Contains(request.SearchTerm)).ToList();
			}

			// Apply sorting
			query = request.SortDirection == "desc" ?
				query.OrderByDescending(f => f.NextFollowUpDate).ToList() :
				query.OrderBy(f => f.NextFollowUpDate).ToList();

			var totalCount = query.Count;

			var followUps =  query
				.Skip((request.PageNumber - 1) * request.PageSize)
				.Take(request.PageSize)
				.ToList();

			var result = new PaginatedResult<FollowUpListDto>
			{
				Items = _mapper.Map<List<FollowUpListDto>>(followUps),
				TotalCount = totalCount,
				PageNumber = request.PageNumber,
				PageSize = request.PageSize
			};

			return new GlobalResponse<PaginatedResult<FollowUpListDto>>
			{
				StatusCode = 200,
				Data = result,
				Message = "عملیات موفق"
			};
		}
		catch (Exception ex)
		{
			return new GlobalResponse<PaginatedResult<FollowUpListDto>>
			{
				StatusCode = 500,
				Message = $"خطا در دریافت لیست پیگیری‌ها: {ex.Message}"
			};
		}
	}

	public async Task<GlobalResponse<PaginatedResult<FollowUpListDto>>> GetUpcomingFollowUps(PaginationRequest request, CancellationToken cancellationToken = default)
	{
		try
		{
			var fromDate = DateTime.Now;
			var toDate = DateTime.Now.AddDays(7); // Next 7 days

			var query = _followUpRepository.GetAll()
				.Where(f => f.NextFollowUpDate >= fromDate && f.NextFollowUpDate <= toDate)
				.Include(f => f.Customer);

			var totalCount = await query.CountAsync(cancellationToken);

			var followUps = await query
				.OrderBy(f => f.NextFollowUpDate)
				.Skip((request.PageNumber - 1) * request.PageSize)
				.Take(request.PageSize)
				.ToListAsync(cancellationToken);

			var result = new PaginatedResult<FollowUpListDto>
			{
				Items = _mapper.Map<List<FollowUpListDto>>(followUps),
				TotalCount = totalCount,
				PageNumber = request.PageNumber,
				PageSize = request.PageSize
			};

			return new GlobalResponse<PaginatedResult<FollowUpListDto>>
			{
				StatusCode = 200,
				Data = result,
				Message = "عملیات موفق"
			};
		}
		catch (Exception ex)
		{
			return new GlobalResponse<PaginatedResult<FollowUpListDto>>
			{
				StatusCode = 500,
				Message = $"خطا در دریافت لیست پیگیری‌های آینده: {ex.Message}"
			};
		}
	}

	public async Task<GlobalResponse<FollowUpListDto>> Create(FollowUpCreateDto createDto, long userId, CancellationToken cancellationToken = default)
	{
		try
		{
			// Verify customer exists
			var customer = await _customerRepository.GetById(createDto.CustomerId, cancellationToken);
			if (customer == null)
				return new GlobalResponse<FollowUpListDto> { StatusCode = 404, Message = "مشتری یافت نشد" };

			var followUp = _mapper.Map<FollowUp>(createDto);
			var created = await _followUpRepository.Create(followUp, userId);
			var result = _mapper.Map<FollowUpListDto>(created);

			return new GlobalResponse<FollowUpListDto> { StatusCode = 201, Data = result, Message = "پیگیری با موفقیت ایجاد شد" };
		}
		catch (Exception ex)
		{
			return new GlobalResponse<FollowUpListDto> { StatusCode = 500, Message = $"خطا در ایجاد پیگیری: {ex.Message}" };
		}
	}

	public async Task<GlobalResponse<FollowUpListDto>> Update(FollowUpUpdateDto updateDto, long userId, CancellationToken cancellationToken = default)
	{
		try
		{
			var existing = await _followUpRepository.GetById(updateDto.Id, cancellationToken);
			if (existing == null)
				return new GlobalResponse<FollowUpListDto> { StatusCode = 404, Message = "پیگیری یافت نشد" };

			_mapper.Map(updateDto, existing);
			await _followUpRepository.Update(existing, userId, cancellationToken);
			var result = _mapper.Map<FollowUpListDto>(existing);

			return new GlobalResponse<FollowUpListDto> { StatusCode = 200, Data = result, Message = "پیگیری با موفقیت بروزرسانی شد" };
		}
		catch (Exception ex)
		{
			return new GlobalResponse<FollowUpListDto> { StatusCode = 500, Message = $"خطا در بروزرسانی پیگیری: {ex.Message}" };
		}
	}

	public async Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default)
	{
		try
		{
			var followUp = await _followUpRepository.GetById(id, cancellationToken);
			if (followUp == null)
				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 404,
					Message = "پیگیری یافت نشد",
					Data = new ResponseResultGlobally { DoneSuccessfully = false }
				};

			await _followUpRepository.Delete(id, userId);

			return new GlobalResponse<ResponseResultGlobally>
			{
				StatusCode = 200,
				Message = "پیگیری با موفقیت حذف شد",
				Data = new ResponseResultGlobally { DoneSuccessfully = true }
			};
		}
		catch (Exception ex)
		{
			return new GlobalResponse<ResponseResultGlobally>
			{
				StatusCode = 500,
				Message = $"خطا در حذف پیگیری: {ex.Message}",
				Data = new ResponseResultGlobally { DoneSuccessfully = false }
			};
		}
	}
}
