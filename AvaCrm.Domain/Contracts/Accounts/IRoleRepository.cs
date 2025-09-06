
namespace AvaCrm.Domain.Contracts.Accounts
{
	public interface IRoleRepository : IGenericRepository<Role>
	{
		Task<bool> ExistEnglishTitle(string titleEnglish);
		Task<bool> ExistPersianTitle(string titlePersian);
	}
}
