using AvaCrm.Domain.Enums.Accounts;

namespace AvaCrm.Domain.Entities.Accounts;

public class UserInfo
{
	[Key,ForeignKey("User")]
	public long UserId { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public UserGender UserGender { get; set; }
	public DateTime BirthDate { get; set; }
	public string? Avatar { get; set; }

	public virtual User User { get; set; } = null!;

}

