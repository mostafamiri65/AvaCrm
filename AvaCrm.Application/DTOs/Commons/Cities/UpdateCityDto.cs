namespace AvaCrm.Application.DTOs.Commons.Cities;

public class UpdateCityDto
{
	public int Id { get; set; }
	public int ProvinceId { get; set; }
	public string Name { get; set; } = string.Empty;
}
