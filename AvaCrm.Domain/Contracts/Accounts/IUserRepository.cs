using AvaCrm.Domain.Readers.Accounts;

namespace AvaCrm.Domain.Contracts.Accounts;

public interface IUserRepository 
{
	Task<User?> GetUserByUsername(string username); Task<User?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<List<UserListDto>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<UserDetailDto?> GetDetailByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<User> CreateAsync(User user, CancellationToken cancellationToken = default);
    Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> IsUsernameDuplicateAsync(string username, long? excludeId = null, CancellationToken cancellationToken = default);
    Task<bool> IsEmailDuplicateAsync(string email, long? excludeId = null, CancellationToken cancellationToken = default);
}
