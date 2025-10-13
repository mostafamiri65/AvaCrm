using AvaCrm.Domain.Contracts.CustomerManagement;

namespace AvaCrm.Persistence.Repositories.CustomerManagement
{
    public class FollowUpRepository : GenericRepository<FollowUp>, IFollowUpRepository
    {
        private readonly AvaCrmContext _context;
        public FollowUpRepository(AvaCrmContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<FollowUp>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.FollowUps
                .Where(f => f.CustomerId == customerId && !f.IsDelete)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<FollowUp>> GetUpcomingFollowUps(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default)
        {
            return await _context.FollowUps
                .Where(f => f.NextFollowUpDate >= fromDate &&
                            f.NextFollowUpDate <= toDate &&
                            !f.IsDelete)
                .Include(f => f.Customer)
                .ToListAsync(cancellationToken);
        }

	}
}