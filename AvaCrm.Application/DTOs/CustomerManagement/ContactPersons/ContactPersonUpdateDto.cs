namespace AvaCrm.Application.DTOs.CustomerManagement.ContactPersons;

public class ContactPersonUpdateDto
{
    public long Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string JobTitle { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}