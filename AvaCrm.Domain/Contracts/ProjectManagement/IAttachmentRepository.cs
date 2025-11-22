namespace AvaCrm.Domain.Contracts.ProjectManagement;

public interface IAttachmentRepository : IGenericRepository<Attachment>
{
    Task<List<Attachment>> GetAttachmentsByTaskId(long taskId);
}
