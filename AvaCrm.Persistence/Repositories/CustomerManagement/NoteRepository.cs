using AvaCrm.Domain.Contracts.CustomerManagement;

namespace AvaCrm.Persistence.Repositories.CustomerManagement
{
    public class NoteRepository : GenericRepository<Note>, INoteRepository
    {
        private readonly AvaCrmContext _context;
        public NoteRepository(AvaCrmContext context, AvaCrmContext context1) : base(context)
        {
            _context = context1;
        }

        public async Task<List<Note>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.Notes
                .Where(n => n.CustomerId == customerId && !n.IsDelete)
                .ToListAsync(cancellationToken);
        }
	}
}