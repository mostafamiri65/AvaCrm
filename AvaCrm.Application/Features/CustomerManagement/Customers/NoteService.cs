using AvaCrm.Application.DTOs.CustomerManagement.Notes;
using AvaCrm.Application.Pagination;
using AvaCrm.Application.Services;
using AvaCrm.Domain.Contracts.CustomerManagement;
using AvaCrm.Domain.Entities.CustomerManagement;
using Microsoft.EntityFrameworkCore;

namespace AvaCrm.Application.Features.CustomerManagement.Customers
{
    public class NoteService : BaseService, INoteService
	{
		private readonly INoteRepository _noteRepository;
		private readonly ICustomerRepository _customerRepository;

		public NoteService(
			IMapper mapper,
			INoteRepository noteRepository,
			ICustomerRepository customerRepository) : base(mapper)
		{
			_noteRepository = noteRepository;
			_customerRepository = customerRepository;
		}

		public async Task<GlobalResponse<NoteListDto>> GetById(long id, CancellationToken cancellationToken = default)
		{
			try
			{
				var note = await _noteRepository.GetById(id, cancellationToken);
				if (note == null)
					return new GlobalResponse<NoteListDto> { StatusCode = 404, Message = "یادداشت یافت نشد" };

				var result = _mapper.Map<NoteListDto>(note);
				return new GlobalResponse<NoteListDto> { StatusCode = 200, Data = result, Message = "عملیات موفق" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<NoteListDto> { StatusCode = 500, Message = $"خطا در دریافت اطلاعات: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<PaginatedResult<NoteListDto>>> GetByCustomerId(long customerId, PaginationRequest request, CancellationToken cancellationToken = default)
		{
			try
			{
				var query = _noteRepository.GetAll().Where(n => n.CustomerId == customerId);

				// Apply search filter
				if (!string.IsNullOrWhiteSpace(request.SearchTerm))
				{
					query = query.Where(n => n.Content.Contains(request.SearchTerm));
				}

				// Apply sorting
				query = request.SortDirection == "desc" ?
					query.OrderByDescending(n => n.CreationDate) :
					query.OrderBy(n => n.CreationDate);

				var totalCount = await query.CountAsync(cancellationToken);

				var notes = await query
					.Skip((request.PageNumber - 1) * request.PageSize)
					.Take(request.PageSize)
					.ToListAsync(cancellationToken);

				var result = new PaginatedResult<NoteListDto>
				{
					Items = _mapper.Map<List<NoteListDto>>(notes),
					TotalCount = totalCount,
					PageNumber = request.PageNumber,
					PageSize = request.PageSize
				};

				return new GlobalResponse<PaginatedResult<NoteListDto>>
				{
					StatusCode = 200,
					Data = result,
					Message = "عملیات موفق"
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<PaginatedResult<NoteListDto>>
				{
					StatusCode = 500,
					Message = $"خطا در دریافت لیست یادداشت‌ها: {ex.Message}"
				};
			}
		}

		public async Task<GlobalResponse<NoteListDto>> Create(NoteCreateDto createDto, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				// Verify customer exists
				var customer = await _customerRepository.GetById(createDto.CustomerId, cancellationToken);
				if (customer == null)
					return new GlobalResponse<NoteListDto> { StatusCode = 404, Message = "مشتری یافت نشد" };

				var note = _mapper.Map<Note>(createDto);
				var created = await _noteRepository.Create(note, userId);
				var result = _mapper.Map<NoteListDto>(created);

				return new GlobalResponse<NoteListDto> { StatusCode = 201, Data = result, Message = "یادداشت با موفقیت ایجاد شد" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<NoteListDto> { StatusCode = 500, Message = $"خطا در ایجاد یادداشت: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<NoteListDto>> Update(NoteUpdateDto updateDto, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				var existing = await _noteRepository.GetById(updateDto.Id, cancellationToken);
				if (existing == null)
					return new GlobalResponse<NoteListDto> { StatusCode = 404, Message = "یادداشت یافت نشد" };

				_mapper.Map(updateDto, existing);
				await _noteRepository.Update(existing, userId, cancellationToken);
				var result = _mapper.Map<NoteListDto>(existing);

				return new GlobalResponse<NoteListDto> { StatusCode = 200, Data = result, Message = "یادداشت با موفقیت بروزرسانی شد" };
			}
			catch (Exception ex)
			{
				return new GlobalResponse<NoteListDto> { StatusCode = 500, Message = $"خطا در بروزرسانی یادداشت: {ex.Message}" };
			}
		}

		public async Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default)
		{
			try
			{
				var note = await _noteRepository.GetById(id, cancellationToken);
				if (note == null)
					return new GlobalResponse<ResponseResultGlobally>
					{
						StatusCode = 404,
						Message = "یادداشت یافت نشد",
						Data = new ResponseResultGlobally { DoneSuccessfully = false }
					};

				await _noteRepository.Delete(id, userId);

				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 200,
					Message = "یادداشت با موفقیت حذف شد",
					Data = new ResponseResultGlobally { DoneSuccessfully = true }
				};
			}
			catch (Exception ex)
			{
				return new GlobalResponse<ResponseResultGlobally>
				{
					StatusCode = 500,
					Message = $"خطا در حذف یادداشت: {ex.Message}",
					Data = new ResponseResultGlobally { DoneSuccessfully = false }
				};
			}
		}

	}
}