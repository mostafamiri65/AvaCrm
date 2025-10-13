using AvaCrm.Application.DTOs.CustomerManagement.ContactPersons;
using AvaCrm.Application.DTOs.CustomerManagement.CustomerAddresses;
using AvaCrm.Application.DTOs.CustomerManagement.FollowUps;
using AvaCrm.Application.DTOs.CustomerManagement.IndividualCustomers;
using AvaCrm.Application.DTOs.CustomerManagement.Interactions;
using AvaCrm.Application.DTOs.CustomerManagement.Notes;
using AvaCrm.Application.DTOs.CustomerManagement.OrganizationCustomers;
using AvaCrm.Domain.Enums.CustomerManagement;

namespace AvaCrm.Application.DTOs.CustomerManagement.Customers
{
    public class CustomerDetailDto
    {
		public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public CustomerType CustomerType { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        public IndividualCustomerListDto? IndividualCustomer { get; set; }
        public OrganizationCustomerListDto? OrganizationCustomer { get; set; }
        public List<CustomerAddressListDto>? CustomerAddresses { get; set; }
        public List<CustomerTagListDto>? CustomerTags { get; set; }
        public List<FollowUpListDto>? FollowUps { get; set; }
        public List<NoteListDto>? Notes { get; set; }
        public List<InteractionListDto>? Interactions { get; set; }
        public List<ContactPersonListDto>? ContactPersons { get; set; }
	}
}