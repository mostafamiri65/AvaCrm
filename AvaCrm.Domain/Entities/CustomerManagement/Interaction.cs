using AvaCrm.Domain.Enums.CustomerManagement;

namespace AvaCrm.Domain.Entities.CustomerManagement;

public class Interaction : BaseEntity
{
	public long CustomerId { get; set; }
	public InteractionType InteractionType { get; set; }
	public string Subject { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public DateTime? NextInteraction { get; set; }

	[ForeignKey(nameof(CustomerId))]
	public virtual Customer Customer { get; set; } = null!;
}
