using AvaCrm.Domain.Readers.Accounts;
using Microsoft.EntityFrameworkCore;

namespace AvaCrm.Persistence.Repositories.Accounts;

public class RoleRepository : IRoleRepository
{
    private readonly AvaCrmContext _context;

    public RoleRepository(AvaCrmContext context)
    {
        _context = context;
    }
    public async Task<Role?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
	{
		return await _context.Roles
			.FirstOrDefaultAsync(r => r.Id == id && !r.IsDelete, cancellationToken);
	}

	public async Task<List<RoleListDto>> GetAllAsync(CancellationToken cancellationToken = default)
	{
		return await _context.Roles
			.Where(r => !r.IsDelete)
			.OrderBy(r => r.Id)
			.Select(r => new RoleListDto
			{
				Id = r.Id,
				TitleEnglish = r.TitleEnglish,
				TitlePersian = r.TitlePersian,
				CreatedDate = r.CreationDate
			})
			.ToListAsync(cancellationToken);
	}

	public async Task<Role> CreateAsync(Role role, CancellationToken cancellationToken = default)
	{
		_context.Roles.Add(role);
		await _context.SaveChangesAsync(cancellationToken);
		return role;
	}

	public async Task<Role> UpdateAsync(Role role, CancellationToken cancellationToken = default)
	{
		_context.Roles.Update(role);
		await _context.SaveChangesAsync(cancellationToken);
		return role;
	}

	public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
	{
		var role = await _context.Roles
			.FirstOrDefaultAsync(r => r.Id == id && !r.IsDelete, cancellationToken);

		if (role == null) return false;

		role.IsDelete = true;
		await _context.SaveChangesAsync(cancellationToken);
		return true;
	}

	public async Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default)
	{
		return await _context.Roles
			.AnyAsync(r => r.Id == id && !r.IsDelete, cancellationToken);
	}

	public async Task<bool> IsTitlePersianDuplicateAsync(string titlePersian, long? excludeId = null, CancellationToken cancellationToken = default)
	{
		var query = _context.Roles
			.Where(r => !r.IsDelete && r.TitlePersian == titlePersian);

		if (excludeId.HasValue)
		{
			query = query.Where(r => r.Id != excludeId.Value);
		}

		return await query.AnyAsync(cancellationToken);
	}
}