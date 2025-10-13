using AvaCrm.Domain.Enums.CustomerManagement;

namespace AvaCrm.Application.DTOs.CustomerManagement.Customers
{
    public class CustomerUpdateDto
    {
		public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public CustomerType CustomerType { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
	}
}