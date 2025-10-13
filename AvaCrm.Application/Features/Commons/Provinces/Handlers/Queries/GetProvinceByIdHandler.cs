using AvaCrm.Application.DTOs.Commons.Provinces;
using AvaCrm.Application.Features.Commons.Provinces.Requests.Queries;

namespace AvaCrm.Application.Features.Commons.Provinces.Handlers.Queries;

public class GetProvinceByIdHandler : IRequestHandler<GetProvinceByIdRequest, ProvinceDto>
{
	private readonly IProvinceRepository _repository;

	public GetProvinceByIdHandler(IProvinceRepository repository)
	{
		_repository = repository;
	}

	public async Task<ProvinceDto> Handle(GetProvinceByIdRequest request, CancellationToken cancellationToken)
	{
		var province = await _repository.GetById(request.ProvinceId);
		if (province == null) return new ProvinceDto();
		return new ProvinceDto()
		{
			CountryId = province.CountryId,
			Id = province.Id,
			Name = province.Name,
		};
	}
}
