namespace AvaCrm.Domain.Readers.Accounts;


public class UserListDto
{
	public long Id { get; set; }
	public string? Username { get; set; }
	public string? Email { get; set; }
	public long RoleId { get; set; }
	public string? RoleTitlePersian { get; set; }
	public string? RoleTitleEnglish { get; set; }
	public bool EmailConfirmed { get; set; }
	public string? PhoneNumber { get; set; }
	public bool PhoneNumberConfirmed { get; set; }
	public bool LockoutEnabled { get; set; }
	public bool LockoutTotal { get; set; }
	public DateTime CreatedDate { get; set; }
}

public class UserCreateDto
{
	public string? Username { get; set; }
	public string? Email { get; set; }
	public string? Password { get; set; }
	public long RoleId { get; set; }
	public string? PhoneNumber { get; set; }
}

public class UserUpdateDto
{
	public long Id { get; set; }
	public string? Username { get; set; }
	public string? Email { get; set; }
	public long RoleId { get; set; }
	public string? PhoneNumber { get; set; }
	public bool LockoutEnabled { get; set; }
	public bool LockoutTotal { get; set; }
}

public class UserChangePasswordDto
{
	public long UserId { get; set; }
	public string? NewPassword { get; set; }
}

public class UserDetailDto
{
	public long Id { get; set; }
	public string? Username { get; set; }
	public string? Email { get; set; }
	public long RoleId { get; set; }
	public string? RoleTitlePersian { get; set; }
	public string? RoleTitleEnglish { get; set; }
	public bool EmailConfirmed { get; set; }
	public string? PhoneNumber { get; set; }
	public bool PhoneNumberConfirmed { get; set; }
	public bool TwoFactorEnabled { get; set; }
	public bool LockoutEnabled { get; set; }
	public bool LockoutTotal { get; set; }
	public int AccessFailedCount { get; set; }
	public DateTimeOffset? LockoutEnd { get; set; }
	public DateTime CreatedDate { get; set; }
}