namespace AvaCrm.Domain.Entities.Accounts;

public class User : BaseEntity
{
	public string? Username { get; set; }
	public string? Email { get; set; }
	public string? SentCode { get; set; }
	public long RoleId { get; set; }
	public bool EmailConfirmed { get; set; }
	public string? PasswordHash { get; set; }
	public string? SecurityStamp { get; set; }
	public string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
	public string? PhoneNumber { get; set; }
	public bool PhoneNumberConfirmed { get; set; }
	public bool TwoFactorEnabled { get; set; } = false;
	public DateTimeOffset? LockoutEnd { get; set; }  
	public bool LockoutEnabled { get; set; }         
	public int AccessFailedCount { get; set; }
	public bool LockoutTotal { get; set; } = false;

	#region Relations
	public virtual UserInfo? UserInfo { get; set; }
	[ForeignKey("RoleId")]
	public virtual Role Role { get; set; } = null!;
	#endregion
}
