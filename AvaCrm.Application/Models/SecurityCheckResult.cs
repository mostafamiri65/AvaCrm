namespace AvaCrm.Application.Models;

public class SecurityCheckResult
{
    public bool RequiresCaptcha { get; set; }
    public bool IsBlocked { get; set; }
    public string Message { get; set; } = string.Empty;
    public int FailedAttempts { get; set; }
}

