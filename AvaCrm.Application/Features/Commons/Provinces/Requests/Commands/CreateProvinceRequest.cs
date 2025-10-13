using AvaCrm.Application.DTOs.Commons.Provinces;
using AvaCrm.Application.Responses;

namespace AvaCrm.Application.Features.Commons.Provinces.Requests.Commands;

public class CreateProvinceRequest : IRequest<GlobalResponse<ProvinceDto>>
{
	public required CreateProvinceDto CreateProvince { get; set; }
}
