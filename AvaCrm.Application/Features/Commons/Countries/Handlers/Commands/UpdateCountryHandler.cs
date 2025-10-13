using AvaCrm.Application.Features.Commons.Countries.Requests.Commands;

namespace AvaCrm.Application.Features.Commons.Countries.Handlers.Commands;

public class UpdateCountryHandler : IRequestHandler<UpdateCountryCommand, bool>
{
	private readonly ICountryRepository _repository;
	public UpdateCountryHandler(ICountryRepository repository)
	{
		_repository = repository;
	}

	public async Task<bool> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
	{
		var country = await _repository.GetById(request.UpdateCountry.Id);
		if (country == null) return false;
		country.Name = request.UpdateCountry.Name;
		return await _repository.UpdateCountry(country);
	}
}
