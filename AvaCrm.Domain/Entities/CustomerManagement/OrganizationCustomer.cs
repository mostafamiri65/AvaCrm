namespace AvaCrm.Domain.Entities.CustomerManagement;

public class OrganizationCustomer: BaseEntity
{
	public long CustomerId { get; set; }
	public string CompanyName { get; set; } = string.Empty;
	public string RegistrationNumber { get; set; } = string.Empty;
	public string Industry { get; set; } = string.Empty;

	[ForeignKey(nameof(CustomerId))]
	public virtual Customer Customer { get; set; } = null!;
}
