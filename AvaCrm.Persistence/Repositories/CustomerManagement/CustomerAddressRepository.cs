using AvaCrm.Domain.Contracts.CustomerManagement;

namespace AvaCrm.Persistence.Repositories.CustomerManagement
{
    public class CustomerAddressRepository : GenericRepository<CustomerAddress>, ICustomerAddressRepository
    {
        private readonly AvaCrmContext _context;
        public CustomerAddressRepository(AvaCrmContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<CustomerAddress>> GetByCustomerId(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.CustomerAddresses
                .Where(ca => ca.CustomerId == customerId && !ca.IsDelete)
                .Include(ca => ca.Country)
                .Include(ca => ca.Province)
                .Include(ca => ca.City)
                .ToListAsync(cancellationToken);
        }
	}
}