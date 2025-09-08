
namespace AvaCrm.Persistence.Repositories.Commons;

public class CityRepository : ICityRepository
{
	private readonly AvaCrmContext _context;
	public CityRepository(AvaCrmContext context)
	{
		_context = context;
	}

	public Task<City> Create(City city)
	{
		throw new NotImplementedException();
	}

	public Task<bool> DeleteById(int cityId)
	{
		throw new NotImplementedException();
	}

	public Task<List<City>> GetAll(int provinceId)
	{
		throw new NotImplementedException();
	}

	public Task<City> GetById(int cityId)
	{
		throw new NotImplementedException();
	}

	public Task<bool> IsExist(string name, int provinceId)
	{
		throw new NotImplementedException();
	}

	public Task<bool> Update(City city)
	{
		throw new NotImplementedException();
	}
}
