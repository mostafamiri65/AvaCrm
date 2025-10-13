using AvaCrm.Domain.Enums.CustomerManagement;

namespace AvaCrm.Application.DTOs.CustomerManagement.Interactions
{
    public class InteractionUpdateDto
    {
		public long Id { get; set; }
        public InteractionType InteractionType { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? NextInteraction { get; set; }
	}
}