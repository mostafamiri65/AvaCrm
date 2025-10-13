using AvaCrm.Application.Models.Identity;

namespace AvaCrm.Application.Contracts.Identity;

public interface IAuthService
{
	Task<AuthResponse> Login(AuthRequest request);
	Task<AuthResponse?> GetUserForLogin(string username, string password);

}
