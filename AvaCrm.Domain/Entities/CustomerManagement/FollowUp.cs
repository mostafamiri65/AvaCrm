namespace AvaCrm.Domain.Entities.CustomerManagement;

public class FollowUp : BaseEntity
{
	public long CustomerId { get; set; }
	public string Description { get; set; } = string.Empty;
	public DateTime? NextFollowUpDate { get; set; }
	public string? NextFollowUpDescription { get; set; }

	[ForeignKey(nameof(CustomerId))]
	public virtual Customer Customer { get; set; } = null!;
}
