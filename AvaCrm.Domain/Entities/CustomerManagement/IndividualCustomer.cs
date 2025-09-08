namespace AvaCrm.Domain.Entities.CustomerManagement;

public class IndividualCustomer : BaseEntity
{
	public long CustomerId { get; set; }
	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;
	public DateTime? BirthDate { get; set; }

	[ForeignKey(nameof(CustomerId))]
	public virtual Customer Customer { get; set; } = null!;
}
