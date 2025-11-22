using AvaCrm.Application.DTOs.ProjectManagement;
using AvaCrm.Application.Features.ProjectManagement;
using AvaCrm.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class TaskItemsController : BaseController
{
    private readonly ITaskItemService _taskItemService;
    public TaskItemsController(ITaskItemService taskItemService)
    {
        _taskItemService = taskItemService;
    }

    [HttpGet("GetAllTasks")]
    public async Task<ActionResult> GetAllTask(long projectId, int pageSize, int pageNumber)
    {
        PaginationRequest request = new PaginationRequest()
        {
            PageSize = pageSize,
            PageNumber = pageNumber
        };
        var res = await _taskItemService.GetAllTaskItems(request, projectId);
        return StatusCode(res.StatusCode, res);
    }

    [HttpGet("GetTask/{taskItemId}")]
    public async Task<ActionResult> GetTask(long taskItemId)
    {
        var res = await _taskItemService.GetTaskItem(taskItemId);
        return StatusCode(res.StatusCode, res);
    }

    [HttpPost("CreateTask")]
    public async Task<ActionResult> CreateTask(CreateTaskItemDto create)
    {
        var res = await _taskItemService.CreateTaskItem(create,GetCurrentUserId());
        return StatusCode(res.StatusCode, res);
    }

    [HttpPut("UpdateTask")]
    public async Task<ActionResult> UpdateTask(UpdateTaskItemDto update) {

        var res = await _taskItemService.UpdateTaskItem(update, GetCurrentUserId());
        return StatusCode(res.StatusCode, res);
    }

    [HttpDelete("DeleteTask/{taskItemId}")]
    public async Task<ActionResult> DeleteTask(long taskItemId)
    {
        var res = await _taskItemService.DeleteTaskItem(taskItemId,GetCurrentUserId());
        return StatusCode(res.StatusCode, res);
    }
}
