
using AvaCrm.Domain.Entities.Commons;

namespace AvaCrm.Persistence.Repositories.Commons;

public class CityRepository : ICityRepository
{
	private readonly AvaCrmContext _context;
	public CityRepository(AvaCrmContext context)
	{
		_context = context;
	}

	public async Task<City> Create(City city)
	{
		await _context.Cities.AddAsync(city);
		await _context.SaveChangesAsync();
		return city;
	}

	public async Task<bool> DeleteById(int cityId)
	{
		if (await _context.CustomerAddresses.AnyAsync(c => c.CityId == cityId))
			return false;
		var city = await GetById(cityId);
		if (city == null) return false;
		_context.Cities.Remove(city);
		await _context.SaveChangesAsync();
		return true;
	}

    public async Task<City?> GetByName(string name, int provinceId)
    {
        return await _context.Cities.FirstOrDefaultAsync(c =>
            c.ProvinceId == provinceId && c.Name == name);
    }

    public async Task<List<City>> GetAll(int provinceId)
	{
		return await _context.Cities.Where(c => c.ProvinceId == provinceId).ToListAsync();
	}

	public async Task<City?> GetById(int cityId)
	{
		return await _context.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
	}

	public async Task<bool> IsExist(string name, int provinceId,int cityId)
	{
		return await _context.Cities.AnyAsync(p => p.ProvinceId == provinceId &&
		p.Name == name && p.Id != cityId);
	}

	public async Task<bool> Update(City city)
	{
		if (await IsExist(city.Name, city.ProvinceId, city.Id)) return false;
		var entity = await GetById(city.Id);
		if (entity == null) return false;
		entity.Name = city.Name;
		_context.Cities.Update(entity);
		await _context.SaveChangesAsync();
		return true;
	}
}
