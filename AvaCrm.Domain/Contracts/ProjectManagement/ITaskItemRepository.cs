namespace AvaCrm.Domain.Contracts.ProjectManagement;

public interface ITaskItemRepository : IGenericRepository<TaskItem>
{
	IQueryable<TaskItem> GetTaskByProjectId(long  projectId);

}