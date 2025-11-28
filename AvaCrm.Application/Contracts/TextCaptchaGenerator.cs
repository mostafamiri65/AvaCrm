using AvaCrm.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaCrm.Application.Contracts;

public class TextCaptchaGenerator : ICaptchaGenerator
{
    private readonly Random _random = new();
    public string GeneratorType => "Text";

    private readonly string[] _persianWords = {
        "سیب", "میز", "کتاب", "خورشید", "درخت", "آب", "هوا", "خانه",
        "ماشین", "گل", "دانش", "عشق", "دوست", "شهر", "رود", "کوه",
        "دریا", "ماه", "ستاره", "باران", "برف", "باد", "ابر", "مه"
    };

    public CaptchaChallenge GenerateChallenge()
    {
        var word = _persianWords[_random.Next(_persianWords.Length)];

        var patterns = new[]
        {
            new { Question = $"حرف سوم کلمه '{word}' چیست؟", Answer = GetThirdLetter(word) },
            new { Question = $"حرف اول کلمه '{word}' چیست؟", Answer = GetFirstLetter(word) },
            new { Question = $"حرف آخر کلمه '{word}' چیست؟", Answer = GetLastLetter(word) },
            new { Question = $"تعداد حروف کلمه '{word}' چند تا است؟", Answer = word.Length.ToString() },
            new { Question = $"اگر حروف کلمه '{word}' را برعکس کنیم چه می‌شود؟", Answer = ReverseWord(word) }
        };

        var selected = patterns[_random.Next(patterns.Length)];

        return new CaptchaChallenge
        {
            Question = selected.Question,
            Answer = selected.Answer,
            Type = CaptchaType.Text
        };
    }

    private string GetFirstLetter(string word) => word[0].ToString();
    private string GetLastLetter(string word) => word[^1].ToString();
    private string GetThirdLetter(string word) => word.Length >= 3 ? word[2].ToString() : "ندارد";
    private string ReverseWord(string word) => new string(word.Reverse().ToArray());
}
