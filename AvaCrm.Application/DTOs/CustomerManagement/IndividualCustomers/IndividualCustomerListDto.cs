namespace AvaCrm.Application.DTOs.CustomerManagement.IndividualCustomers
{
    public class IndividualCustomerListDto
    {
		public long Id { get; set; }
        public long CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public string? Email { get; set; }
        public string? CustomerCode { get; set; }
        public string? PhoneNumber { get; set; }
	}
}