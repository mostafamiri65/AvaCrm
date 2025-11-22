using AvaCrm.Domain.Enums.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaCrm.Application.DTOs.ProjectManagement;

public class CreateProjectDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public List<long> UserIds { get; set; } = new List<long>();
}
