using AvaCrm.Domain.Enums.ProjectManagement;
using TaskStatus = AvaCrm.Domain.Enums.ProjectManagement.TaskStatus;

namespace AvaCrm.Domain.Entities.ProjectManagement;

public class TaskItem : BaseEntity
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public long ProjectId { get; set; }
    public long AssignedTo { get; set; }
    public TaskPriority Priority { get; set; }
    public TaskStatus Status { get; set; }
    public DateTime? DueDate { get; set; }
}
