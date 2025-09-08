using AvaCrm.Application.Contracts.Identity;
using AvaCrm.Application.Models.Identity;
using AvaCrm.Application.Rules.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
	private readonly IAuthService _authService;
	public AccountController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpPost("login")]
	public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
	{
		var res = await _authService.Login(request);
		if (res.LoginState == LoginState.Success)
		{
			return Ok(res);
		}
		else
		{
			return BadRequest(res);
		}
	}
}
