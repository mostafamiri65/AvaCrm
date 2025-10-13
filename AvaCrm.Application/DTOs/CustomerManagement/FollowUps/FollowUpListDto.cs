namespace AvaCrm.Application.DTOs.CustomerManagement.FollowUps
{
    public class FollowUpListDto
    {
		public long Id { get; set; }
        public long CustomerId { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime? NextFollowUpDate { get; set; }
        public string? NextFollowUpDescription { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerCode { get; set; } = string.Empty;
	}
}