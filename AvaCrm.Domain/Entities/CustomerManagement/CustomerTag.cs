namespace AvaCrm.Domain.Entities.CustomerManagement;

public class CustomerTag
{
	[Key, Column(Order = 0)]
	public int TagId { get; set; }
	[Key, Column(Order = 1)]
	public long CustomerId { get; set; }

	[ForeignKey("TagId")]
	public virtual Tag Tag { get; set; } = null!;
	[ForeignKey("CustomerId")]
	public virtual Customer Customer { get; set; } = null!;

}
