using AvaCrm.Application.Rules.Enums;

namespace AvaCrm.Application.Models.Identity;

public class AuthResponse
{
	public LoginState LoginState { get; set; }
	public long Id { get; set; } 
	public string? UserName { get; set; } 
	public string? Email { get; set; } 
	public string? PhoneNumber { get; set; } 
	public string? Token { get; set; } 
}
