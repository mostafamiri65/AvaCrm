using AvaCrm.Application.DTOs.Commons.Provinces;
using AvaCrm.Application.Features.Commons.Provinces.Requests.Queries;

namespace AvaCrm.Application.Features.Commons.Provinces.Handlers.Queries;

public class GetAllProvincesHandler : IRequestHandler<GetAllProvincesRequest, List<ProvinceDto>>
{
	private readonly IProvinceRepository _repository;

	public GetAllProvincesHandler(IProvinceRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<ProvinceDto>> Handle(GetAllProvincesRequest request, CancellationToken cancellationToken)
	{
		var list = await _repository.GetAll(request.CountryId);
		return list.Select(p => new ProvinceDto
		{
			CountryId = p.CountryId,
			Name = p.Name,
			Id = p.Id
		}).ToList();
	}
}
