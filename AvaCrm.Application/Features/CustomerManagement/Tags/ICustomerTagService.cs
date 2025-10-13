using AvaCrm.Application.DTOs.CustomerManagement.Customers;
using AvaCrm.Application.Pagination;

namespace AvaCrm.Application.Features.CustomerManagement.Tags
{
    public interface ICustomerTagService
    {
		Task<GlobalResponse<PaginatedResult<CustomerTagListDto>>> GetByCustomerId(long customerId, PaginationRequest request, CancellationToken cancellationToken = default);
        Task<GlobalResponse<PaginatedResult<CustomerTagListDto>>> GetByTagId(int tagId, PaginationRequest request, CancellationToken cancellationToken = default);
        Task<GlobalResponse<CustomerTagListDto>> AddTagToCustomer(CustomerTagCreateDto createDto, long userId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<ResponseResultGlobally>> RemoveTagFromCustomer(long customerId, int tagId, long userId, CancellationToken cancellationToken = default);
	}
}