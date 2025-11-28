using AvaCrm.Application.Contracts;
using AvaCrm.Application.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaptchaController : ControllerBase
    {
        private readonly ICaptchaService _captchaService;
        private readonly ISecurityService _securityService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CaptchaController(
            ICaptchaService captchaService,
            ISecurityService securityService,
            IHttpContextAccessor httpContextAccessor)
        {
            _captchaService = captchaService;
            _securityService = securityService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("generate")]
        public ActionResult<CaptchaResponse> GenerateCaptcha()
        {
            try
            {
                var challenge = _captchaService.GenerateChallenge();
                return Ok(new CaptchaResponse
                {
                    Success = true,
                    Challenge = challenge
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CaptchaResponse
                {
                    Success = false,
                    Message = "خطا در تولید کد امنیتی"
                });
            }
        }

        [HttpPost("verify")]
        public ActionResult<CaptchaResponse> VerifyCaptcha([FromBody] CaptchaRequest request)
        {
            try
            {
                var isValid = _captchaService.ValidateChallenge(request.CaptchaId, request.UserAnswer);

                return Ok(new CaptchaResponse
                {
                    Success = isValid,
                    Message = isValid ? "کد امنیتی صحیح است" : "کد امنیتی نادرست است"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CaptchaResponse
                {
                    Success = false,
                    Message = "خطا در بررسی کد امنیتی"
                });
            }
        }

        [HttpPost("check-security")]
        public async Task<ActionResult<SecurityCheckResult>> CheckSecurity([FromBody] SecurityCheckRequest request)
        {
            // اگر IP ارسال نشده، از درخواست جاری بگیر
            var ipAddress = string.IsNullOrEmpty(request.IpAddress)
                ? GetClientIpAddress()
                : request.IpAddress;

            var result = await _securityService.CheckLoginSecurityAsync(request.Username, ipAddress);
            return Ok(result);
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
                Console.WriteLine($"Error getting IP address: {ex.Message}");
            }

            return "unknown";
        }
    }
}
