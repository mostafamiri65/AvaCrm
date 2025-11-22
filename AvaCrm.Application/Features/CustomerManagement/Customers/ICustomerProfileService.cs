using AvaCrm.Application.DTOs.CustomerManagement.Customers;
using AvaCrm.Application.Pagination;

namespace AvaCrm.Application.Features.CustomerManagement.Customers
{
    public interface ICustomerProfileService
    {
		Task<GlobalResponse<CustomerProfileDto>> GetCustomerProfile(long customerId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<PaginatedResult<CustomerActivityDto>>> GetCustomerActivities(long customerId, PaginationRequest request, CancellationToken cancellationToken = default);
        Task<GlobalResponse<CustomerStatsDto>> GetCustomerStats(long customerId, CancellationToken cancellationToken = default);
	}
}