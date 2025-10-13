namespace AvaCrm.Application.DTOs.CustomerManagement.Notes
{
    public class NoteListDto
    {
		public long Id { get; set; }
        public long CustomerId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string CustomerCode { get; set; } = string.Empty;
	}
}