using AvaCrm.Domain.Readers.Dashboard;

namespace AvaCrm.Domain.Contracts.Dashboard;

public interface IDashboardRepository
{
	Task<DashboardSummaryReader> GetDashboardSummaryAsync(long userId);
	Task<List<UpcomingActivityReader>> GetUpcomingActivitiesAsync(long userId,int count = 10);
	Task<List<RecentActivityReader>> GetRecentActivitiesAsync(long userId,int days = 7);
}
