namespace AvaCrm.Application.DTOs.ProjectManagement;

public class CreateCommentDto
{
    public long TaskId { get; set; }
    public string Content { get; set; } = string.Empty;
}
