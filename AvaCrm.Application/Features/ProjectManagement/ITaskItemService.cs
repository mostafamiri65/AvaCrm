using AvaCrm.Application.DTOs.ProjectManagement;
using AvaCrm.Application.Pagination;
using AvaCrm.Domain.Enums.ProjectManagement;

namespace AvaCrm.Application.Features.ProjectManagement;

public interface ITaskItemService
{
    Task<GlobalResponse<PaginatedResult<TaskItemDto>>> GetAllTaskItems(PaginationRequest request, long projectId);
    Task<GlobalResponse<TaskItemDto>> GetTaskItem(long taskItemId);
    Task<GlobalResponse<TaskItemDto>> CreateTaskItem(CreateTaskItemDto create, long userId);
    Task<GlobalResponse<TaskItemDto>> UpdateTaskItem(UpdateTaskItemDto update, long userId);
    Task<GlobalResponse<ResponseResultGlobally>> DeleteTaskItem(long taskItemId, long userId);
}
