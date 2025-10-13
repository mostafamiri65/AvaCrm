using AvaCrm.Application.DTOs.Commons.Provinces;
using AvaCrm.Application.Features.Commons.Provinces.Requests.Commands;
using System.Net;

namespace AvaCrm.Application.Features.Commons.Provinces.Handlers.Commands;

public class CreateProvinceHandler : IRequestHandler<CreateProvinceRequest,
													  GlobalResponse<ProvinceDto>>
{
	private readonly IProvinceRepository _provinceRepository;

	public CreateProvinceHandler(IProvinceRepository provinceRepository)
	{
		_provinceRepository = provinceRepository;
	}

	public async Task<GlobalResponse<ProvinceDto>> Handle(CreateProvinceRequest request,
		CancellationToken cancellationToken)
	{
		var isExist = await _provinceRepository.IsExist(request.CreateProvince.Name,
			request.CreateProvince.CountryId, 0);
		if (isExist)
		{
			return new GlobalResponse<ProvinceDto>()
			{
				Message = "استان وارد شده تکراری است",
				StatusCode = (int)HttpStatusCode.BadRequest
			};
		}
		var province = new Province()
		{
			Name = request.CreateProvince.Name,
			CountryId = request.CreateProvince.CountryId,
		};
		var data = await _provinceRepository.Create(province);
		return new GlobalResponse<ProvinceDto>()
		{
			Message = "استان مورد نظر با موفقیت اضافه شد",
			StatusCode = 200,
			Data = new ProvinceDto()
			{ Id = data.Id, Name = data.Name, CountryId = data.CountryId }
		};
	}
}
