namespace AvaCrm.Application.Models;

public class SecurityCheckRequest
{
    public string Username { get; set; } = string.Empty;
    public string? IpAddress { get; set; }
}
