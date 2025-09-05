namespace AvaCrm.Persistence.Data;

public class AvaCrmContext : DbContext
{
	public AvaCrmContext(DbContextOptions<AvaCrmContext> options) : base(options)
	{

	}
	#region DbSets
	public virtual DbSet<User> Users { get; set; }
	public virtual DbSet<UserInfo> UserInfos { get; set; }
	public virtual DbSet<Role> Roles { get; set; }
	public virtual DbSet<Permission> Permissions { get; set; }
	public virtual DbSet<RolePermission> RolePermissions { get; set; }
	#endregion
}
