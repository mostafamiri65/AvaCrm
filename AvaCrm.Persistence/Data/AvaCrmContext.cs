using System.Security.Cryptography;
using System.Text;

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
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		// --- Seed Role ---
		var adminRoleId = 1L;
		modelBuilder.Entity<Role>().HasData(new Role
		{
			Id = adminRoleId,
			TitleEnglish = "Admin",
			TitlePersian = "مدیر",
			CreationDate = DateTime.Now,
			CreatedBy = 0,
			ModifiedDate = DateTime.Now,
			ModifiedBy = 0,
			IsDelete = false
		});

		// --- Seed User ---
		var adminUserId = 1L;
		var email = "admin@avacrm.com";

		string password = "Admin@123";
		string passwordHash = ComputeSha256Hash(password);

		modelBuilder.Entity<User>().HasData(new User
		{
			Id = adminUserId,
			Username = "admin",
			Email = email,
			EmailConfirmed = true,
			PasswordHash = passwordHash,
			SecurityStamp = Guid.NewGuid().ToString(),
			ConcurrencyStamp = Guid.NewGuid().ToString(),
			PhoneNumber = null,
			PhoneNumberConfirmed = false,
			TwoFactorEnabled = false,
			LockoutEnabled = false,
			AccessFailedCount = 0,
			LockoutTotal = false,
			RoleId = adminRoleId,
			CreationDate = DateTime.Now,
			CreatedBy = 0,
			ModifiedDate = DateTime.Now,
			ModifiedBy = 0,
			IsDelete = false
		});
		modelBuilder.Entity<UserInfo>().HasData(new UserInfo
		{
			UserId = adminUserId,
			FirstName = "System",
			LastName = "Administrator",
			Avatar = null,
			BirthDate = DateTime.Now,
			UserGender = Domain.Enums.Accounts.UserGender.Male
		});
	}

	private static string ComputeSha256Hash(string rawData)
	{
		using var sha256 = SHA256.Create();
		var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
		var builder = new StringBuilder();
		foreach (var b in bytes)
			builder.Append(b.ToString("x2"));
		return builder.ToString();
	}
}
