using AvaCrm.Application.DTOs.CustomerManagement.Dashboard;
using AvaCrm.Domain.Contracts.Dashboard;

namespace AvaCrm.Application.Features.Dashboard
{
    public class DashboardService : IDashboardService
	{
		private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<GlobalResponse<DashboardSummaryDto>> GetDashboardSummaryAsync(long userId, CancellationToken cancellationToken = default)
        {
			try
            {
                var result = await _dashboardRepository.GetDashboardSummaryAsync(userId);
                return new GlobalResponse<DashboardSummaryDto>
                {
                    StatusCode = 200,
                    Message = "Dashboard summary retrieved successfully",
                    Data = new DashboardSummaryDto()
                    {
                        RecentFollowUpsCount = result.RecentFollowUpsCount,
                        RecentInteractionsCount = result.RecentInteractionsCount,
                        TotalCustomers = result.TotalCustomers,
                        UpcomingFollowUpsCount = result.UpcomingFollowUpsCount,
                        UpcomingInteractionsCount = result.UpcomingInteractionsCount
                    }
                };
            }
            catch (Exception ex)
            {
                return new GlobalResponse<DashboardSummaryDto>
                {
                    StatusCode = 500,
                    Message = $"Error retrieving dashboard summary: {ex.Message}"
                };
            }
		}

        public async Task<GlobalResponse<List<UpcomingActivityDto>>> GetUpcomingActivitiesAsync(long userId, int count = 10, CancellationToken cancellationToken = default)
        {
			try
            {
                var result = await _dashboardRepository.GetUpcomingActivitiesAsync(userId,count);
                return new GlobalResponse<List<UpcomingActivityDto>>
                {
                    StatusCode = 200,
                    Message = "Upcoming activities retrieved successfully",
                    Data = result.Select(r=>new UpcomingActivityDto()
                    {
                        ActivityDate = r.ActivityDate,
                        ActivityType = r.ActivityType,
                        CreatedDate = r.CreatedDate,
                        CustomerCode = r.CustomerCode,
                        CustomerId = r.CustomerId,
                        CustomerName = r.CustomerName,
                        Description = r.Description,
                        Id = r.Id,
                        Title = r.Title
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                return new GlobalResponse<List<UpcomingActivityDto>>
                {
                    StatusCode = 500,
                    Message = $"Error retrieving upcoming activities: {ex.Message}"
                };
            }
		}

        public async Task<GlobalResponse<List<RecentActivityDto>>> GetRecentActivitiesAsync(long userId, int days = 7, CancellationToken cancellationToken = default)
        {
			try
			{
                var result = await _dashboardRepository.GetRecentActivitiesAsync(days);
                return new GlobalResponse<List<RecentActivityDto>>
                {
                    StatusCode = 200,
                    Message = "Recent activities retrieved successfully",
                    Data = result.Select(r=> new RecentActivityDto()
                    {
						ActivityDate = r.ActivityDate,
                        ActivityType = r.ActivityType,
                        CustomerCode = r.CustomerCode,
                        CustomerId = r.CustomerId,
                        CustomerName = r.CustomerName,
                        Description = r.Description,
                        Id = r.Id,
                        Title = r.Title
					}).ToList()
                };
            }
            catch (Exception ex)
            {
                return new GlobalResponse<List<RecentActivityDto>>
                {
                    StatusCode = 500,
                    Message = $"Error retrieving recent activities: {ex.Message}"
                };
            }
		}
    }
}