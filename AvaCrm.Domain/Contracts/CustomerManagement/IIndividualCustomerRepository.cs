using AvaCrm.Domain.Entities.CustomerManagement;

namespace AvaCrm.Domain.Contracts.CustomerManagement
{
    public interface IIndividualCustomerRepository : IGenericRepository<IndividualCustomer>
    {
        Task<IndividualCustomer?> GetByCustomerId(long customerId, CancellationToken cancellationToken = default);
    }
}