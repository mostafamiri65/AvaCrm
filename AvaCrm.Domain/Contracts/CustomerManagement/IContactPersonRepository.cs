using AvaCrm.Domain.Entities.CustomerManagement;

namespace AvaCrm.Domain.Contracts.CustomerManagement
{
    public interface IContactPersonRepository : IGenericRepository<ContactPerson>
    {
        Task<List<ContactPerson>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default);
	}
}