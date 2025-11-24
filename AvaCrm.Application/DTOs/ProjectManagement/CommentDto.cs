namespace AvaCrm.Application.DTOs.ProjectManagement;

public class CommentDto
{
    public long Id { get; set; }
    public DateTime CreationDate { get; set; } 
    public long CreatedBy { get; set; }
    public long TaskId { get; set; }
    public string? Content { get; set; }
}
