using AvaCrm.Domain.Contracts.CustomerManagement;

namespace AvaCrm.Persistence.Repositories.CustomerManagement
{
    public class IndividualCustomerRepository : GenericRepository<IndividualCustomer>, IIndividualCustomerRepository
    {
        private readonly AvaCrmContext _context;
        public IndividualCustomerRepository(AvaCrmContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IndividualCustomer?> GetByCustomerId(long customerId, CancellationToken cancellationToken = default)
        {
            return await _context.IndividualCustomers
                .FirstOrDefaultAsync(ic => ic.CustomerId == customerId && !ic.IsDelete, cancellationToken);
        }
	}
}