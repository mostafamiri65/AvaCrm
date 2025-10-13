namespace AvaCrm.Application.DTOs.CustomerManagement.Dashboard;

public class DashboardSummaryDto
{
	public int TotalCustomers { get; set; }
	public int RecentFollowUpsCount { get; set; }
	public int RecentInteractionsCount { get; set; }
	public int UpcomingFollowUpsCount { get; set; }
	public int UpcomingInteractionsCount { get; set; }

}
