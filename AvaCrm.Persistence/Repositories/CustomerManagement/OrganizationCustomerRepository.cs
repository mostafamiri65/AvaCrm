using AvaCrm.Domain.Contracts.CustomerManagement;

namespace AvaCrm.Persistence.Repositories.CustomerManagement
{
    public class OrganizationCustomerRepository : GenericRepository<OrganizationCustomer>, IOrganizationCustomerRepository
    {
        private readonly AvaCrmContext _context;
        public OrganizationCustomerRepository(AvaCrmContext context) : base(context)
        {
            _context = context;
        }

        public async Task<OrganizationCustomer?> GetByCustomerId(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.OrganizationCustomers
                .FirstOrDefaultAsync(oc => oc.CustomerId == customerId && !oc.IsDelete, cancellationToken);
        }
    }
}