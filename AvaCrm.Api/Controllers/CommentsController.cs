using AvaCrm.Application.DTOs.ProjectManagement;
using AvaCrm.Application.Features.ProjectManagement;
using AvaCrm.Application.Pagination;
using AvaCrm.Application.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AvaCrm.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : BaseController
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("task/{taskId}")]
    public async Task<ActionResult<GlobalResponse<PaginatedResult<List<CommentDto>>>>> GetCommentsByTaskId(
        long taskId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var response = await _commentService.GetCommentsByTaskIdAsync(taskId, pageNumber, pageSize, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetComment/{id}")]
    public async Task<ActionResult<GlobalResponse<CommentDto>>> GetCommentById(
        long id, CancellationToken cancellationToken = default)
    {
        var response = await _commentService.GetCommentByIdAsync(id, cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("CreateComment")]
    public async Task<ActionResult<GlobalResponse<ResponseResultGlobally>>> CreateComment(
        [FromBody] CreateCommentDto createCommentDto, CancellationToken cancellationToken = default)
    {
       
        var response = await _commentService.CreateCommentAsync(createCommentDto, GetCurrentUserId(), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("UpdateComment")]
    public async Task<ActionResult<GlobalResponse<ResponseResultGlobally>>> UpdateComment(
        [FromBody] UpdateCommentDto updateCommentDto, CancellationToken cancellationToken = default)
    {
        var response = await _commentService.UpdateCommentAsync(updateCommentDto, GetCurrentUserId(), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("DeleteComment/{id}")]
    public async Task<ActionResult<GlobalResponse<ResponseResultGlobally>>> DeleteComment(
        long id, CancellationToken cancellationToken = default)
    {
         var response = await _commentService.DeleteCommentAsync(id, GetCurrentUserId(), cancellationToken);
        return StatusCode(response.StatusCode, response);
    }
}
