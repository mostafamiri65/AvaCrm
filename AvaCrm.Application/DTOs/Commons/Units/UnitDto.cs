using AvaCrm.Domain.Enums.Commons;
using System.ComponentModel.DataAnnotations;

namespace AvaCrm.Application.DTOs.Commons.Units;

public class UnitDto
{
    public long Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    public UnitCategory Category { get; set; } = UnitCategory.General;

}
