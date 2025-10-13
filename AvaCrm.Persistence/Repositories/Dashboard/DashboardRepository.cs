using AvaCrm.Domain.Contracts.Dashboard;
using AvaCrm.Domain.Readers.Dashboard;
using Microsoft.Extensions.Configuration;

namespace AvaCrm.Persistence.Repositories.Dashboard
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AvaCrmContext _context;
        private IConfiguration _configuration;

        public DashboardRepository(AvaCrmContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<DashboardSummaryReader> GetDashboardSummaryAsync(long userId)
        {
			var now = DateTime.Now;
            var oneWeekAgo = now.AddDays(-7);
            var user = await _context.Users.FirstOrDefaultAsync(f => f.Id == userId);
            var adminRoleId = Convert.ToInt64(_configuration.
                GetSection("ApplicationSetting:RoleIdForSeeAll").Value);
			var isAdmin = user != null && user.RoleId == adminRoleId;

            var totalCustomers = await _context.Customers
                .Where(c => !c.IsDelete)
                .ToListAsync();
            if (!isAdmin)
            {
                totalCustomers = totalCustomers.Where(c => c.CreatedBy == userId).ToList();
            }
            var recentFollowUps = await _context.FollowUps
                .Where(f => !f.IsDelete && f.CreationDate >= oneWeekAgo)
                .ToListAsync();
            if (!isAdmin)
            {
                recentFollowUps = recentFollowUps.Where(c => c.CreatedBy == userId).ToList();
            }
            var recentInteractions = await _context.Interactions
                .Where(i => !i.IsDelete && i.CreationDate >= oneWeekAgo)
                .ToListAsync();
            if (!isAdmin)
            {
                recentInteractions = recentInteractions.Where(c => c.CreatedBy == userId).ToList();
            }
			var upcomingFollowUps = await _context.FollowUps
                .Where(f => !f.IsDelete &&
                            f.NextFollowUpDate.HasValue &&
                            f.NextFollowUpDate >= now)
                .ToListAsync();
            if (!isAdmin)
            {
                upcomingFollowUps = upcomingFollowUps.Where(c => c.CreatedBy == userId).ToList();
            }
			var upcomingInteractions = await _context.Interactions
                .Where(i => !i.IsDelete &&
                            i.NextInteraction.HasValue &&
                            i.NextInteraction >= now)
                .ToListAsync();
            if (!isAdmin)
            {
                upcomingInteractions = upcomingInteractions.Where(c => c.CreatedBy == userId).ToList();
            }
			return new DashboardSummaryReader
            {
                TotalCustomers = totalCustomers.Count,
                RecentFollowUpsCount = recentFollowUps.Count,
                RecentInteractionsCount = recentInteractions.Count,
                UpcomingFollowUpsCount = upcomingFollowUps.Count,
                UpcomingInteractionsCount = upcomingInteractions.Count
            };
		}

        public async Task<List<UpcomingActivityReader>> GetUpcomingActivitiesAsync(long userId,int count = 10)
        {
            var user = await _context.Users.FirstOrDefaultAsync(f => f.Id == userId);
            var adminRoleId = Convert.ToInt64(_configuration.
                GetSection("ApplicationSetting:RoleIdForSeeAll").Value);
            var isAdmin = user != null && user.RoleId == adminRoleId;

			var now = DateTime.Now;

			var upcomingFollowUps = await _context.FollowUps
				.Include(f => f.Customer)
				.Where(f => !f.IsDelete &&  
						   f.NextFollowUpDate.HasValue &&
						   f.NextFollowUpDate >= now &&
                           (isAdmin || f.CreatedBy == userId))
				.OrderBy(f => f.NextFollowUpDate)
				.Take(count)
				.Select(f => new UpcomingActivityReader
				{
					Id = f.Id,
					CustomerId = f.CustomerId,
					CustomerName = f.Customer.IndividualCustomer != null ?
						$"{f.Customer.IndividualCustomer.FirstName} {f.Customer.IndividualCustomer.LastName}" :
						f.Customer.OrganizationCustomer!.CompanyName,
					CustomerCode = f.Customer.Code,
					ActivityType = "FollowUp",
					Title = "پیگیری",
					Description = f.Description,
					ActivityDate = f.NextFollowUpDate!.Value,
					CreatedDate = f.CreationDate
				})
				.ToListAsync();

			var upcomingInteractions = await _context.Interactions
				.Include(i => i.Customer)
				.Where(i => !i.IsDelete &&
						   i.NextInteraction.HasValue &&
						   i.NextInteraction >= now &&
                           (isAdmin || i.CreatedBy == userId))
				.OrderBy(i => i.NextInteraction)
				.Take(count)
				.Select(i => new UpcomingActivityReader
				{
					Id = i.Id,
					CustomerId = i.CustomerId,
					CustomerName = i.Customer.IndividualCustomer != null ?
						$"{i.Customer.IndividualCustomer.FirstName} {i.Customer.IndividualCustomer.LastName}" :
						i.Customer.OrganizationCustomer!.CompanyName,
					CustomerCode = i.Customer.Code,
					ActivityType = "Interaction",
					Title = i.Subject,
					Description = i.Description,
					ActivityDate = i.NextInteraction!.Value,
					CreatedDate = i.CreationDate
				})
				.ToListAsync();

			var allActivities = upcomingFollowUps
				.Concat(upcomingInteractions)
				.OrderBy(a => a.ActivityDate)
				.Take(count)
				.ToList();

			return allActivities;
		}

        public async Task<List<RecentActivityReader>> GetRecentActivitiesAsync(long userId, int days = 7)
        {
            var user = await _context.Users.FirstOrDefaultAsync(f => f.Id == userId);
            var adminRoleId = Convert.ToInt64(_configuration.
                GetSection("ApplicationSetting:RoleIdForSeeAll").Value);
            var isAdmin = user != null && user.RoleId == adminRoleId;

			var startDate = DateTime.Now.AddDays(-days);

			var recentFollowUps = await _context.FollowUps
				.Include(f => f.Customer)
				.Where(f => !f.IsDelete && f.CreationDate >= startDate
                &&(isAdmin || f.CreatedBy == userId))
				.OrderByDescending(f => f.CreationDate)
				.Select(f => new RecentActivityReader
				{
					Id = f.Id,
					CustomerId = f.CustomerId,
					CustomerName = f.Customer.IndividualCustomer != null ?
						$"{f.Customer.IndividualCustomer.FirstName} {f.Customer.IndividualCustomer.LastName}" :
						f.Customer.OrganizationCustomer!.CompanyName,
					CustomerCode = f.Customer.Code,
					ActivityType = "FollowUp",
					Title = "پیگیری",
					Description = f.Description,
					ActivityDate = f.CreationDate
				})
				.ToListAsync();

			var recentInteractions = await _context.Interactions
				.Include(i => i.Customer)
				.Where(i => !i.IsDelete && i.CreationDate >= startDate &&(isAdmin || i.CreatedBy == userId))
				.OrderByDescending(i => i.CreationDate)
				.Select(i => new RecentActivityReader
				{
					Id = i.Id,
					CustomerId = i.CustomerId,
					CustomerName = i.Customer.IndividualCustomer != null ?
						$"{i.Customer.IndividualCustomer.FirstName} {i.Customer.IndividualCustomer.LastName}" :
						i.Customer.OrganizationCustomer!.CompanyName,
					CustomerCode = i.Customer.Code,
					ActivityType = "Interaction",
					Title = i.Subject,
					Description = i.Description,
					ActivityDate = i.CreationDate
				})
				.ToListAsync();

			var allActivities = recentFollowUps
				.Concat(recentInteractions)
				.OrderByDescending(a => a.ActivityDate)
				.ToList();

			return allActivities;
		}
	}
}