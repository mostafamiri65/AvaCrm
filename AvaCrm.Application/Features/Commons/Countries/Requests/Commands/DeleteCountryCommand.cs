namespace AvaCrm.Application.Features.Commons.Countries.Requests.Commands;

public class DeleteCountryCommand : IRequest<bool>
{
	public int CountryId { get; set; }
}
