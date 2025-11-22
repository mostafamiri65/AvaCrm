using System.Security.Cryptography;
using System.Text;
using AvaCrm.Domain.Entities.ProjectManagement;

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

	public virtual DbSet<Country> Countries { get; set; }
	public virtual DbSet<Province> Provinces { get; set; }
	public virtual DbSet<City> Cities { get; set; }

	public virtual DbSet<Customer> Customers { get; set; }
	public virtual DbSet<ContactPerson> ContactPeople { get; set; }
	public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
	public virtual DbSet<CustomerTag> CustomerTags { get; set; }
	public virtual DbSet<FollowUp> FollowUps { get; set; }
	public virtual DbSet<IndividualCustomer> IndividualCustomers { get; set; }
	public virtual DbSet<Interaction> Interactions { get; set; }
	public virtual DbSet<Note> Notes { get; set; }
	public virtual DbSet<OrganizationCustomer> OrganizationCustomers { get; set; }
	public virtual DbSet<Tag> Tags { get; set; }
	public virtual DbSet<Project> Projects { get; set; }
	public virtual DbSet<TaskItem> TaskItems { get; set; }
	public virtual DbSet<Comment> Comments { get; set; }
	public virtual DbSet<Attachment> Attachments { get; set; }
	public virtual DbSet<ActivityLog> ActivityLogs { get; set; }
	public virtual DbSet<UserProject> UserProjects { get; set; }

	#endregion
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		DateTime createDate = new DateTime(2025, 9, 8, 13, 30, 33, 713, DateTimeKind.Local).AddTicks(9117);
		modelBuilder.Entity<RolePermission>().HasKey(rp => new { rp.RoleId, rp.PermissionId });

		modelBuilder.Entity<CustomerTag>().HasKey(ct => new { ct.TagId, ct.CustomerId });
		var adminRoleId = 1L;
		modelBuilder.Entity<Role>().HasData(new Role
		{
			Id = adminRoleId,
			TitleEnglish = "Admin",
			TitlePersian = "مدیر",
			CreationDate = createDate,
			CreatedBy = 0,
			ModifiedDate = createDate,
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
			SecurityStamp = "e498f27c-da62-4ad4-bb93-64e5b0238d58",
			ConcurrencyStamp = "05cd8348-3dc0-4c9d-b3d6-ecc730a32bb8",
			PhoneNumber = null,
			PhoneNumberConfirmed = false,
			TwoFactorEnabled = false,
			LockoutEnabled = false,
			AccessFailedCount = 0,
			LockoutTotal = false,
			RoleId = adminRoleId,
			CreationDate = createDate,
			CreatedBy = 0,
			ModifiedDate = createDate,
			ModifiedBy = 0,
			IsDelete = false
		});
		modelBuilder.Entity<UserInfo>().HasData(new UserInfo
		{
			UserId = adminUserId,
			FirstName = "System",
			LastName = "Administrator",
			Avatar = null,
			UserGender = Domain.Enums.Accounts.UserGender.Male
		});
		modelBuilder.Entity<Country>().HasData(new Country { Id = 1, Name = "ایران" });

		modelBuilder.Entity<UserProject>().HasKey(e=> new {e.ProjectId, e.UserId});
		#region Province
		var provinces = new List<Province>
		{
			new Province { Id = 1, CountryId = 1, Name = "آذربایجان شرقی" },
			new Province { Id = 2, CountryId = 1, Name = "آذربایجان غربی" },
			new Province { Id = 3, CountryId = 1, Name = "اردبیل" },
			new Province { Id = 4, CountryId = 1, Name = "اصفهان" },
			new Province { Id = 5, CountryId = 1, Name = "البرز" },
			new Province { Id = 6, CountryId = 1, Name = "ایلام" },
			new Province { Id = 7, CountryId = 1, Name = "بوشهر" },
			new Province { Id = 8, CountryId = 1, Name = "تهران" },
			new Province { Id = 9, CountryId = 1, Name = "چهارمحال و بختیاری" },
			new Province { Id = 10, CountryId = 1, Name = "خراسان جنوبی" },
			new Province { Id = 11, CountryId = 1, Name = "خراسان رضوی" },
			new Province { Id = 12, CountryId = 1, Name = "خراسان شمالی" },
			new Province { Id = 13, CountryId = 1, Name = "خوزستان" },
			new Province { Id = 14, CountryId = 1, Name = "زنجان" },
			new Province { Id = 15, CountryId = 1, Name = "سمنان" },
			new Province { Id = 16, CountryId = 1, Name = "سیستان و بلوچستان" },
			new Province { Id = 17, CountryId = 1, Name = "فارس" },
			new Province { Id = 18, CountryId = 1, Name = "قزوین" },
			new Province { Id = 19, CountryId = 1, Name = "قم" },
			new Province { Id = 20, CountryId = 1, Name = "کردستان" },
			new Province { Id = 21, CountryId = 1, Name = "کرمان" },
			new Province { Id = 22, CountryId = 1, Name = "کرمانشاه" },
			new Province { Id = 23, CountryId = 1, Name = "کهکیلویه و بویراحمد" },
			new Province { Id = 24, CountryId = 1, Name = "گلستان" },
			new Province { Id = 25, CountryId = 1, Name = "گیلان" },
			new Province { Id = 26, CountryId = 1, Name = "لرستان" },
			new Province { Id = 27, CountryId = 1, Name = "مازندران" },
			new Province { Id = 28, CountryId = 1, Name = "مرکزی" },
			new Province { Id = 29, CountryId = 1, Name = "هرمزگان" },
			new Province { Id = 30, CountryId = 1, Name = "همدان" },
			new Province { Id = 31, CountryId = 1, Name = "یزد" }
		};

		modelBuilder.Entity<Province>().HasData(provinces);
		#endregion

	}
	public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
	{
		foreach (var entry in ChangeTracker.Entries<BaseEntity>())
		{
			entry.Entity.ModifiedDate = DateTime.Now;

			if (entry.State == EntityState.Added)
			{
				entry.Entity.CreationDate = DateTime.Now;
			}
		}


		return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
	}

	public override int SaveChanges()
	{
		foreach (var entry in ChangeTracker.Entries<BaseEntity>())
		{
			entry.Entity.ModifiedDate = DateTime.Now;

			if (entry.State == EntityState.Added)
			{
				entry.Entity.CreationDate = DateTime.Now;
			}
		}
		return base.SaveChanges();
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
