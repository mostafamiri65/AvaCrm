namespace AvaCrm.Domain.Entities.CustomerManagement;

public class ContactPerson : BaseEntity
{
	public long CustomerId { get; set; }
	public string FullName { get; set; } = string.Empty;
	public string JobTitle { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;

	[ForeignKey(nameof(CustomerId))]
	public virtual Customer Customer { get; set; } = null!;
}
