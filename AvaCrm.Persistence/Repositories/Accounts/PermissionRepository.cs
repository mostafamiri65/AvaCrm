
namespace AvaCrm.Persistence.Repositories.Accounts;

public class PermissionRepository : IPermissionRepository
{
	private readonly AvaCrmContext _context;
	public PermissionRepository(AvaCrmContext context)
	{
		_context = context;
	}

	public async Task<bool> AddRolePermission(RolePermission rolePermission)
	{
		if (await _context.RolePermissions.AnyAsync(r => r == rolePermission))
			return false;

		await _context.RolePermissions.AddAsync(rolePermission);
		await _context.SaveChangesAsync();

		return true;
	}

	public async Task<bool> AddRolePermissions(List<int> permissionIds, long roleId)
	{
		if (permissionIds == null || !permissionIds.Any())
			return false;

		var existing = await _context.RolePermissions
			.Where(rp => rp.RoleId == roleId && permissionIds.Contains(rp.PermissionId))
			.Select(rp => rp.PermissionId)
			.ToListAsync();

		var newPermissions = permissionIds
			.Except(existing)
			.Select(pid => new RolePermission
			{
				RoleId = roleId,
				PermissionId = pid
			})
			.ToList();

		if (!newPermissions.Any())
			return false;

		await _context.RolePermissions.AddRangeAsync(newPermissions);
		await _context.SaveChangesAsync();

		return true;
	}

	public async Task<bool> DeleteRolePermission(int permissionId, long roleId)
	{
		var entity = await _context.RolePermissions.FirstOrDefaultAsync(r => r.RoleId == roleId
		&& r.PermissionId == permissionId);
		if (entity == null)
			return false;

		_context.RolePermissions.Remove(entity);
		await _context.SaveChangesAsync();
		return true;
	}

	public IQueryable<RolePermission> GetRolePermissions(long roleId)
	{
		return _context.RolePermissions.Include(r => r.Role)
				.Include(r => r.Permission).Where(r => r.RoleId == roleId);
	}
}
