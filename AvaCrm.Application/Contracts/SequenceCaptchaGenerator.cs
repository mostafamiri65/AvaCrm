using AvaCrm.Application.Models;

namespace AvaCrm.Application.Contracts;

public class SequenceCaptchaGenerator : ICaptchaGenerator
{
    private readonly Random _random = new();
    public string GeneratorType => "Sequence";

    public CaptchaChallenge GenerateChallenge()
    {
        var start = _random.Next(1, 15);
        var patternType = _random.Next(3);

        switch (patternType)
        {
            case 0: // جمع
                var step1 = _random.Next(2, 5);
                return new CaptchaChallenge
                {
                    Question = $"عدد بعدی در دنباله {start}, {start + step1}, {start + step1 * 2} چیست؟",
                    Answer = (start + step1 * 3).ToString(),
                    Type = CaptchaType.Sequence
                };

            case 1: // ضرب
                var step2 = _random.Next(2, 4);
                return new CaptchaChallenge
                {
                    Question = $"عدد بعدی در دنباله {start}, {start * step2}, {start * step2 * step2} چیست؟",
                    Answer = (start * step2 * step2 * step2).ToString(),
                    Type = CaptchaType.Sequence
                };

            default: // توان
                return new CaptchaChallenge
                {
                    Question = $"عدد بعدی در دنباله {start}, {start * start}, {start * start * start} چیست؟",
                    Answer = (start * start * start * start).ToString(),
                    Type = CaptchaType.Sequence
                };
        }
    }
}