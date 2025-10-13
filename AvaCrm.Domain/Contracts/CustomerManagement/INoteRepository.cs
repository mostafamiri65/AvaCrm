using AvaCrm.Domain.Entities.CustomerManagement;

namespace AvaCrm.Domain.Contracts.CustomerManagement
{
    public interface INoteRepository : IGenericRepository<Note>
    {
		Task<List<Note>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default);
	}
}