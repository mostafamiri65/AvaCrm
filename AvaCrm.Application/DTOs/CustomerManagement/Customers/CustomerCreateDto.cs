using AvaCrm.Application.DTOs.CustomerManagement.CustomerAddresses;
using AvaCrm.Application.DTOs.CustomerManagement.IndividualCustomers;
using AvaCrm.Application.DTOs.CustomerManagement.OrganizationCustomers;
using AvaCrm.Domain.Enums.CustomerManagement;

namespace AvaCrm.Application.DTOs.CustomerManagement.Customers;

    public class CustomerCreateDto
    {
		public string Code { get; set; } = string.Empty;
        public CustomerType? CustomerType { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        // Optional: Include creation DTOs for related entities if needed
        public IndividualCustomerCreateDto? IndividualCustomer { get; set; }
        public OrganizationCustomerCreateDto? OrganizationCustomer { get; set; }
        public List<CustomerAddressCreateDto>? CustomerAddresses { get; set; }
        public List<int>? TagIds { get; set; }
        public int TypeOfCustomer { get; set; }
	}
