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
	public class UsersController : ControllerBase
	{
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var response = await _userService.GetAllAsync(cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken = default)
        {
            var response = await _userService.GetByIdAsync(id, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpGet("{id}/detail")]
        public async Task<IActionResult> GetDetailById(long id, CancellationToken cancellationToken = default)
        {
            var response = await _userService.GetDetailByIdAsync(id, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto createDto, CancellationToken cancellationToken = default)
        {
            var response = await _userService.CreateAsync(createDto, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UserUpdateDto updateDto, CancellationToken cancellationToken = default)
        {
            var response = await _userService.UpdateAsync(updateDto, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] UserChangePasswordDto chang)
        {
            var res = await _userService.ChangePasswordAsync(chang);
            return ControllerHelper.ReturnResult(res);
        } 

        [HttpPost("toggle-lockout")]
        public async Task<IActionResult> ToggleLockOut([FromBody] long userId)
        {
            var res = await _userService.ToggleLockoutAsync(userId);
            return ControllerHelper.ReturnResult(res);
        }
	}
}
