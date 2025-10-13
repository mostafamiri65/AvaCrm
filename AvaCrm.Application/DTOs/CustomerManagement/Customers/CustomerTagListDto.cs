namespace AvaCrm.Application.DTOs.CustomerManagement.Customers
{
    public class CustomerTagListDto
    {
		public int TagId { get; set; }
        public long CustomerId { get; set; }
        public string TagTitle { get; set; } = string.Empty;
        public string? CustomerCode { get; set; }
	}
}