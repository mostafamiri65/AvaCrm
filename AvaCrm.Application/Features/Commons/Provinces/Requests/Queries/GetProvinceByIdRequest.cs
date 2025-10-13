using AvaCrm.Application.DTOs.Commons.Provinces;

namespace AvaCrm.Application.Features.Commons.Provinces.Requests.Queries;

public class GetProvinceByIdRequest : IRequest<ProvinceDto>
{
	public int ProvinceId { get; set; }
}
