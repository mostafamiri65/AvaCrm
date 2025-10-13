using AvaCrm.Application.DTOs.CustomerManagement.Dashboard;

namespace AvaCrm.Application.Features.Dashboard
{
    public interface IDashboardService
    {
		Task<GlobalResponse<DashboardSummaryDto>> GetDashboardSummaryAsync(long userId,CancellationToken cancellationToken = default);
        Task<GlobalResponse<List<UpcomingActivityDto>>> GetUpcomingActivitiesAsync(long userId, int count = 10, CancellationToken cancellationToken = default);
        Task<GlobalResponse<List<RecentActivityDto>>> GetRecentActivitiesAsync(long userId, int days = 7, CancellationToken cancellationToken = default);
	}
}