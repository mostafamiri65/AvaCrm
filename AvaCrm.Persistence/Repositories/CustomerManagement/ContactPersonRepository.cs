using AvaCrm.Domain.Contracts.CustomerManagement;

namespace AvaCrm.Persistence.Repositories.CustomerManagement
{
    public class ContactPersonRepository : GenericRepository<ContactPerson>, IContactPersonRepository
    {
        private readonly AvaCrmContext _context;
        public ContactPersonRepository(AvaCrmContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ContactPerson>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.ContactPeople
                .Where(cp => cp.CustomerId == customerId && !cp.IsDelete)
                .ToListAsync(cancellationToken);
        }
	}
}