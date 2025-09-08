using AvaCrm.Application.Models.Identity;
using AvaCrm.Application.Rules.Enums;
using AvaCrm.Domain.Entities.Accounts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AvaCrm.Application.Contracts.Identity;

public class AuthService : IAuthService
{
	private readonly IUserRepository _userRepository;
	private readonly IHashingService _hashingService;
	private readonly JwtSettings _jwtSettings;
	public AuthService(IUserRepository userRepository, IHashingService hashingService, JwtSettings jwtSettings)
	{
		_userRepository = userRepository;
		_hashingService = hashingService;
		_jwtSettings = jwtSettings;
	}

	public async Task<AuthResponse> Login(AuthRequest request)
	{
		AuthResponse response = new AuthResponse();
		var user = await _userRepository.GetUserByUsername(request.Username);
		if (user == null)
		{
			response.LoginState = LoginState.InvalidCredentials;
		}
		else if (user.TwoFactorEnabled)
		{
			response.LoginState = LoginState.RequiresTwoFactor;
		}
		else if (user.LockoutEnd > DateTimeOffset.Now)
		{
			response.LoginState = LoginState.TempararyLockedOut;
		}
		else if (user.LockoutEnabled)
		{
			response.LoginState = LoginState.LockedOut;
		}
		else if (user.PasswordHash != null &&
			!_hashingService.Verify(request.Password, user.PasswordHash))
		{
			response.LoginState = LoginState.InvalidCredentials;
		}
		else
		{
			JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
			response.LoginState = LoginState.Success;
			response.Id = user.Id;
			response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
			response.Email = user.Email;
			response.UserName = user.Username;
			response.PhoneNumber = user.PhoneNumber;
		}

		return response;
	}
	private async Task<JwtSecurityToken> GenerateToken(User user)
	{
		var displayName = !string.IsNullOrWhiteSpace(user.Username)
			? user.Username : !string.IsNullOrWhiteSpace(user.Email)
			? user.Email : !string.IsNullOrWhiteSpace(user.PhoneNumber)
			? user.PhoneNumber : string.Empty;
		var authClaims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, displayName),
			new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Role, user.RoleId.ToString())
		};

		var authSigningKey = new SymmetricSecurityKey(
			Encoding.UTF8.GetBytes(_jwtSettings.Key));

		var token = new JwtSecurityToken(
			issuer: _jwtSettings.Issuer,
			audience: _jwtSettings.Audience,
			expires: DateTime.Now.AddMinutes(_jwtSettings.ExpireMinutes),
			claims: authClaims,
			signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
		);

		return token;
	}

}
