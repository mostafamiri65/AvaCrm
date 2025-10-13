namespace AvaCrm.Application.DTOs.CustomerManagement.FollowUps
{
    public class FollowUpUpdateDto
    {
		public long Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime? NextFollowUpDate { get; set; }
        public string? NextFollowUpDescription { get; set; }
	}
}