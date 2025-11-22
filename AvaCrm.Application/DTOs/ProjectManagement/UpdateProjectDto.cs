using AvaCrm.Domain.Enums.ProjectManagement;

namespace AvaCrm.Application.DTOs.ProjectManagement;

public class UpdateProjectDto
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public List<long> UserIds { get; set; } = new List<long>();
}
