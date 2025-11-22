using AvaCrm.Domain.Enums.ProjectManagement;

namespace AvaCrm.Domain.Contracts.ProjectManagement;

public interface IProjectRepository : IGenericRepository<Project>
{
    Task<IQueryable<Project>> GetAllProjects(ProjectStatus projectStatus, long userId);
    Task<bool> IsExistTitle(string title, long id);

    Task ManageUsersInProject(List<long> userIds, long projectId);

}
