namespace AvaCrm.Domain.Contracts.Commons;

public interface ICountryRepository 
{
	Task<Country> GetById(int id);
	Task<List<Country>> GetAll();
	Task<Country> CreateCountry(string name);
	Task UpdateCountry(Country country);
	Task<bool> DeleteCountry(int id);
	bool IsExist(string name);
}
