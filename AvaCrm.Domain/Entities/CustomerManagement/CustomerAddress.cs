namespace AvaCrm.Domain.Entities.CustomerManagement;

public class CustomerAddress : BaseEntity
{
	public long CustomerId { get; set; }
	public int CountryId { get; set; }
	public int ProvinceId { get; set; }
	public int? CityId { get; set; }
	public string Street { get; set; } = string.Empty;
	public string AdditionalInfo { get; set; } = string.Empty;

	[ForeignKey(nameof(CustomerId))]
	public virtual Customer Customer { get; set; } = null!;
	[ForeignKey(nameof(CountryId))]
	public virtual Country Country { get; set; } = null!;
	[ForeignKey(nameof(ProvinceId))]
	public virtual Province Province { get; set; } = null!;
	[ForeignKey(nameof(CityId))]
	public virtual City? City { get; set; }
}
