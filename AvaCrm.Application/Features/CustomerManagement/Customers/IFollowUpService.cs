using AvaCrm.Application.DTOs.CustomerManagement.FollowUps;
using AvaCrm.Application.Pagination;

namespace AvaCrm.Application.Features.CustomerManagement.Customers
{
    public interface IFollowUpService
    {
		Task<GlobalResponse<FollowUpListDto>> GetById(long id, CancellationToken cancellationToken = default);
        Task<GlobalResponse<PaginatedResult<FollowUpListDto>>> GetByCustomerId(long customerId, PaginationRequest request, CancellationToken cancellationToken = default);
        Task<GlobalResponse<PaginatedResult<FollowUpListDto>>> GetUpcomingFollowUps(PaginationRequest request, CancellationToken cancellationToken = default);
        Task<GlobalResponse<FollowUpListDto>> Create(FollowUpCreateDto createDto, long userId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<FollowUpListDto>> Update(FollowUpUpdateDto updateDto, long userId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default);
	}
}