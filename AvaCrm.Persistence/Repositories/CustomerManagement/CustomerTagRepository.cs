using AvaCrm.Domain.Contracts.CustomerManagement;

namespace AvaCrm.Persistence.Repositories.CustomerManagement
{
    public class CustomerTagRepository : ICustomerTagRepository
    {
        private readonly AvaCrmContext _context;
        private readonly DbSet<CustomerTag> _dbSet;
		public CustomerTagRepository(AvaCrmContext context)
        {
            _context = context;
            _dbSet = _context.CustomerTags;
        }

        public async Task<List<CustomerTag>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(ct => ct.CustomerId == customerId)
                .Include(ct => ct.Tag)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<CustomerTag>> GetByTagId(int tagId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .Where(ct => ct.TagId == tagId)
                .Include(ct => ct.Customer)
                .ToListAsync(cancellationToken);
        }

        public async Task<CustomerTag?> GetByCustomerAndTag(long customerId, int tagId, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(ct => ct.CustomerId == customerId && ct.TagId == tagId, cancellationToken);
        }

        public async Task<bool> CreateCustomerTag(CustomerTag customerTag)
        {
            if (await _dbSet.AnyAsync(d=>d.CustomerId == customerTag.CustomerId &&
                                         d.TagId == customerTag.TagId))
            {
                return false;
            }

            await _dbSet.AddAsync(customerTag);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveCustomerTag(CustomerTag customerTag)
        {
            _dbSet.Remove(customerTag);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}