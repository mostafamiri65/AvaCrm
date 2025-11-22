using AvaCrm.Application.DTOs.ProjectManagement;
using AvaCrm.Application.Pagination;
using AvaCrm.Domain.Contracts.ProjectManagement;
using AvaCrm.Domain.Entities.ProjectManagement;
using AvaCrm.Domain.Enums.ProjectManagement;
using System.Linq.Expressions;
using System.Net;
using System.Net.NetworkInformation;

namespace AvaCrm.Application.Features.ProjectManagement;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IMapper _mapper;
    public ProjectService(
        IProjectRepository projectRepository,
        IActivityLogRepository activityLogRepository,
        IMapper mapper)
    {
        _projectRepository = projectRepository;
        _activityLogRepository = activityLogRepository;
        _mapper = mapper;
    }
    public async Task<GlobalResponse<ProjectDto>> ChangeProjectStatus(long projectId, ProjectStatus status, long userId)
    {
        var project = await _projectRepository.GetById(projectId);
        if (project == null)
        {
            return new GlobalResponse<ProjectDto>()
            {
                Message = "پروژه مورد نظر یافت نشد",
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }
        project.Status = status;
        await _projectRepository.Update(project, userId);
        ActivityLog activityLog = new ActivityLog()
        {
            Action = "تغییر وضعیت",
            Details = $"کاربر با شناسه {userId} وضعیت پروژه را به {status} تغییر داد",
            ProjectId = projectId
        };
        await _activityLogRepository.Create(activityLog, userId);

        return new GlobalResponse<ProjectDto>()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Data = _mapper.Map<ProjectDto>(project),
            Message = "تغییر وضعیت با موفقیت انجام شد"
        };
    }

    public async Task<GlobalResponse<ProjectDto>> CreateProject(CreateProjectDto create, long userId)
    {
        var exist = await _projectRepository.IsExistTitle(create.Title, 0);
        if (exist)
        {
            return new GlobalResponse<ProjectDto>()
            {
                Message = "عنوان پروژه تکراری می باشد",
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        var project = _mapper.Map<Project>(create);
        project.Status = ProjectStatus.Planning;
        var entity = await _projectRepository.Create(project, userId);
        await _projectRepository.ManageUsersInProject(create.UserIds, entity.Id);

        ActivityLog activityLog = new ActivityLog()
        {
            Action = "ایجاد پروژه",
            Details = $"کاربر با شناسه {userId} پروژه را با شناسه {entity.Id} ایجاد کرد",
            ProjectId = entity.Id
        };
        await _activityLogRepository.Create(activityLog, userId);
        return new GlobalResponse<ProjectDto>()
        {
            Message = "پروژه مورد نظر با موفقیت ایجاد شد",
            StatusCode = (int)HttpStatusCode.OK,
            Data = _mapper.Map<ProjectDto>(entity)
        };

    }

    public async Task<GlobalResponse<ResponseResultGlobally>> DeleteProjects(long projectId, long userId)
    {
        var project = await _projectRepository.GetById(projectId);
        if (project == null)
        {
            return new GlobalResponse<ResponseResultGlobally>()
            {
                Message = "پروژه مورد نظر یافت نشد",
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }
        await _projectRepository.Delete(projectId, userId);

        ActivityLog activityLog = new ActivityLog()
        {
            Action = "حذف پروژه",
            Details = $"کاربر با شناسه {userId} پروژه را با شناسه {projectId} حذف کرد",
            ProjectId = projectId
        };
        await _activityLogRepository.Create(activityLog, userId);
        return new GlobalResponse<ResponseResultGlobally>()
        {
            Message = "پروژه مورد نظر با موفقیت حذف شد",
            StatusCode = (int)HttpStatusCode.OK,
        };
    }

    public async Task<GlobalResponse<PaginatedResult<ProjectDto>>> GetAllProject(PaginationRequest request, ProjectStatus projectStatus, long userId)
    {
        var projects = await _projectRepository.GetAllProjects(projectStatus, userId);

        // Apply search filter if search term is provided
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            projects = projects.Where(p =>
                p.Title.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)  
            );
        }

        // Apply sorting
        if (!string.IsNullOrWhiteSpace(request.SortColumn))
        {
            projects = request.SortDirection?.ToLower() == "desc"
                ? projects.OrderByDescending(GetSortProperty(request.SortColumn))
                : projects.OrderBy(GetSortProperty(request.SortColumn));
        }
        else
        {
            // Default sorting
            projects = projects.OrderByDescending(p => p.CreationDate);
        }

        // Get total count before pagination
        var totalCount = projects.Count();

        // Apply pagination
        var paginatedProjects = projects
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToList();

        // Map to DTOs if needed (assuming projects might need conversion to ProjectDto)
        var projectDtos = paginatedProjects.Select(p => _mapper.Map<ProjectDto>(p)).ToList();

        var paginatedResult = new PaginatedResult<ProjectDto>
        {
            Items = projectDtos,
            TotalCount = totalCount,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };

        return new GlobalResponse<PaginatedResult<ProjectDto>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Message = "Projects retrieved successfully",
            Data = paginatedResult
        };
    }

    public async Task<GlobalResponse<ProjectDto>> GetProject(long projectId)
    {
        var project = await _projectRepository.GetById(projectId);
        if (project == null)
        {
            return new GlobalResponse<ProjectDto>()
            {
                Data = null,
                Message = "پروژه مورد نظر یافت نشد",
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }
        return new GlobalResponse<ProjectDto>
        {
            Message = string.Empty,
            Data = _mapper.Map<ProjectDto>(project),
            StatusCode = (int)HttpStatusCode.OK,
        };
    }

    public async Task<GlobalResponse<ProjectDto>> UpdateProject(UpdateProjectDto update, long userId)
    {
       
        var project = await _projectRepository.GetById(update.Id);
        if (project == null)
        {
            return new GlobalResponse<ProjectDto>()
            {
                Message = "پروژه مورد نظر یافت نشد",
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }
 var exist = await _projectRepository.IsExistTitle(update.Title, update.Id);
        if (exist)
        {
            return new GlobalResponse<ProjectDto>()
            {
                Message = "عنوان پروژه تکراری می باشد",
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
        project.Title = update.Title;
        project.Description = update.Description;
        project.StartDate = update.StartDate;
        project.EndDate = update.EndDate;

        await _projectRepository.Update(project, userId);
        await _projectRepository.ManageUsersInProject(update.UserIds, project.Id);

        ActivityLog activityLog = new ActivityLog()
        {
            Action = "ویرایش پروژه",
            Details = $"کاربر با شناسه {userId} پروژه {update.Title} ویرایش کرد",
            ProjectId = project.Id
        };
        await _activityLogRepository.Create(activityLog, userId);

        return new GlobalResponse<ProjectDto>()
        {
            StatusCode = (int)HttpStatusCode.OK,
            Data = _mapper.Map<ProjectDto>(project),
            Message = "ویرایش با موفقیت انجام شد"
        };
    }

    private static Expression<Func<Project, object>> GetSortProperty(string sortColumn)
    {
        return sortColumn?.ToLower() switch
        {
            "name" => p => p.Title,
            "createddate" => p => p.CreationDate,
            "status" => p => p.Status,
            _ => p => p.CreationDate
        };
    }
}
