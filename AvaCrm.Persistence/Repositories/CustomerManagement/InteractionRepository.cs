using AvaCrm.Domain.Contracts.CustomerManagement;
using AvaCrm.Domain.Enums.CustomerManagement;

namespace AvaCrm.Persistence.Repositories.CustomerManagement
{
    public class InteractionRepository : GenericRepository<Interaction>, IInteractionRepository
    {
        private readonly AvaCrmContext _context;
        public InteractionRepository(AvaCrmContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Interaction>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Interactions
                .Where(i => i.CustomerId == customerId && !i.IsDelete)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Interaction>> GetByType(InteractionType interactionType, CancellationToken cancellationToken = default)
        {
            return await _context.Interactions
                .Where(i => i.InteractionType == interactionType && !i.IsDelete)
                .Include(i => i.Customer)
                .ToListAsync(cancellationToken);
        }

	}
}