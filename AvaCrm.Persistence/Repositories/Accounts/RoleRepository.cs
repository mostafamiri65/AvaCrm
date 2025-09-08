namespace AvaCrm.Persistence.Repositories.Accounts;

public class RoleRepository : GenericRepository<Role>, IRoleRepository
{
	private readonly AvaCrmContext _context;
	public RoleRepository(AvaCrmContext context) : base(context)
	{
		_context = context;
	}

	public async Task<bool> ExistEnglishTitle(string titleEnglish)
	{
		return await _context.Roles.AnyAsync(r=>r.TitleEnglish == titleEnglish);
	}

	public async Task<bool> ExistPersianTitle(string titlePersian)
	{
		return await _context.Roles.AnyAsync(r => r.TitlePersian == titlePersian);
	}
}
