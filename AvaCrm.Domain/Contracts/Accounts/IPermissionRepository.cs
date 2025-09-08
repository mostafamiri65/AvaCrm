namespace AvaCrm.Domain.Contracts.Accounts;

public interface IPermissionRepository
{
	IQueryable<RolePermission> GetRolePermissions(long roleId);
	Task<bool> AddRolePermission(RolePermission rolePermission);
	Task<bool> AddRolePermissions(List<int> permissionIds, long roleId);
	Task<bool> DeleteRolePermission(int permissionId,long roleId);

}
