namespace AvaCrm.Application.DTOs.CustomerManagement.IndividualCustomers
{
    public class IndividualCustomerCreateDto
    {
		public long CustomerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
	}
}