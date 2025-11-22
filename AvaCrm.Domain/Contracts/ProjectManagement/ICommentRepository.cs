namespace AvaCrm.Domain.Contracts.ProjectManagement;

public interface ICommentRepository : IGenericRepository<Comment>
{
	Task<List<Comment>> GetCommentsByTaskId(long taskId);
}
