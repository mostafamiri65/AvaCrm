using AvaCrm.Application.Features.Commons.Countries.Requests.Queries;

namespace AvaCrm.Application.Features.Commons.Countries.Handlers.Queries;

public class GetCountryByIdHandler : IRequestHandler<GetCountryByIdQuery, CountryDto?>
{
	private readonly ICountryRepository _repository;
	private readonly IMapper _mapper;
	public GetCountryByIdHandler(ICountryRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<CountryDto?> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
	{
		var res = await _repository.GetById(request.CountryId);
		if (res == null) return null;
		return _mapper.Map<CountryDto>(res);
	}
}
