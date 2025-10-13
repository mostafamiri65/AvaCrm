namespace AvaCrm.Application.Features.Commons.Countries.Requests.Commands;

public class UpdateCountryCommand : IRequest<bool>
{
	public required UpdateCountryDto UpdateCountry { get; set; }
}
