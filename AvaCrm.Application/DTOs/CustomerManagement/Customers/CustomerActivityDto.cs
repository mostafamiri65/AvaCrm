namespace AvaCrm.Application.DTOs.CustomerManagement.Customers
{
	public class CustomerActivityDto
    {
        public long Id { get; set; }
        public string ActivityType { get; set; } = string.Empty; // "Interaction", "Note", "Order", "Ticket"
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}