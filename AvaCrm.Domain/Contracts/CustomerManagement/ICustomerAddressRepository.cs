using AvaCrm.Domain.Entities.CustomerManagement;

namespace AvaCrm.Domain.Contracts.CustomerManagement
{
    public interface ICustomerAddressRepository : IGenericRepository<CustomerAddress>
    {
		Task<List<CustomerAddress>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default);
	}
}