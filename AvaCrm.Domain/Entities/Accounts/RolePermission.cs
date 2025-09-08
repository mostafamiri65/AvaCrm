namespace AvaCrm.Domain.Entities.Accounts;

public class RolePermission
{
	[Key, Column(Order = 0)]
	public long RoleId { get; set; }
	[Key, Column(Order = 1)]
	public int PermissionId { get; set; }

	[ForeignKey(nameof(RoleId))]
	public virtual Role Role { get; set; } = null!;
	[ForeignKey(nameof(PermissionId))]
	public virtual Permission Permission { get; set; } = null!;
}
