using AvaCrm.Domain.Enums.CustomerManagement;

namespace AvaCrm.Application.DTOs.CustomerManagement.Interactions
{
    public class InteractionListDto
    {
		public long Id { get; set; }
        public long CustomerId { get; set; }
        public InteractionType InteractionType { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? NextInteraction { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerCode { get; set; } = string.Empty;
        public string InteractionTypeName { get; set; } = string.Empty;
	}
}