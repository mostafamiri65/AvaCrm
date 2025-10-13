using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AvaCrm.Api.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected long GetCurrentUserId()
    {
        // این متد باید بر اساس سیستم احراز هویت شما پیاده‌سازی شود
        // به عنوان مثال:
       
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null && long.TryParse(userIdClaim.Value, out long userId))
        {
            return userId;
        }
        return 1; // مقدار پیش‌فرض برای تست
    }
}
