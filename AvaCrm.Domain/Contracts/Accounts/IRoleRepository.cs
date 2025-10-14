
using AvaCrm.Domain.Readers.Accounts;

namespace AvaCrm.Domain.Contracts.Accounts
{
	public interface IRoleRepository 
	{
        Task<Role?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<List<RoleListDto>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Role> CreateAsync(Role role, CancellationToken cancellationToken = default);
        Task<Role> UpdateAsync(Role role, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
        Task<bool> ExistsAsync(long id, CancellationToken cancellationToken = default);
        Task<bool> IsTitlePersianDuplicateAsync(string titlePersian, long? excludeId = null, CancellationToken cancellationToken = default);
	}
}
