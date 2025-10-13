using AvaCrm.Application.Contracts.Identity;
using AvaCrm.MVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AvaCrm.MVC.Controllers;

public class AccountController : BaseController
{
	private readonly IAuthService _authService;
	public AccountController(IAuthService authService)
	{
		_authService = authService;
	}
	public IActionResult Index()
	{
		return View();
	}
	public IActionResult Login(string returnUrl="")
	{
		var result = new LoginDto();
		if (!string.IsNullOrEmpty(returnUrl))
		{
			result.ReturnUrl = returnUrl;
		}
		return View(result);
	}
	[HttpPost("Login")]
	public async Task<IActionResult> Login(LoginDto login)
	{
		if (!ModelState.IsValid)
		{
			return View(login);
		}

		if (!string.IsNullOrEmpty(login.UserName) && !string.IsNullOrEmpty(login.Password))
		{
			var user = await _accountService.GetUserForLogin(login.UserName, login.Password);
			if (user == null)
			{
				var result = new LoginDto();
				if (!string.IsNullOrEmpty(login.ReturnUrl))
				{
					result.ReturnUrl = login.ReturnUrl;
				}
				TempData[ErrorMessage] = "اطلاعات وارده اشتباه است";
				return View(login);
			}

			if (user.Username != null)
			{
				var claims = new List<Claim>
					{
						new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),


					};
				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				var principal = new ClaimsPrincipal(identity);

				var properties = new AuthenticationProperties
				{
					IsPersistent = login.RememberMe
				};

				await HttpContext.SignInAsync(principal, properties);
			}
		}

		if (!string.IsNullOrEmpty(login.ReturnUrl))
		{
			return Redirect(login.ReturnUrl);
		}
		return Redirect("/");
	}
}
