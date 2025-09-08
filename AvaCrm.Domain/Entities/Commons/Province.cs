namespace AvaCrm.Domain.Entities.Commons;

public class Province
{
	public int Id { get; set; }
	public int CountryId { get; set; }
	public string Name { get; set; } = string.Empty;

	[ForeignKey(nameof(CountryId))]
	public virtual Country Country { get; set; } = null!;
}
