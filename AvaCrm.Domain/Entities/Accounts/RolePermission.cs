namespace AvaCrm.Domain.Entities.Accounts;

public class RolePermission
{
	[Key, Column(Order = 0)]
	public long RoleId { get; set; }
	[Key, Column(Order = 1)]
	public int PermissionId { get; set; }
}
