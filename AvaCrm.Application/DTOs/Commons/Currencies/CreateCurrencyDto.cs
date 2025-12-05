using System.ComponentModel.DataAnnotations;

namespace AvaCrm.Application.DTOs.Commons.Currencies;

public class CreateCurrencyDto
{
    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;  // مثل USD ، IRR

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;  // مثل ریال ایران، یورو

    [MaxLength(5)]
    public string Symbol { get; set; } = string.Empty; // مثل ﷼ , $ , €

    /// <summary>
    /// تعداد رقم اعشار (مثلاً 2 برای دلار / 0 برای ریال)
    /// </summary>
    public int DecimalPlaces { get; set; } = 0;

    /// <summary>
    /// آیا ارز پیش‌فرض سیستم است؟
    /// </summary>
    public bool IsDefault { get; set; } = false;
}
