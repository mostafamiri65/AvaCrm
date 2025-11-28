using AvaCrm.Application.Models;

namespace AvaCrm.Application.Contracts;
public interface ICaptchaService
{
    CaptchaChallenge GenerateChallenge();
    bool ValidateChallenge(string challengeId, string userAnswer);
    void CleanupExpiredChallenges();
}
