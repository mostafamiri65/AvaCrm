namespace AvaCrm.Domain.Contracts.Commons;

public interface ICityRepository
{
	Task<List<City>> GetAll(int provinceId);
	Task<City> GetById(int cityId);
	Task<bool> IsExist(string name, int provinceId);
	Task<City> Create(City city);
	Task<bool> Update(City city);
	Task<bool> DeleteById(int cityId);
}
