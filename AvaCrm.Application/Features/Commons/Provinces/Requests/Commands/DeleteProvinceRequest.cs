using AvaCrm.Application.Responses;

namespace AvaCrm.Application.Features.Commons.Provinces.Requests.Commands;

public class DeleteProvinceRequest : IRequest<GlobalResponse<ResponseResultGlobally>>
{
	public int ProvinceId { get; set; }
}
