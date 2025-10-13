using AvaCrm.Application.DTOs.Commons.Provinces;
using AvaCrm.Application.Features.Commons.Provinces.Requests.Commands;
using System.Net;

namespace AvaCrm.Application.Features.Commons.Provinces.Handlers.Commands;

public class UpdateResponseHandler : IRequestHandler<UpdateProvinceRequest,
	GlobalResponse<ProvinceDto>>
{
	private readonly IProvinceRepository _repository;

	public UpdateResponseHandler(IProvinceRepository repository)
	{
		_repository = repository;
	}

	public async Task<GlobalResponse<ProvinceDto>> Handle(UpdateProvinceRequest request, CancellationToken cancellationToken)
	{
		var isExist = await _repository.IsExist(request.UpdateProvince.Name,
		request.UpdateProvince.CountryId, request.UpdateProvince.Id);
		if (isExist)
		{
			return new GlobalResponse<ProvinceDto>()
			{
				Message = "استان وارد شده تکراری است",
				StatusCode = (int)HttpStatusCode.BadRequest
			};
		}
		var data = await _repository.GetById(request.UpdateProvince.Id);
		if (data == null)
		{
			return new GlobalResponse<ProvinceDto>()
			{
				Message = "استان مورد نظر یافت نشد",
				StatusCode = (int)HttpStatusCode.NotFound
			};
		}
		data.Name = request.UpdateProvince.Name;
		data.CountryId = request.UpdateProvince.CountryId;

		var res = await _repository.Update(data);
		return new GlobalResponse<ProvinceDto>()
		{
			Message = "استان مورد نظر با موفقیت ویرایش شد",
			StatusCode = 200,
			Data = new ProvinceDto()
			{ Id = data.Id, Name = data.Name, CountryId = data.CountryId }
		};
	}
}
