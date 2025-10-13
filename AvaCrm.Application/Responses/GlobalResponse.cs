namespace AvaCrm.Application.Responses;

public class GlobalResponse<T> where T : class
{
	public int StatusCode { get; set; }
	public string? Message { get; set; }
	public T? Data { get; set; }
}
