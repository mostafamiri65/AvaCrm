namespace AvaCrm.Application.DTOs.ProjectManagement;

public class UpdateCommentDto
{
    public long Id { get; set; }
    public long TaskId { get; set; }
    public string Content { get; set; } = string.Empty;
}
