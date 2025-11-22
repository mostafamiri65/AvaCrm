using AvaCrm.Domain.Contracts.ProjectManagement;
using AvaCrm.Domain.Entities.ProjectManagement;

namespace AvaCrm.Persistence.Repositories.ProjectManagement;

public class TaskItemRepository : GenericRepository<TaskItem>, ITaskItemRepository
{
    private readonly AvaCrmContext _context;

    public TaskItemRepository(AvaCrmContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<TaskItem> GetTaskByProjectId(long projectId)
    {
        return _context.TaskItems.Where(t => t.ProjectId == projectId);
    }
}