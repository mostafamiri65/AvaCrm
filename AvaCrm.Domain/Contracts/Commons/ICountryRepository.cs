namespace AvaCrm.Domain.Contracts.Commons;

public interface ICountryRepository 
{
	Task<Country?> GetById(int id);
	Task<List<Country>> GetAll();
	Task<bool> CreateCountry(string name);
	Task<bool> UpdateCountry(Country country);
	Task<bool> DeleteCountry(int id);
	Task<bool> IsExist(string name,int countryId);
}
