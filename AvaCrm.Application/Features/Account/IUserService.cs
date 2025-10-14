using AvaCrm.Application.DTOs.Accounts;

namespace AvaCrm.Application.Features.Account
{
    public interface IUserService
    {
		Task<GlobalResponse<UserListDto>> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<GlobalResponse<UserDetailDto>> GetDetailByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<GlobalResponse<List<UserListDto>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<GlobalResponse<UserListDto>> CreateAsync(UserCreateDto createDto, CancellationToken cancellationToken = default);
        Task<GlobalResponse<UserListDto>> UpdateAsync(UserUpdateDto updateDto, CancellationToken cancellationToken = default);
        Task<GlobalResponse<ResponseResultGlobally>> DeleteAsync(long id, CancellationToken cancellationToken = default);
        Task<GlobalResponse<ResponseResultGlobally>> ChangePasswordAsync(UserChangePasswordDto changePasswordDto, CancellationToken cancellationToken = default);
        Task<GlobalResponse<ResponseResultGlobally>> ToggleLockoutAsync(long id, CancellationToken cancellationToken = default);
	}
}