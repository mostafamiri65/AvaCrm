using AvaCrm.Domain.Enums.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaCrm.Application.DTOs.ProjectManagement;

public class UpdateTaskItemDto
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public long ProjectId { get; set; }
    public long AssignedTo { get; set; }
    public TaskPriority Priority { get; set; }
    public Domain.Enums.ProjectManagement.TaskStatus Status { get; set; }
    public DateTime? DueDate { get; set; }
}
