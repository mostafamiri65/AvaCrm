namespace AvaCrm.Application.DTOs.CustomerManagement.Notes
{
    public class NoteCreateDto
    {
		public long CustomerId { get; set; }
        public string Content { get; set; } = string.Empty;
	}
}