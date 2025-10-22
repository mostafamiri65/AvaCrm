namespace AvaCrm.Domain.Entities.ProjectManagement
{
    public class Comment : BaseEntity
    {
        public long TaskId { get; set; }
        public string? Content { get; set; }
    }
}