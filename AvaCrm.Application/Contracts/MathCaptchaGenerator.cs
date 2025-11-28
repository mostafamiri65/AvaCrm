using AvaCrm.Application.Models;

namespace AvaCrm.Application.Contracts;

public class MathCaptchaGenerator : ICaptchaGenerator
{
    private readonly Random _random = new();
    public string GeneratorType => "Math";

    public CaptchaChallenge GenerateChallenge()
    {
        var num1 = _random.Next(1, 20);
        var num2 = _random.Next(1, 20);

        // برای کاهش احتمال حدس زدن، عملیات پیچیده‌تر
        var operations = new[]
        {
            new {
                Question = $"{num1} + {num2} × 2 = ?",
                Answer = (num1 + num2 * 2).ToString()
            },
            new {
                Question = $"{num1 + num2} - {num2} = ?",
                Answer = num1.ToString()
            },
            new {
                Question = $"{num1} × {num2} + {num1} = ?",
                Answer = (num1 * num2 + num1).ToString()
            },
            new {
                Question = $"({num1} + {num2}) ÷ {_random.Next(2, 5)} = ?",
                Answer = ((num1 + num2) / _random.Next(2, 5)).ToString()
            }
        };

        var selected = operations[_random.Next(operations.Length)];

        return new CaptchaChallenge
        {
            Question = selected.Question,
            Answer = selected.Answer,
            Type = CaptchaType.Math
        };
    }
}