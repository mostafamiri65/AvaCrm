using AvaCrm.Domain.Enums.ProjectManagement;

namespace AvaCrm.Domain.Entities.ProjectManagement;
public class Project : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public ProjectStatus Status { get; set; }

    public virtual ICollection<UserProject> UserProjects { get; set; } = new List<UserProject>();
}