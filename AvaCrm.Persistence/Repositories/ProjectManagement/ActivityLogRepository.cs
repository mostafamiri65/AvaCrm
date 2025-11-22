using AvaCrm.Domain.Contracts.ProjectManagement;
using AvaCrm.Domain.Entities.ProjectManagement;

namespace AvaCrm.Persistence.Repositories.ProjectManagement;

public class ActivityLogRepository : GenericRepository<ActivityLog>, IActivityLogRepository
{
    private readonly AvaCrmContext _context;
    public ActivityLogRepository(AvaCrmContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<ActivityLog>> GetActivityLogsByProjectId(long projectId)
    {
        return await _context.ActivityLogs.Where(a => a.ProjectId == projectId).ToListAsync();
    }

    public async Task<List<ActivityLog>> GetActivityLogsByProjectIdAndUserId(long projectId, long userId)
    {
        return await _context.ActivityLogs.Where(a => a.ProjectId == projectId
        && a.CreatedBy == userId).ToListAsync();
    }
}
