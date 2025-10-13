namespace AvaCrm.Application.DTOs.CustomerManagement.FollowUps
{
    public class FollowUpCreateDto
    {
		public long CustomerId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime? NextFollowUpDate { get; set; }
        public string? NextFollowUpDescription { get; set; }
	}
}