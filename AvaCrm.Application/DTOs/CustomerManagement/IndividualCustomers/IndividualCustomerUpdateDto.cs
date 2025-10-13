namespace AvaCrm.Application.DTOs.CustomerManagement.IndividualCustomers
{
    public class IndividualCustomerUpdateDto
    {
		public long Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
	}
}