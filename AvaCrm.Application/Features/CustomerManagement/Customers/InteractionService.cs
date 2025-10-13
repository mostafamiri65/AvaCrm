using AvaCrm.Application.DTOs.CustomerManagement.Interactions;
using AvaCrm.Application.Pagination;
using AvaCrm.Application.Services;
using AvaCrm.Domain.Contracts.CustomerManagement;
using AvaCrm.Domain.Entities.CustomerManagement;
using AvaCrm.Domain.Enums.CustomerManagement;
using Microsoft.EntityFrameworkCore;

namespace AvaCrm.Application.Features.CustomerManagement.Customers
{
    public class InteractionService : BaseService, IInteractionService
	{
		private readonly IInteractionRepository _interactionRepository;
		private readonly ICustomerRepository _customerRepository;

		public InteractionService(
			IMapper mapper,
			IInteractionRepository interactionRepository,
			ICustomerRepository customerRepository) : base(mapper)
		{
			_interactionRepository = interactionRepository;
			_customerRepository = customerRepository;
		}

		public async Task<GlobalResponse<InteractionListDto>> GetById(long id, CancellationToken cancellationToken = default)
		{
			try
			{
				var interaction = await _interactionRepository.GetById(id, cancellationToken);
				if (interaction == null)
					return new GlobalResponse<InteractionListDto> { StatusCode = 404, Message = "تعامل یافت نشد" };

				var result = _mapper.Map<InteractionListDto>(interaction);
				return new GlobalResponse<InteractionListDto> { StatusCode = 200, Data = result, Message = "عملیات موفق" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<InteractionListDto> { StatusCode = 500, Message = $"خطا در دریافت اطلاعات: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<PaginatedResult<InteractionListDto>>> GetByCustomerId(long customerId, PaginationRequest request, CancellationToken cancellationToken = default)
		{
			try
			{
				var query = _interactionRepository.GetAll().Where(i => i.CustomerId == customerId);

				// Apply search filter
				if (!string.IsNullOrWhiteSpace(request.SearchTerm))
				{
					query = query.Where(i => i.Subject.Contains(request.SearchTerm) ||
											i.Description.Contains(request.SearchTerm));
				}

				// Apply sorting
				query = request.SortDirection == "desc" ?
					query.OrderByDescending(i => i.CreationDate) :
					query.OrderBy(i => i.CreationDate);

				var totalCount = await query.CountAsync(cancellationToken);

				var interactions = await query
					.Skip((request.PageNumber - 1) * request.PageSize)
					.Take(request.PageSize)
					.ToListAsync(cancellationToken);

				var result = new PaginatedResult<InteractionListDto>
				{
					Items = _mapper.Map<List<InteractionListDto>>(interactions),
					TotalCount = totalCount,
					PageNumber = request.PageNumber,
					PageSize = request.PageSize
				};

				return new GlobalResponse<PaginatedResult<InteractionListDto>>
				{
					StatusCode = 200,
					Data = result,
					Message = "عملیات موفق"
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<PaginatedResult<InteractionListDto>>
				{
					StatusCode = 500,
					Message = $"خطا در دریافت لیست تعاملات: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<PaginatedResult<InteractionListDto>>> GetByType(InteractionType interactionType, PaginationRequest request, CancellationToken cancellationToken = default)
		{
			try
			{
				var query = _interactionRepository.GetAll().Where(i => i.InteractionType == interactionType);

				// Apply search filter
				if (!string.IsNullOrWhiteSpace(request.SearchTerm))
				{
					query = query.Where(i => i.Subject.Contains(request.SearchTerm) ||
											i.Description.Contains(request.SearchTerm));
				}

				// Apply sorting
				query = request.SortDirection == "desc" ?
					query.OrderByDescending(i => i.CreationDate) :
					query.OrderBy(i => i.CreationDate);

				var totalCount = await query.CountAsync(cancellationToken);

				var interactions = await query
					.Skip((request.PageNumber - 1) * request.PageSize)
					.Take(request.PageSize)
					.ToListAsync(cancellationToken);

				var result = new PaginatedResult<InteractionListDto>
				{
					Items = _mapper.Map<List<InteractionListDto>>(interactions),
					TotalCount = totalCount,
					PageNumber = request.PageNumber,
					PageSize = request.PageSize
				};

				return new GlobalResponse<PaginatedResult<InteractionListDto>>
				{
					StatusCode = 200,
					Data = result,
					Message = "عملیات موفق"
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<PaginatedResult<InteractionListDto>>
				{
					StatusCode = 500,
					Message = $"خطا در دریافت لیست تعاملات: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<InteractionListDto>> Create(InteractionCreateDto createDto, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				// Verify customer exists
				var customer = await _customerRepository.GetById(createDto.CustomerId, cancellationToken);
				if (customer == null)
					return new GlobalResponse<InteractionListDto> { StatusCode = 404, Message = "مشتری یافت نشد" };

				var interaction = _mapper.Map<Interaction>(createDto);
				var created = await _interactionRepository.Create(interaction, userId);
				var result = _mapper.Map<InteractionListDto>(created);

				return new GlobalResponse<InteractionListDto> { StatusCode = 201, Data = result, Message = "تعامل با موفقیت ایجاد شد" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<InteractionListDto> { StatusCode = 500, Message = $"خطا در ایجاد تعامل: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<InteractionListDto>> Update(InteractionUpdateDto updateDto, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				var existing = await _interactionRepository.GetById(updateDto.Id, cancellationToken);
				if (existing == null)
					return new GlobalResponse<InteractionListDto> { StatusCode = 404, Message = "تعامل یافت نشد" };

				_mapper.Map(updateDto, existing);
				await _interactionRepository.Update(existing, userId, cancellationToken);
				var result = _mapper.Map<InteractionListDto>(existing);

				return new GlobalResponse<InteractionListDto> { StatusCode = 200, Data = result, Message = "تعامل با موفقیت بروزرسانی شد" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<InteractionListDto> { StatusCode = 500, Message = $"خطا در بروزرسانی تعامل: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				var interaction = await _interactionRepository.GetById(id, cancellationToken);
				if (interaction == null)
					return new GlobalResponse<ResponseResultGlobally>
					{
						StatusCode = 404,
						Message = "تعامل یافت نشد",
						Data = new ResponseResultGlobally { DoneSuccessfully = false }
					};

				await _interactionRepository.Delete(id, userId);

				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 200,
					Message = "تعامل با موفقیت حذف شد",
					Data = new ResponseResultGlobally { DoneSuccessfully = true }
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 500,
					Message = $"خطا در حذف تعامل: {ex.Message}",
					Data = new ResponseResultGlobally { DoneSuccessfully = false }
				};
			}
		}
	}
}