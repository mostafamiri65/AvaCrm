using AvaCrm.Domain.Enums.CustomerManagement;

namespace AvaCrm.Application.DTOs.CustomerManagement.Interactions
{
    public class InteractionCreateDto
    {
		public long CustomerId { get; set; }
        public InteractionType InteractionType { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? NextInteraction { get; set; }
	}
}