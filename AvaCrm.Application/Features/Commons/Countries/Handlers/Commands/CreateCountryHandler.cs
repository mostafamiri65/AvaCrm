using AvaCrm.Application.Features.Commons.Countries.Requests.Commands;

namespace AvaCrm.Application.Features.Commons.Countries.Handlers.Commands;

public class CreateCountryHandler : IRequestHandler<CreateCountryCommand, bool>
{
	private readonly ICountryRepository _repository;
	private readonly IMapper _mapper;
	public CreateCountryHandler(ICountryRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
	{
		var country = _mapper.Map<Country>(request.CreateCountry);
		var res = await _repository.CreateCountry(country.Name);
		return res;
	}
}
