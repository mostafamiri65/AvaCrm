namespace AvaCrm.Application.Features.Commons.Countries.Requests.Commands;

public class CreateCountryCommand : IRequest<bool>
{
	public required CreateCountryDto CreateCountry { get; set; }
}
