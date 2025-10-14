using AvaCrm.Application.DTOs.Accounts;

namespace AvaCrm.Application.Features.Account
{
    public interface IRoleService
    {
		Task<GlobalResponse<RoleListDto>> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<GlobalResponse<List<RoleListDto>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<GlobalResponse<RoleListDto>> CreateAsync(RoleCreateDto createDto, CancellationToken cancellationToken = default);
        Task<GlobalResponse<RoleListDto>> UpdateAsync(RoleUpdateDto updateDto, CancellationToken cancellationToken = default);
        Task<GlobalResponse<ResponseResultGlobally>> DeleteAsync(long id, CancellationToken cancellationToken = default);
    }
}