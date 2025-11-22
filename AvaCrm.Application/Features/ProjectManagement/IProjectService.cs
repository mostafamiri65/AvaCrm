using AvaCrm.Application.DTOs.ProjectManagement;
using AvaCrm.Application.Pagination;
using AvaCrm.Domain.Enums.ProjectManagement;

namespace AvaCrm.Application.Features.ProjectManagement;

public interface IProjectService
{
    Task<GlobalResponse<PaginatedResult<ProjectDto>>> GetAllProject(PaginationRequest request, ProjectStatus projectStatus, long userId);
    Task<GlobalResponse<ProjectDto>> GetProject(long projectId);
    Task<GlobalResponse<ProjectDto>> CreateProject(CreateProjectDto create,long userId);
    Task<GlobalResponse<ProjectDto>> UpdateProject(UpdateProjectDto update,long userId);
    Task<GlobalResponse<ResponseResultGlobally>> DeleteProjects(long projectId, long userId);
    Task<GlobalResponse<ProjectDto>> ChangeProjectStatus(long projectId, ProjectStatus status,long userId);
}
