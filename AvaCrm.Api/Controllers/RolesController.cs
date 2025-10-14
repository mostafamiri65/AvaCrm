using AvaCrm.Api.Helper;
using AvaCrm.Application.DTOs.Accounts;
using AvaCrm.Application.Features.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class RolesController : ControllerBase
	{
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var response = await _roleService.GetAllAsync(cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken = default)
        {
            var response = await _roleService.GetByIdAsync(id, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleCreateDto createDto, CancellationToken cancellationToken = default)
        {
            var response = await _roleService.CreateAsync(createDto, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] RoleUpdateDto updateDto, CancellationToken cancellationToken = default)
        {
            var response = await _roleService.UpdateAsync(updateDto, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
        {
            var response = await _roleService.DeleteAsync(id, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }
	}
}
