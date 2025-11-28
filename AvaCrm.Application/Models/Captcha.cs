namespace AvaCrm.Application.Models;

public class CaptchaChallenge
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Question { get; set; } = string.Empty;
    public string Answer { get; set; } = string.Empty;
    public List<string> Options { get; set; } = new List<string>();
    public CaptchaType Type { get; set; }
    public DateTime ExpiresAt { get; set; } = DateTime.Now.AddMinutes(5);
}
public enum CaptchaType
{
    Text,           // سوالات متنی
    Math,           // سوالات ریاضی
    Logic,          // سوالات منطقی
    Sequence,       // دنباله‌ها
    ImageRecognition // تشخیص تصویر/کلمه
}

public class CaptchaRequest
{
    public string CaptchaId { get; set; } = string.Empty;
    public string UserAnswer { get; set; } = string.Empty;
}

public class CaptchaResponse
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public CaptchaChallenge? Challenge { get; set; }
}
