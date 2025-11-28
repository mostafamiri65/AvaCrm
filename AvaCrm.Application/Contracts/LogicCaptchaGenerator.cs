using AvaCrm.Application.Models;

namespace AvaCrm.Application.Contracts;

public class LogicCaptchaGenerator : ICaptchaGenerator
{
    private readonly Random _random = new();
    public string GeneratorType => "Logic";

    private readonly (string Question, string Answer)[] _questions =
    {
        ("رنگ آسمان در روز آفتابی چیست؟", "آبی"),
        ("چه حیوانی میو میو می کند؟", "گربه"),
        ("چند تا پا دارد یک سگ؟", "4"),
        ("فصل بعد از تابستان چیست؟", "پاییز"),
        ("چه حیوانی عسل تولید می کند؟", "زنبور"),
        ("چه رنگی مخلوط آبی و زرد است؟", "سبز"),
        ("چه عددی بین ۱۰ و ۱۲ قرار دارد؟", "11"),
        ("نام پایتخت ایران چیست؟", "تهران"),
        ("چه حیوانی شاه جنگل نامیده می شود؟", "شیر"),
        ("چند ماه ۳۰ روز دارد؟", "4"),
        ("سردترین فصل سال کدام است؟", "زمستان"),
        ("بزرگترین سیاره منظومه شمسی چیست؟", "مشتری"),
        ("چه حیوانی بزرگترین جاندار خشکی است؟", "فیل"),
        ("کدام میوه قرمز است؟", "سیب"),
        ("چند ثانیه در یک دقیقه وجود دارد؟", "60")
    };

    public CaptchaChallenge GenerateChallenge()
    {
        var question = _questions[_random.Next(_questions.Length)];

        // ایجاد گزینه‌های تصادفی
        var wrongAnswers = _questions
            .Where(q => q.Answer != question.Answer)
            .OrderBy(x => _random.Next())
            .Take(3)
            .Select(q => q.Answer)
            .ToList();

        var allOptions = wrongAnswers.Concat(new[] { question.Answer })
                                   .OrderBy(x => _random.Next())
                                   .ToList();

        return new CaptchaChallenge
        {
            Question = question.Question,
            Answer = question.Answer,
            Options = allOptions,
            Type = CaptchaType.Logic
        };
    }
}