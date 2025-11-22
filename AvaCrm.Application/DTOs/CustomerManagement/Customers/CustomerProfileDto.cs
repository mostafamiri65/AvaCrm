namespace AvaCrm.Application.DTOs.CustomerManagement.Customers
{
	public class CustomerProfileDto
	{
		public CustomerDetailDto? CustomerDetail { get; set; }

		// آمار کلی
		public int TotalInteractions { get; set; }
		public int TotalNotes { get; set; }
		public int TotalOrders { get; set; }
		public decimal TotalOrderValue { get; set; }
		public int OpenTickets { get; set; }

		// آخرین فعالیت‌ها
		public DateTime? LastInteractionDate { get; set; }
		public string? LastInteractionType { get; set; }
	}
}