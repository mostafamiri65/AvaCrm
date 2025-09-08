namespace AvaCrm.Domain.Entities.CustomerManagement
{
	public class Note : BaseEntity
	{
		public long CustomerId { get; set; }
		public string Content { get; set; } = string.Empty;

		[ForeignKey(nameof(CustomerId))]
		public virtual Customer Customer { get; set; } = null!;
	}
}
