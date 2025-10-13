namespace AvaCrm.Domain.Readers.Dashboard;

public class UpcomingActivityReader
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerCode { get; set; } = string.Empty;
    public string ActivityType { get; set; } = string.Empty; // "FollowUp" or "Interaction"
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime ActivityDate { get; set; }
    public DateTime CreatedDate { get; set; }
}
