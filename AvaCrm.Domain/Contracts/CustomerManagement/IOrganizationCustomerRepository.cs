using AvaCrm.Domain.Entities.CustomerManagement;

namespace AvaCrm.Domain.Contracts.CustomerManagement
{
    public interface IOrganizationCustomerRepository : IGenericRepository<OrganizationCustomer>
    {
        Task<OrganizationCustomer?> GetByCustomerId(long customerId, CancellationToken cancellationToken = default);
    }
}