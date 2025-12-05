using AvaCrm.Application.DTOs.ProjectManagement;
using AvaCrm.Application.Pagination;
using AvaCrm.Domain.Contracts.ProjectManagement;
using AvaCrm.Domain.Entities.Accounts;
using AvaCrm.Domain.Entities.ProjectManagement;
using AvaCrm.Domain.Enums.ProjectManagement;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Net;
using System.Net.NetworkInformation;

namespace AvaCrm.Application.Features.ProjectManagement;

public class TaskItemService : ITaskItemService
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    private readonly IMapper _mapper;
    public TaskItemService(ITaskItemRepository taskItemRepository,
        IMapper mapper, IActivityLogRepository activityLogRepository)
    {
        _taskItemRepository = taskItemRepository;
        _mapper = mapper;
        _activityLogRepository = activityLogRepository;
    }
    public async Task<GlobalResponse<TaskItemDto>> CreateTaskItem(CreateTaskItemDto create, long userId)
    {
        try
        {
            var task = _mapper.Map<TaskItem>(create);
            var entity = await _taskItemRepository.Create(task, userId);
            ActivityLog activityLog = new ActivityLog()
            {
                Action = "اضافه کردن تسک",
                Details = $"کاربر با شناسه {userId} به پروژه تسکی با شناسه {entity.Id} اضافه کرد",
                ProjectId = create.ProjectId
            };
            await _activityLogRepository.Create(activityLog, userId);

            return new GlobalResponse<TaskItemDto>()
            {
                Data = _mapper.Map<TaskItemDto>(entity),
                Message = "تسک مورد نظر با موفقیت اضافه شد",
                StatusCode = 200
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<TaskItemDto>()
            {
                StatusCode = 500,
                Message = "خطا در اضافه کردن تسک :" + ex.Message
            };
        }
    }

    public async Task<GlobalResponse<ResponseResultGlobally>> DeleteTaskItem(long taskItemId, long userId)
    {
        try
        {
            var task = await _taskItemRepository.GetById(taskItemId);
            if (task == null)
            {
                return new GlobalResponse<ResponseResultGlobally>()
                {
                    Message = "تسکی یافت نشد",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
            await _taskItemRepository.Delete(taskItemId, userId);
            ActivityLog activityLog = new ActivityLog()
            {
                Action = "حذف کردن تسک",
                Details = $"کاربر با شناسه {userId} از پروژه تسک {taskItemId} با شناسه {task.ProjectId} حذف کرد",
                ProjectId = task.ProjectId
            };
            await _activityLogRepository.Create(activityLog, userId);

            return new GlobalResponse<ResponseResultGlobally>()
            {
                Message = "حذف انجام شد",
                StatusCode = 200
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<ResponseResultGlobally>()
            {
                Message = $"خطا در حذف : {ex.Message}",
                StatusCode = 500
            };
        }
    }

    public async Task<GlobalResponse<PaginatedResult<TaskItemDto>>> GetAllTaskItems(PaginationRequest request, long projectId)
    {
        try
        {
            var tasks = _taskItemRepository.GetTaskByProjectId(projectId);

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                tasks = tasks.Where(p => p.Title != null &&
                    p.Title.Contains(request.SearchTerm, StringComparison.OrdinalIgnoreCase)
                );
            }

            // Apply sorting
            if (!string.IsNullOrWhiteSpace(request.SortColumn))
            {
                tasks = request.SortDirection?.ToLower() == "desc"
                    ? tasks.OrderByDescending(GetSortProperty(request.SortColumn))
                    : tasks.OrderBy(GetSortProperty(request.SortColumn));
            }
            else
            {
                // Default sorting
                tasks = tasks.OrderByDescending(p => p.CreationDate);
            }

            // Get total count before pagination
            var totalCount = await tasks.CountAsync();

            // Apply pagination
            var paginatedProjects = await tasks
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            // Map to DTOs if needed (assuming projects might need conversion to ProjectDto)
            var projectDtos = paginatedProjects.Select(p => _mapper.Map<TaskItemDto>(p)).ToList();

            var paginatedResult = new PaginatedResult<TaskItemDto>
            {
                Items = projectDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            return new GlobalResponse<PaginatedResult<TaskItemDto>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Task Item retrieved successfully",
                Data = paginatedResult
            };

        }
        catch (Exception ex)
        {
            return new GlobalResponse<PaginatedResult<TaskItemDto>>()
            {
                Message = $"خطا : {ex.Message}",
                StatusCode = (int)HttpStatusCode.BadRequest,
            };
        }

    }

    public async Task<GlobalResponse<TaskItemDto>> GetTaskItem(long taskItemId)
    {
        try
        {
            var task = await _taskItemRepository.GetById(taskItemId);
            return new GlobalResponse<TaskItemDto>()
            {
                StatusCode = (int)HttpStatusCode.OK,
                Data = _mapper.Map<TaskItemDto>(task),
                Message = ""
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<TaskItemDto>()
            {
                Message = $"خطا : {ex.Message}",
                StatusCode = (int)HttpStatusCode.BadRequest,
            };
        }
    }

    public async Task<GlobalResponse<TaskItemDto>> UpdateTaskItem(UpdateTaskItemDto update, long userId)
    {
        GlobalResponse<TaskItemDto> res = new GlobalResponse<TaskItemDto>();
        try
        {
            var task = await _taskItemRepository.GetById(update.Id);
            if (task == null)
            {
                return new GlobalResponse<TaskItemDto>
                {
                    Message = "موردی برای ویرایش یافت نشد",
                    StatusCode = (int)HttpStatusCode.NotFound
                };
            }
            task.Status = update.Status;
            task.DueDate = update.DueDate;
            task.Title = update.Title;
            task.Description = update.Description;
            task.AssignedTo = update.AssignedTo;
            task.Priority = update.Priority;
            await _taskItemRepository.Update(task, userId);

            ActivityLog activityLog = new ActivityLog()
            {
                Action = "ویرایش کردن تسک",
                Details = $"کاربر با شناسه {userId}  تسکی با شناسه {task.ProjectId} را ویرایش کرد",
                ProjectId = task.ProjectId
            };
            await _activityLogRepository.Create(activityLog, userId);
            res.Message = "ویرایش با موفقیت انجام شد";
            res.StatusCode = (int)HttpStatusCode.OK;
            res.Data = _mapper.Map<TaskItemDto>(task);
        }
        catch (Exception)
        {
            res.Message = "ویرایش با خطا مواجه شد";
            res.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
        return res;

    }

    private static Expression<Func<TaskItem, object>> GetSortProperty(string sortColumn)
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
