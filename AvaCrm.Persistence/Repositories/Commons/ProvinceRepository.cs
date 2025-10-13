
using AvaCrm.Domain.Entities.Commons;

namespace AvaCrm.Persistence.Repositories.Commons;

public class ProvinceRepository : IProvinceRepository
{
	private readonly AvaCrmContext _context;
	public ProvinceRepository(AvaCrmContext context)
	{
		_context = context;
	}

	public async Task<Province> Create(Province province)
	{
		await _context.Provinces.AddAsync(province);
		await _context.SaveChangesAsync();
		return province;
	}

	public async Task<bool> Delete(int id)
	{
		if (await _context.Cities.AnyAsync(p => p.ProvinceId == id))
			return false;
		if (await _context.CustomerAddresses.AnyAsync(c => c.ProvinceId == id))
			return false;
		var province = await GetById(id);
		if (province == null) return false;
		_context.Provinces.Remove(province);
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<List<Province>> GetAll(int countryId)
	{
		return await _context.Provinces.Where(p => p.CountryId == countryId).ToListAsync();
	}

	public async Task<Province?> GetById(int id)
	{
		return await _context.Provinces.FirstOrDefaultAsync(p => p.Id == id);
	}

	public async Task<bool> IsExist(string name, int countryId, int provinceId)
	{
		return await _context.Provinces.AnyAsync(p => p.CountryId == countryId &&
		p.Name == name && p.Id != provinceId);
	}

	public async Task<bool> Update(Province province)
	{
		if (await IsExist(province.Name, province.CountryId, province.Id)) return false;
		var entity = await GetById(province.Id);
		if (entity == null) return false;
		entity.Name = province.Name;
		_context.Provinces.Update(entity);
		await _context.SaveChangesAsync();
		return true;
	}
}
