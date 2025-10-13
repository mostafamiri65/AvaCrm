namespace AvaCrm.Application.DTOs.Commons.Provinces;

public class UpdateProvinceDto
{
	public int Id { get; set; }
	public int CountryId { get; set; }
	public string Name { get; set; } = string.Empty;
}
