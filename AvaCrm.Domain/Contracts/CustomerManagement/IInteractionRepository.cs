using AvaCrm.Domain.Entities.CustomerManagement;
using AvaCrm.Domain.Enums.CustomerManagement;

namespace AvaCrm.Domain.Contracts.CustomerManagement
{
    public interface IInteractionRepository : IGenericRepository<Interaction>
    {
		Task<List<Interaction>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default);
        Task<List<Interaction>> GetByType(InteractionType interactionType, CancellationToken cancellationToken = default);
	}
}