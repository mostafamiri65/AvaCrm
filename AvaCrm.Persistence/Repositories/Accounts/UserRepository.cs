
using AvaCrm.Domain.Readers.Accounts;

namespace AvaCrm.Persistence.Repositories.Accounts;

public class UserRepository :  IUserRepository
{
	private readonly AvaCrmContext _context;
	public UserRepository(AvaCrmContext context) 
	{
		_context = context;
	}
    
	public async Task<User?> GetUserByUsername(string username)
	{
		return await _context.Users.FirstOrDefaultAsync(u => u.Username == username ||
		u.Email == username || u.PhoneNumber == username);
	}

	public async Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
	{
		return await _context.Users
			.Include(u => u.Role)
			.FirstOrDefaultAsync(u => u.Id == id && !u.IsDelete, cancellationToken);
	}

	public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
	{
		return await _context.Users
			.Include(u => u.Role)
			.FirstOrDefaultAsync(u => u.Username == username && !u.IsDelete, cancellationToken);
	}

	public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
	{
		return await _context.Users
			.Include(u => u.Role)
			.FirstOrDefaultAsync(u => u.Email == email && !u.IsDelete, cancellationToken);
	}

	public async Task<List<UserListDto>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		return await _context.Users
			.Include(u => u.Role)
			.Where(u => !u.IsDelete)
			.OrderBy(u => u.Id)
			.Select(u => new UserListDto
			{
				Id = u.Id,
				Username = u.Username,
				Email = u.Email,
				RoleId = u.RoleId,
				RoleTitlePersian = u.Role.TitlePersian,
				RoleTitleEnglish = u.Role.TitleEnglish,
				EmailConfirmed = u.EmailConfirmed,
				PhoneNumber = u.PhoneNumber,
				PhoneNumberConfirmed = u.PhoneNumberConfirmed,
				LockoutEnabled = u.LockoutEnabled,
				LockoutTotal = u.LockoutTotal,
				CreatedDate = u.CreationDate
			})
			.ToListAsync(cancellationToken);
	}

	public async Task<UserDetailDto?> GetDetailByIdAsync(long id, CancellationToken cancellationToken = default)
	{
		return await _context.Users
			.Include(u => u.Role)
			.Where(u => u.Id == id && !u.IsDelete)
			.Select(u => new UserDetailDto
			{
				Id = u.Id,
				Username = u.Username,
				Email = u.Email,
				RoleId = u.RoleId,
				RoleTitlePersian = u.Role.TitlePersian,
				RoleTitleEnglish = u.Role.TitleEnglish,
				EmailConfirmed = u.EmailConfirmed,
				PhoneNumber = u.PhoneNumber,
				PhoneNumberConfirmed = u.PhoneNumberConfirmed,
				TwoFactorEnabled = u.TwoFactorEnabled,
				LockoutEnabled = u.LockoutEnabled,
				LockoutTotal = u.LockoutTotal,
				AccessFailedCount = u.AccessFailedCount,
				LockoutEnd = u.LockoutEnd,
				CreatedDate = u.CreationDate
			})
			.FirstOrDefaultAsync(cancellationToken);
	}

	public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
	{
		_context.Users.Add(user);
		await _context.SaveChangesAsync(cancellationToken);
		return user;
	}

	public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
	{
		_context.Users.Update(user);
		await _context.SaveChangesAsync(cancellationToken);
		return user;
	}

	public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
	{
		var user = await _context.Users
			.FirstOrDefaultAsync(u => u.Id == id && !u.IsDelete, cancellationToken);

		if (user == null) return false;

		user.IsDelete = true;
		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

	public async Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default)
	{
		return await _context.Users
			.AnyAsync(u => u.Id == id && !u.IsDelete, cancellationToken);
	}

	public async Task<bool> IsUsernameDuplicateAsync(string username, long? excludeId = null, CancellationToken cancellationToken = default)
	{
		var query = _context.Users
			.Where(u => !u.IsDelete && u.Username == username);

		if (excludeId.HasValue)
		{
			query = query.Where(u => u.Id != excludeId.Value);
		}

		return await query.AnyAsync(cancellationToken);
	}

	public async Task<bool> IsEmailDuplicateAsync(string email, long? excludeId = null, CancellationToken cancellationToken = default)
	{
		var query = _context.Users
			.Where(u => !u.IsDelete && u.Email == email);

		if (excludeId.HasValue)
		{
			query = query.Where(u => u.Id != excludeId.Value);
		}

		return await query.AnyAsync(cancellationToken);
	}
}
