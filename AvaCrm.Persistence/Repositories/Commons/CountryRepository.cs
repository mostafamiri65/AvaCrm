namespace AvaCrm.Persistence.Repositories.Commons;

public class CountryRepository : ICountryRepository
{
	private readonly AvaCrmContext _context;
	public CountryRepository(AvaCrmContext context)
	{
		_context = context;
	}

	public async Task<bool> CreateCountry(string name)
	{
		if(await IsExist(name,0)) return false;
		Country country = new Country() { Name = name };
		await _context.Countries.AddAsync(country);
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<bool> DeleteCountry(int id)
	{
		if (await _context.Provinces.AnyAsync(p => p.CountryId == id))
			return false;
		if (await _context.CustomerAddresses.AnyAsync(c => c.CountryId == id))
			return false;
		var country = await GetById(id);
		if (country == null) return false;
		_context.Countries.Remove(country);
		await _context.SaveChangesAsync();
		return true;
	}

	public async Task<List<Country>> GetAll()
	{
		return await _context.Countries.ToListAsync();
	}

	public async Task<Country?> GetById(int id)
	{
		return await _context.Countries.FirstOrDefaultAsync(r => r.Id == id);
	}

	public async Task<bool> IsExist(string name, int countryId)
	{
		return await _context.Countries.AnyAsync(c => c.Name == name &&
		c.Id != countryId);
	}

	public async Task<bool> UpdateCountry(Country country)
	{
		if (await IsExist(country.Name, country.Id)) return false;
		var entity = await GetById(country.Id);
		if (entity == null) return false;
		entity.Name = country.Name;
		_context.Countries.Update(entity);
		await _context.SaveChangesAsync();
		return true;
	}
}
