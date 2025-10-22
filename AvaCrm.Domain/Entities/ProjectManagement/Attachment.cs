namespace AvaCrm.Domain.Entities.ProjectManagement;

public class Attachment : BaseEntity
{
	public long TaskId { get; set; }
	public string FileName { get; set; } = string.Empty;
	public string FilePath { get; set; } = string.Empty;
}
