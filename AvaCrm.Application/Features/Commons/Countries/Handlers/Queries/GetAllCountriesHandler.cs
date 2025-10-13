using AvaCrm.Application.Features.Commons.Countries.Requests.Queries;

namespace AvaCrm.Application.Features.Commons.Countries.Handlers.Queries;

public class GetAllCountriesHandler : IRequestHandler<GetAllCountriesQuery, List<CountryDto>>
{
	private readonly ICountryRepository _repository;
	private readonly IMapper _mapper;
	public GetAllCountriesHandler(IMapper mapper, ICountryRepository repository)
	{
		_mapper = mapper;
		_repository = repository;
	}

	public async Task<List<CountryDto>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
	{
		return _mapper.Map<List<CountryDto>>(await _repository.GetAll());
	}
}
