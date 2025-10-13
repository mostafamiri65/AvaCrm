using AvaCrm.Application.DTOs.Commons.Provinces;
using AvaCrm.Application.Responses;

namespace AvaCrm.Application.Features.Commons.Provinces.Requests.Commands;

public class UpdateProvinceRequest : IRequest<GlobalResponse<ProvinceDto>>
{
	public required UpdateProvinceDto UpdateProvince { get; set; }
}
