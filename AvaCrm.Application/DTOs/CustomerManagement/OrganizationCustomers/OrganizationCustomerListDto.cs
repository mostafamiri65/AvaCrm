namespace AvaCrm.Application.DTOs.CustomerManagement.OrganizationCustomers
{
    public class OrganizationCustomerListDto
    {
		public long Id { get; set; }
        public long CustomerId { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string Industry { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? CustomerCode { get; set; }
        public string? PhoneNumber { get; set; }
	}
}