namespace AvaCrm.Domain.Entities.Commons;

public class City
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public int ProvinceId { get; set; }

	[ForeignKey(nameof(ProvinceId))]
	public virtual Province Province { get; set; } = null!;
}
