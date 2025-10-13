using AvaCrm.Application.Features.Commons.Countries.Requests.Commands;

namespace AvaCrm.Application.Features.Commons.Countries.Handlers.Commands;

public class DeleteCountryHandler : IRequestHandler<DeleteCountryCommand, bool>
{
	private readonly ICountryRepository _repository;
	public DeleteCountryHandler(ICountryRepository repository)
	{
		_repository = repository;
	}

	public Task<bool> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
	{
		return _repository.DeleteCountry(request.CountryId);
	}
}
