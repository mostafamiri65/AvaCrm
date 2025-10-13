using AvaCrm.Application.DTOs.CustomerManagement.OrganizationCustomers;

namespace AvaCrm.Application.Features.CustomerManagement.Customers
{
    public interface IOrganizationCustomerService
    {
		Task<GlobalResponse<OrganizationCustomerListDto>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<OrganizationCustomerListDto>> Create(OrganizationCustomerCreateDto createDto, long userId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<OrganizationCustomerListDto>> Update(OrganizationCustomerUpdateDto updateDto, long userId, CancellationToken cancellationToken = default);
        Task<GlobalResponse<ResponseResultGlobally>> Delete(long id, long userId, CancellationToken cancellationToken = default);
	}
}