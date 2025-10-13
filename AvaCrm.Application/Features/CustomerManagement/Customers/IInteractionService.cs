using AvaCrm.Application.DTOs.CustomerManagement.Interactions;
using AvaCrm.Application.Pagination;
using AvaCrm.Domain.Enums.CustomerManagement;

namespace AvaCrm.Application.Features.CustomerManagement.Customers
{
    public interface IInteractionService
    {
		Task<GlobalResponse<InteractionListDto>> GetById(long id, CancellationToken cancellationToken = default);
        Task<GlobalResponse<PaginatedResult<InteractionListDto>>> GetByCustomerId(long customerId, PaginationRequest request, CancellationToken cancellationToken = default);
        Task<GlobalResponse<PaginatedResult<InteractionListDto>>> GetByType(InteractionType interactionType, PaginationRequest request, CancellationToken cancellationToken = default);
        Task<GlobalResponse<InteractionListDto>> Create(InteractionCreateDto createDto, long userId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<InteractionListDto>> Update(InteractionUpdateDto updateDto, long userId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default);
	}
}