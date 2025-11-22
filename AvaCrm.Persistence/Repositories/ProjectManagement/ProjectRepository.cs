using AvaCrm.Domain.Contracts.ProjectManagement;
using AvaCrm.Domain.Entities.ProjectManagement;
using AvaCrm.Domain.Enums.ProjectManagement;
using Microsoft.Extensions.Configuration;

namespace AvaCrm.Persistence.Repositories.ProjectManagement;

public class ProjectRepository : GenericRepository<Project>, IProjectRepository
{
    private readonly AvaCrmContext _context;
    private readonly IConfiguration _configuration;

    public ProjectRepository(AvaCrmContext context, IConfiguration configuration) : base(context)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<IQueryable<Project>> GetAllProjects(ProjectStatus projectStatus, long userId)
    {
        var user = await _context.Users.FirstOrDefaultAsync(f => f.Id == userId);
        var adminRoleId = Convert.ToInt64(_configuration.
            GetSection("ApplicationSetting:RoleIdForSeeAll").Value);
        if (user == null) return (IQueryable<Project>)(new List<Project>());
        if (projectStatus == ProjectStatus.All)
            return _context.Projects.Where(p => (adminRoleId != user.RoleId ? p.UserProjects.Any(u => u.UserId == userId) : true) &&
        !p.IsDelete);
        return _context.Projects.Where(p => p.Status == projectStatus &&
        (adminRoleId != user.RoleId ? p.UserProjects.Any(u => u.UserId == userId) : true) &&
        !p.IsDelete);
    }

    public async Task<bool> IsExistTitle(string title, long id)
    {
        return await _context.Projects.AnyAsync(p => p.Title == title && p.Id != id);
    }

    public async Task ManageUsersInProject(List<long> userIds, long projectId)
    {
        var users = await _context.UserProjects.Where(p => p.ProjectId == projectId).ToListAsync();
        _context.RemoveRange(users);
        foreach (var user in userIds)
        {
            UserProject userProject = new UserProject()
            {
                ProjectId = projectId,
                UserId = user
            };
            await _context.UserProjects.AddAsync(userProject);
        }
        await _context.SaveChangesAsync();
    }
}