namespace AvaCrm.Domain.Entities.ProjectManagement
{
    public class ActivityLog : BaseEntity
    {
        public string Action { get; set; } = string.Empty;
        public string? Details { get; set; }
    }
}