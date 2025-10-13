using AvaCrm.Application.Features.Commons.Provinces.Requests.Commands;
using System.Net;

namespace AvaCrm.Application.Features.Commons.Provinces.Handlers.Commands;

public class DeleteProvinceHandler : IRequestHandler<DeleteProvinceRequest,
	GlobalResponse<ResponseResultGlobally>>
{
	private readonly IProvinceRepository _repository;

	public DeleteProvinceHandler(IProvinceRepository repository)
	{
		_repository = repository;
	}

	public async Task<GlobalResponse<ResponseResultGlobally>> Handle(DeleteProvinceRequest request, CancellationToken cancellationToken)
	{
		var res = await _repository.Delete(request.ProvinceId);
		if (res)
		{
			return new GlobalResponse<ResponseResultGlobally>()
			{
				Data = new ResponseResultGlobally { DoneSuccessfully = true },
				Message = "با موفقیت حذف شد",
				StatusCode = 200
			};
		}
		return new GlobalResponse<ResponseResultGlobally>()
		{
			Data = new ResponseResultGlobally { DoneSuccessfully = false },
			StatusCode = (int)HttpStatusCode.BadRequest,
			Message = "استان مورد نظر حذف نشد"
		};
	}
}
