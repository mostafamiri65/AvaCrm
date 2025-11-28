using AvaCrm.Application.DTOs.Accounts;
using AvaCrm.Application.Rules.Enums;
using AvaCrm.Domain.Entities.Accounts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AvaCrm.Application.Contracts.Identity;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IHashingService _hashingService;
    private readonly JwtSettings _jwtSettings;
    private readonly ISecurityService _securityService;
    private readonly ICaptchaService _captchaService;
    public AuthService(IUserRepository userRepository, IHashingService hashingService,
        JwtSettings jwtSettings, ISecurityService securityService, ICaptchaService captchaService)
    {
        _userRepository = userRepository;
        _hashingService = hashingService;
        _jwtSettings = jwtSettings;
        _securityService = securityService;
        _captchaService = captchaService;
    }

    public async Task<AuthResponse?> GetUserForLogin(string username, string password)
    {
        AuthResponse response = new AuthResponse();
        var user = await _userRepository.GetUserByUsername(username);
        if (user == null)
        {
            response.LoginState = LoginState.InvalidCredentials;
        }
        else if (user.TwoFactorEnabled)
        {
            response.LoginState = LoginState.RequiresTwoFactor;
        }
        else if (user.LockoutEnd > DateTimeOffset.Now)
        {
            response.LoginState = LoginState.TemporaryLockedOut;
        }
        else if (user.LockoutEnabled)
        {
            response.LoginState = LoginState.LockedOut;
        }
        else if (user.PasswordHash != null &&
            !_hashingService.Verify(password, user.PasswordHash))
        {
            response.LoginState = LoginState.InvalidCredentials;
        }
        else
        {

        }
        return response;
    }


    public async Task<AuthResponse> Login(AuthRequest request)
    {
        AuthResponse response = new AuthResponse();

        // بررسی امنیت - استفاده از متد ساده‌تر که IP را خودش می‌گیرد
        var securityCheck = await _securityService.CheckLoginSecurityAsync(request.Username);

        if (securityCheck.IsBlocked)
        {
            response.LoginState = LoginState.TemporaryLockedOut;
            response.Message = securityCheck.Message;
            return response;
        }

        // اگر کپچا لازم است، اعتبارسنجی کن
        if (securityCheck.RequiresCaptcha)
        {
            if (string.IsNullOrEmpty(request.CaptchaId) || string.IsNullOrEmpty(request.CaptchaAnswer))
            {
                response.LoginState = LoginState.CaptchaRequired;
                response.Message = "لطفا کد امنیتی را وارد کنید";
                return response;
            }

            if (!_captchaService.ValidateChallenge(request.CaptchaId, request.CaptchaAnswer))
            {
                await _securityService.RecordFailedAttemptAsync(request.Username);

                response.LoginState = LoginState.CaptchaFailed;
                response.Message = "کد امنیتی نادرست است";
                return response;
            }
        }

        // ادامه منطق موجود برای بررسی کاربر و رمز عبور...
        var user = await _userRepository.GetUserByUsername(request.Username);
        if (user == null)
        {
            await _securityService.RecordFailedAttemptAsync(request.Username);

            response.LoginState = LoginState.InvalidCredentials;
            response.Message = "نام کاربری یا رمز عبور اشتباه است";
        }
        else if (user.TwoFactorEnabled)
        {
            response.LoginState = LoginState.RequiresTwoFactor;
            response.Message = "لطفا کد تأیید دو مرحله‌ای را وارد کنید";
        }
        else if (user.LockoutEnd > DateTimeOffset.Now)
        {
            response.LoginState = LoginState.TemporaryLockedOut;
            response.Message = "حساب کاربری شما موقتاً قفل شده است";
        }
        else if (user.LockoutEnabled && user.AccessFailedCount >= 5)
        {
            response.LoginState = LoginState.LockedOut;
            response.Message = "حساب کاربری شما قفل شده است";
        }
        else if (user.PasswordHash != null &&
            !_hashingService.Verify(request.Password, user.PasswordHash))
        {
            await _securityService.RecordFailedAttemptAsync(request.Username);

            response.LoginState = LoginState.InvalidCredentials;
            response.Message = "نام کاربری یا رمز عبور اشتباه است";
        }
        else
        {
            // ورود موفق
            await _securityService.RecordSuccessfulAttemptAsync(request.Username);

            // آپدیت وضعیت کاربر در دیتابیس
            user.AccessFailedCount = 0;
            user.LockoutEnd = null;
            await _userRepository.UpdateAsync(user);

            var userdto = new UserDto
            {
                UserId = user.Id,
                RoleId = user.RoleId,
                Email = user.Email,
                AvatarFile = user.UserInfo != null ? user.UserInfo.Avatar : string.Empty,
                Fullname = user.UserInfo?.FirstName + " " + user.UserInfo?.LastName,
                PhoneNumber = user.PhoneNumber,
                UserGender = user.UserInfo != null ? user.UserInfo.UserGender : Domain.Enums.Accounts.UserGender.Male,
                UserName = user.Username
            };

            JwtSecurityToken jwtSecurityToken = GenerateToken(user);
            response.LoginState = LoginState.Success;
            response.User = userdto;
            response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            response.Message = "ورود موفقیت‌آمیز بود";
        }

        return response;
    }

    //public async Task<AuthResponse> Login(AuthRequest request)
    //{
    //    AuthResponse response = new AuthResponse();
    //    var user = await _userRepository.GetUserByUsername(request.Username);
    //    if (user == null)
    //    {
    //        response.LoginState = LoginState.InvalidCredentials;
    //    }
    //    else if (user.TwoFactorEnabled)
    //    {
    //        response.LoginState = LoginState.RequiresTwoFactor;
    //    }
    //    else if (user.LockoutEnd > DateTimeOffset.Now)
    //    {
    //        response.LoginState = LoginState.TempararyLockedOut;
    //    }
    //    else if (user.LockoutEnabled)
    //    {
    //        response.LoginState = LoginState.LockedOut;
    //    }
    //    else if (user.PasswordHash != null &&
    //        !_hashingService.Verify(request.Password, user.PasswordHash))
    //    {
    //        response.LoginState = LoginState.InvalidCredentials;
    //    }
    //    else
    //    {
    //        var userdto = new UserDto
    //        {
    //            UserId = user.Id,
    //            RoleId = user.RoleId,
    //            Email = user.Email,
    //            AvatarFile = user.UserInfo != null ?
    //                    user.UserInfo.Avatar : string.Empty,
    //            Fullname = user.UserInfo?.FirstName + " " + user.UserInfo?.LastName,
    //            PhoneNumber = user.PhoneNumber,
    //            UserGender = user.UserInfo != null ?
    //                    user.UserInfo.UserGender : Domain.Enums.Accounts.UserGender.Male,
    //            UserName = user.Username
    //        };

    //        JwtSecurityToken jwtSecurityToken = GenerateToken(user);
    //        response.LoginState = LoginState.Success;
    //        response.User = userdto;
    //        response.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

    //    }

    //    return response;
    //}
    private JwtSecurityToken GenerateToken(User user)
    {
        var displayName = !string.IsNullOrWhiteSpace(user.Username)
            ? user.Username : !string.IsNullOrWhiteSpace(user.Email)
            ? user.Email : !string.IsNullOrWhiteSpace(user.PhoneNumber)
            ? user.PhoneNumber : string.Empty;
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, displayName),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.RoleId.ToString())
        };

        var authSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: DateTime.Now.AddMinutes(_jwtSettings.ExpireMinutes),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return token;
    }

}
