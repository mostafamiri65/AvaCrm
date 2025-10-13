namespace AvaCrm.Application.Features.Commons.Countries.Requests.Queries;

public class GetCountryByIdQuery : IRequest<CountryDto?>
{
	public int CountryId { get; set; }
}
