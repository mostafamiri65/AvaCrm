
namespace AvaCrm.Persistence.Repositories.Accounts;

public class UserRepository : GenericRepository<User>, IUserRepository
{
	private readonly AvaCrmContext _context;
	public UserRepository(AvaCrmContext context) : base(context)
	{
		_context = context;
	}

	public async Task<bool> ExistsEmail(string email)
	{
		return await _context.Users.AnyAsync(u => u.Email == email);
	}

	public async Task<bool> ExistsPhoneNumber(string phoneNumber)
	{
		return await _context.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);
	}

	public async Task<bool> ExistUsername(string username)
	{
		return await _context.Users.AnyAsync(u=>u.Username == username);
	}

	public async Task<User?> GetUserByUsername(string username)
	{
		return await _context.Users.FirstOrDefaultAsync(u => u.Username == username ||
		u.Email == username || u.PhoneNumber == username);
	}
}
