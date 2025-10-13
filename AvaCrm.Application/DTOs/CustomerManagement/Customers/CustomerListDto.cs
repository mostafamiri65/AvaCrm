using AvaCrm.Domain.Enums.CustomerManagement;

namespace AvaCrm.Application.DTOs.CustomerManagement.Customers
{
    public class CustomerListDto
    {
		public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public CustomerType CustomerType { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? CustomerName { get; set; }
        public string? CompanyName { get; set; }
        public List<string>? Tags { get; set; }
        public int TypeOfCustomer { get; set; }
	}
}