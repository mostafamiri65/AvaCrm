using AvaCrm.Application.Models;

namespace AvaCrm.Application.Contracts;
public class CaptchaService : ICaptchaService
{
    private readonly Dictionary<string, CaptchaChallenge> _challenges = new();
    private readonly List<ICaptchaGenerator> _generators;
    private readonly Timer _cleanupTimer;
    private readonly Random _random = new();

    public CaptchaService()
    {
        _generators = new List<ICaptchaGenerator>
        {
            new TextCaptchaGenerator(),
            new MathCaptchaGenerator(),
            new LogicCaptchaGenerator(),
            new SequenceCaptchaGenerator()
        };

        // تمیز کردن چالش‌های منقضی هر ۵ دقیقه
        _cleanupTimer = new Timer(_ => CleanupExpiredChallenges(), null,
            TimeSpan.Zero, TimeSpan.FromMinutes(5));
    }

    public CaptchaChallenge GenerateChallenge()
    {
        // انتخاب تصادفی یک ژنراتور
        var generator = _generators[_random.Next(_generators.Count)];
        var challenge = generator.GenerateChallenge();

        // ذخیره در حافظه
        _challenges[challenge.Id] = challenge;

        return challenge;
    }

    public bool ValidateChallenge(string challengeId, string userAnswer)
    {
        if (string.IsNullOrEmpty(challengeId) || string.IsNullOrEmpty(userAnswer))
            return false;

        if (!_challenges.TryGetValue(challengeId, out var challenge))
            return false;

        // حذف چالش (یکبار مصرف)
        _challenges.Remove(challengeId);

        // بررسی انقضا
        if (DateTime.Now > challenge.ExpiresAt)
            return false;

        // مقایسه پاسخ (case insensitive و حذف فاصله)
        var normalizedAnswer = challenge.Answer.Trim().ToLower();
        var normalizedUserAnswer = userAnswer.Trim().ToLower();

        return normalizedAnswer == normalizedUserAnswer;
    }

    public void CleanupExpiredChallenges()
    {
        var expiredIds = _challenges
            .Where(kvp => DateTime.Now > kvp.Value.ExpiresAt)
            .Select(kvp => kvp.Key)
            .ToList();

        foreach (var id in expiredIds)
        {
            _challenges.Remove(id);
        }

        if (expiredIds.Count > 0)
        {
            Console.WriteLine($"Cleaned up {expiredIds.Count} expired captcha challenges");
        }
    }
}