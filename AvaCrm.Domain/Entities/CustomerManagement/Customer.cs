using AvaCrm.Domain.Enums.CustomerManagement;

namespace AvaCrm.Domain.Entities.CustomerManagement;

public class Customer : BaseEntity
{
	public string Code { get; set; } = string.Empty;
	public CustomerType CustomerType { get; set; }
	public string Email { get; set; } = string.Empty;
	public string PhoneNumber { get; set; } = string.Empty;

	public IndividualCustomer? IndividualCustomer { get; set; }
	public OrganizationCustomer? OrganizationCustomer { get; set; }
	public virtual List<CustomerAddress>? CustomerAddresses { get; set; }
	public virtual List<CustomerTag>? CustomerTags { get; set; }
	public virtual List<FollowUp>? FollowUps { get; set; }
	public virtual List<Note>? Notes { get; set; }
	public virtual List<Interaction>? Interactions { get; set; }
	public virtual List<ContactPerson>? ContactPersons { get; set; }

}
