using AvaCrm.Application.Models;

namespace AvaCrm.Application.Contracts;

public interface ISecurityService
{
    Task<SecurityCheckResult> CheckLoginSecurityAsync(string username, string ipAddress);
    Task<bool> RequiresCaptchaAsync(string username, string ipAddress);
    Task RecordFailedAttemptAsync(string username, string ipAddress);
    Task RecordSuccessfulAttemptAsync(string username, string ipAddress);
    Task<SecurityCheckResult> CheckLoginSecurityAsync(string username);
    Task<bool> RequiresCaptchaAsync(string username);
    Task RecordFailedAttemptAsync(string username);
    Task RecordSuccessfulAttemptAsync(string username);
}
