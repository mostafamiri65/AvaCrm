using AvaCrm.Application.DTOs.CustomerManagement.Notes;
using AvaCrm.Application.Pagination;

namespace AvaCrm.Application.Features.CustomerManagement.Customers
{
    public interface INoteService
    {
		Task<GlobalResponse<NoteListDto>> GetById(long id, CancellationToken cancellationToken = default);
        Task<GlobalResponse<PaginatedResult<NoteListDto>>> GetByCustomerId(long customerId, PaginationRequest request, CancellationToken cancellationToken = default);
        Task<GlobalResponse<NoteListDto>> Create(NoteCreateDto createDto, long userId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<NoteListDto>> Update(NoteUpdateDto updateDto, long userId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default);
	}
}