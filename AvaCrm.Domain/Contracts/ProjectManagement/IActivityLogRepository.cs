namespace AvaCrm.Domain.Contracts.ProjectManagement;

public interface IActivityLogRepository : IGenericRepository<ActivityLog>
{
    Task<List<ActivityLog>> GetActivityLogsByProjectId(long projectId);
    Task<List<ActivityLog>> GetActivityLogsByProjectIdAndUserId(long projectId,long userId);
}
