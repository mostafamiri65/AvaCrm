using AvaCrm.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace AvaCrm.Application.Contracts;

public class SecurityService : ISecurityService
{
    private readonly MemoryCache _attemptsCache = new(new MemoryCacheOptions());
    private readonly ICaptchaService _captchaService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public SecurityService(ICaptchaService captchaService, IHttpContextAccessor httpContextAccessor)
    {
        _captchaService = captchaService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SecurityCheckResult> CheckLoginSecurityAsync(string username)
    {
        var ipAddress = GetClientIpAddress();
        return await CheckLoginSecurityAsync(username, ipAddress);
    }

    public async Task<bool> RequiresCaptchaAsync(string username)
    {
        var ipAddress = GetClientIpAddress();
        return await RequiresCaptchaAsync(username, ipAddress);
    }

    public Task RecordFailedAttemptAsync(string username)
    {
        var ipAddress = GetClientIpAddress();
        return RecordFailedAttemptAsync(username, ipAddress);
    }

    public Task RecordSuccessfulAttemptAsync(string username)
    {
        var ipAddress = GetClientIpAddress();
        return RecordSuccessfulAttemptAsync(username, ipAddress);
    }
    public async Task<SecurityCheckResult> CheckLoginSecurityAsync(string username, string ipAddress)
    {
        var userKey = $"user_{username}";
        var ipKey = $"ip_{ipAddress}";

        // بررسی مسدودیت کاربر
        if (_attemptsCache.TryGetValue(userKey, out LoginAttempt userAttempts) && userAttempts.IsLockedOut)
        {
            return new SecurityCheckResult
            {
                IsBlocked = true,
                Message = "حساب کاربری شما به دلیل ورودهای ناموفق متعدد موقتاً مسدود شده است. لطفاً ۱۵ دقیقه دیگر تلاش کنید.",
                FailedAttempts = userAttempts.FailedCount
            };
        }

        // بررسی مسدودیت IP
        if (_attemptsCache.TryGetValue(ipKey, out LoginAttempt ipAttempts) && ipAttempts.FailedCount >= 10)
        {
            return new SecurityCheckResult
            {
                IsBlocked = true,
                Message = "IP شما به دلیل فعالیت مشکوک موقتاً مسدود شده است.",
                FailedAttempts = ipAttempts.FailedCount
            };
        }

        // بررسی نیاز به کپچا
        var requiresCaptcha = await RequiresCaptchaAsync(username, ipAddress);

        return new SecurityCheckResult
        {
            RequiresCaptcha = requiresCaptcha,
            IsBlocked = false,
            FailedAttempts = userAttempts?.FailedCount ?? 0
        };
    }

    public Task<bool> RequiresCaptchaAsync(string username, string ipAddress)
    {
        var userKey = $"user_{username}";
        var ipKey = $"ip_{ipAddress}";

        var userAttempts = _attemptsCache.Get<LoginAttempt>(userKey);
        var ipAttempts = _attemptsCache.Get<LoginAttempt>(ipKey);

        // اگر بیش از ۲ بار اشتباه زده، کپچا نمایش بده
        var userFailed = userAttempts?.FailedCount >= 2;
        var ipFailed = ipAttempts?.FailedCount >= 5;

        return Task.FromResult(userFailed || ipFailed);
    }

    public Task RecordFailedAttemptAsync(string username, string ipAddress)
    {
        var userKey = $"user_{username}";
        var ipKey = $"ip_{ipAddress}";

        // افزایش شمارشگر برای کاربر
        var userAttempts = _attemptsCache.GetOrCreate(userKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return new LoginAttempt();
        });
        userAttempts.FailedCount++;

        // قفل کردن بعد از ۵ بار اشتباه
        if (userAttempts.FailedCount >= 5)
        {
            userAttempts.IsLockedOut = true;
            userAttempts.LockoutEnd = DateTime.Now.AddMinutes(15);
        }

        // افزایش شمارشگر برای IP
        var ipAttempts = _attemptsCache.GetOrCreate(ipKey, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
            return new LoginAttempt();
        });
        ipAttempts.FailedCount++;

        return Task.CompletedTask;
    }

    public Task RecordSuccessfulAttemptAsync(string username, string ipAddress)
    {
        var userKey = $"user_{username}";
        var ipKey = $"ip_{ipAddress}";

        // ریست کردن شمارشگرهای موفق
        _attemptsCache.Remove(userKey);
        _attemptsCache.Remove(ipKey);

        return Task.CompletedTask;
    }

    private string GetClientIpAddress()
    {
        try
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext?.Connection?.RemoteIpAddress != null)
            {
                return httpContext.Connection.RemoteIpAddress.ToString();
            }
        }
        catch (Exception ex)
        {
            // در صورت خطا، مقدار پیش‌فرض برگردان
            Console.WriteLine($"Error getting IP address: {ex.Message}");
        }

        return "unknown";
    }
}

public class LoginAttempt
{
    public int FailedCount { get; set; }
    public bool IsLockedOut { get; set; }
    public DateTime LockoutEnd { get; set; }
}