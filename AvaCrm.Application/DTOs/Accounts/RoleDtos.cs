namespace AvaCrm.Application.DTOs.Accounts;


public class RoleListDto
{
	public long Id { get; set; }
	public string? TitleEnglish { get; set; }
	public string? TitlePersian { get; set; }
	public DateTime CreatedDate { get; set; }
}

public class RoleCreateDto
{
	public string? TitleEnglish { get; set; }
	public string? TitlePersian { get; set; }
}

public class RoleUpdateDto
{
	public long Id { get; set; }
	public string? TitleEnglish { get; set; }
	public string? TitlePersian { get; set; }
}