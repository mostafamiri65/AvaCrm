using AvaCrm.Application.DTOs.Commons.Provinces;

namespace AvaCrm.Application.Features.Commons.Provinces.Requests.Queries;

public class GetAllProvincesRequest : IRequest<List<ProvinceDto>>
{
	public int CountryId { get; set; }
}
