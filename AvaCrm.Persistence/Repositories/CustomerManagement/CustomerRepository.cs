using AvaCrm.Domain.Contracts.CustomerManagement;
using AvaCrm.Domain.Enums.CustomerManagement;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;

namespace AvaCrm.Persistence.Repositories.CustomerManagement
{
	public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
	{
		private readonly AvaCrmContext _context;
        private IConfiguration _configuration;
		public CustomerRepository(AvaCrmContext context, IConfiguration configuration) : base(context)
        {
            _context = context;
            _configuration = configuration;
        }

		public async Task<IQueryable<Customer>> GetAllCustomers(long userId)
		{
			var user = await _context.Users.FirstOrDefaultAsync(f => f.Id == userId);
            var adminRoleId = Convert.ToInt64(_configuration.
                GetSection("ApplicationSetting:RoleIdForSeeAll").Value);
			if (user == null) return _context.Customers.Where(c => false);

			IQueryable<Customer> customers = _context.Customers.Where(c => !c.IsDelete);
			if (user.RoleId != adminRoleId)
			{
				customers = customers.Where(c => c.CreatedBy == userId);
			}

			customers = customers.Include(c => c.IndividualCustomer)
				.Include(c => c.OrganizationCustomer);

			return customers;
		}

		public async Task<Customer?> GetCustomerWithDetails(long customerId, CancellationToken cancellationToken = default)
		{
			return await _context.Customers
				.Include(c => c.IndividualCustomer)
				.Include(c => c.OrganizationCustomer)
				.Include(c => c.CustomerAddresses)
				.Include(c => c.CustomerTags)!
				.ThenInclude(ct => ct.Tag)
				.Include(c => c.FollowUps)
				.Include(c => c.Notes)
				.Include(c => c.Interactions)
				.Include(c => c.ContactPersons)
				.FirstOrDefaultAsync(c => c.Id == customerId && !c.IsDelete, cancellationToken);
		}

		public async Task<List<Customer>> GetCustomersByType(CustomerType customerType, CancellationToken cancellationToken = default)
		{
			return await _context.Customers
				.Where(c => c.CustomerType == customerType && !c.IsDelete)
				.ToListAsync(cancellationToken);
		}

		public async Task<bool> IsCodeUnique(string code, long? excludeCustomerId = null, CancellationToken cancellationToken = default)
		{
			var query = _context.Customers.Where(c => c.Code == code && !c.IsDelete);

			if (excludeCustomerId.HasValue)
				query = query.Where(c => c.Id != excludeCustomerId.Value);

			return !await query.AnyAsync(cancellationToken);
		}

	}
}