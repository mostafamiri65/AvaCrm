using AvaCrm.Application.DTOs.Accounts;
using AvaCrm.Application.Rules.Enums;

namespace AvaCrm.Application.Models.Identity;

public class AuthResponse
{
	public LoginState LoginState { get; set; }
    public string? Message { get; set; }
    public UserDto?	User { get; set; }
	public string? Token { get; set; } 

}
