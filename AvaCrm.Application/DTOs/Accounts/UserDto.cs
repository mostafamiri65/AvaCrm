using AvaCrm.Domain.Enums.Accounts;

namespace AvaCrm.Application.DTOs.Accounts;

public class UserDto
{
	public long UserId { get; set; }
	public string? UserName { get; set; }
	public string? Email { get; set; }
	public string? PhoneNumber { get; set; }
	public string? Fullname { get; set; }
	public string? AvatarFile { get; set; }
	public UserGender UserGender { get; set; }
	public long RoleId { get; set; }

}
