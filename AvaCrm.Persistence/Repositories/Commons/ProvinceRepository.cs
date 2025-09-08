
namespace AvaCrm.Persistence.Repositories.Commons;

public class ProvinceRepository : IProvinceRepository
{
	private readonly AvaCrmContext _context;
	public ProvinceRepository(AvaCrmContext context)
	{
		_context = context;
	}

	public Task<Province> Create(Province province)
	{
		throw new NotImplementedException();
	}

	public Task<bool> Delete(int id)
	{
		throw new NotImplementedException();
	}

	public Task<List<Province>> GetAll(int countryId)
	{
		throw new NotImplementedException();
	}

	public Task<Province> GetById(int id)
	{
		throw new NotImplementedException();
	}

	public Task<bool> IsExist(string name, int countryId)
	{
		throw new NotImplementedException();
	}

	public Task<bool> Update(Province province)
	{
		throw new NotImplementedException();
	}
}
