using AvaCrm.Application.DTOs.ProjectManagement;
using AvaCrm.Application.Features.ProjectManagement;
using AvaCrm.Application.Pagination;
using AvaCrm.Domain.Enums.ProjectManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class ProjectsController : BaseController
{
    private readonly IProjectService _projectService;
    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll(int pageSize,int pageNumber ,int statusNumber,string searchTerm ="")
    {
        PaginationRequest request = new PaginationRequest()
        {
            PageSize = pageSize,
            PageNumber = pageNumber,
            SearchTerm = searchTerm
        };
        
        ProjectStatus status = (ProjectStatus)statusNumber;

        var response = await _projectService.GetAllProject(request, status,GetCurrentUserId());
        return StatusCode(response.StatusCode, response);
    }

    [HttpGet("GetById/{projectId}")]
    public async Task<ActionResult> GetById(long projectId)
    {
        var response = await _projectService.GetProject(projectId);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("CreateProject")]
    public async Task<IActionResult> Create(CreateProjectDto create)
    {
        var response = await _projectService.CreateProject(create,GetCurrentUserId());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("UpdateProject")]
    public async Task<IActionResult> Update(UpdateProjectDto update)
    {
        var response = await _projectService.UpdateProject(update, GetCurrentUserId());
        return StatusCode(response.StatusCode, response);
    }

    [HttpPut("ChangeStatus/{projectId}")]
    public async Task<IActionResult> ChangeStatus(ProjectStatus status,long projectId)
    {
        var response = await _projectService.ChangeProjectStatus(projectId,status,GetCurrentUserId());
        return StatusCode(response.StatusCode, response);
    }

    [HttpDelete("DeleteProject")]
    public async Task<IActionResult> Delete(long projectId)
    {
        var response = await _projectService.DeleteProjects(projectId, GetCurrentUserId());
        return StatusCode(response.StatusCode, response);
    }
}
