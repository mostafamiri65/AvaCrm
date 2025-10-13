namespace AvaCrm.Domain.Readers.Dashboard;

public class DashboardSummaryReader
{
	public int TotalCustomers { get; set; }
	public int RecentFollowUpsCount { get; set; }
	public int RecentInteractionsCount { get; set; }
	public int UpcomingFollowUpsCount { get; set; }
	public int UpcomingInteractionsCount { get; set; }

}
