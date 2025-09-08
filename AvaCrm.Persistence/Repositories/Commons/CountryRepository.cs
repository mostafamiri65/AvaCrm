namespace AvaCrm.Persistence.Repositories.Commons;

public class CountryRepository : ICountryRepository
{
	private readonly AvaCrmContext _context;
	public CountryRepository(AvaCrmContext context)
	{
		_context = context;
	}

	public async Task<Country> CreateCountry(string name)
	{
		Country country = new Country() { Name = name };
		await _context.Countries.AddAsync(country);
		await _context.SaveChangesAsync();
		return country;
	}

	public async Task<bool> DeleteCountry(int id)
	{
		if(await _context.Provinces.AnyAsync(p=>p.CountryId == id))
			return false;
		var country = await GetById(id);
		if(country == null) return false;
		_context.Countries.Remove(country);
		await _context.SaveChangesAsync();
		return true;
	}

	public Task<List<Country>> GetAll()
	{
		throw new NotImplementedException();
	}

	public Task<Country?> GetById(int id)
	{
		throw new NotImplementedException();
	}

	public bool IsExist(string name)
	{
		throw new NotImplementedException();
	}

	public Task UpdateCountry(Country country)
	{
		throw new NotImplementedException();
	}
}
