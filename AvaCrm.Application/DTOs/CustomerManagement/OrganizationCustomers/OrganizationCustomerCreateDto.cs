namespace AvaCrm.Application.DTOs.CustomerManagement.OrganizationCustomers
{
    public class OrganizationCustomerCreateDto
    {
		public long CustomerId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
	}
}