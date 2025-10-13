using AvaCrm.Domain.Entities.CustomerManagement;
using AvaCrm.Domain.Enums.CustomerManagement;

namespace AvaCrm.Domain.Contracts.CustomerManagement;

public interface ICustomerRepository : IGenericRepository<Customer>
{
    Task<IQueryable<Customer>> GetAllCustomers(long userId);
	Task<Customer?> GetCustomerWithDetails(long customerId, CancellationToken cancellationToken = default);
	Task<List<Customer>> GetCustomersByType(CustomerType customerType, CancellationToken cancellationToken = default);
	Task<bool> IsCodeUnique(string code, long? excludeCustomerId = null, CancellationToken cancellationToken = default);
}
