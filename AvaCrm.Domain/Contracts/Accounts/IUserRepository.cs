namespace AvaCrm.Domain.Contracts.Accounts;

public interface IUserRepository : IGenericRepository<User>
{
	Task<User?> GetUserByUsername(string username);
	Task<bool> ExistUsername(string username);
	Task<bool> ExistsEmail(string email);
	Task<bool> ExistsPhoneNumber(string phoneNumber);
}
