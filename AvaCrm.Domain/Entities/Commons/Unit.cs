using AvaCrm.Domain.Enums.Commons;

namespace AvaCrm.Domain.Entities.Commons;

public class Unit : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(10)]
    public string Code { get; set; } = string.Empty;

    public UnitCategory Category { get; set; } = UnitCategory.General;

}
