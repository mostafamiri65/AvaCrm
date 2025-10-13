namespace AvaCrm.Domain.Contracts.Commons;

public interface IProvinceRepository
{
	Task<List<Province>> GetAll(int countryId);
	Task<Province?> GetById(int id);
	Task<bool> IsExist(string name, int countryId,int provinceId);
	Task<Province> Create(Province province);
	Task<bool> Update(Province province);
	Task<bool> Delete(int id);
}
