using AvaCrm.Application.DTOs.Commons.Provinces;
using AvaCrm.Application.Features.Commons.Provinces.Requests.Commands;
using AvaCrm.Application.Features.Commons.Provinces.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class ProvincesController : BaseController
{
	private readonly IMediator _mediator;
	public ProvincesController(IMediator mediator)
	{
		_mediator = mediator;
	}
	[HttpGet("GetAllProvinces/{countryId}")]
	public async Task<ActionResult<List<ProvinceDto>>> GetProvinces(int countryId)
	{
		var request = new GetAllProvincesRequest() { CountryId = countryId };
		var res = await _mediator.Send(request);
		return Ok(res);
	}

	[HttpGet("ProvinceById/{id}")]
	public async Task<ActionResult<ProvinceDto>> ProvinceById(int id)
	{
		var request = new GetProvinceByIdRequest() { ProvinceId = id };
		var res = await _mediator.Send(request);
		return Ok(res);
	}

	[HttpPost("Create")]
	public async Task<ActionResult<bool>> CreateProvince(CreateProvinceDto dto)
	{
		var request = new CreateProvinceRequest() { CreateProvince = dto };
		var res = await _mediator.Send(request);
		return StatusCode(res.StatusCode, res);
	}

	[HttpPut("Update")]
	public async Task<ActionResult<bool>> UpdateProvince(UpdateProvinceDto dto)
	{
		var request = new UpdateProvinceRequest() { UpdateProvince = dto };
		var res = await _mediator.Send(request);
		return StatusCode(res.StatusCode, res);
	}

	[HttpDelete("Delete/{id}")]
	public async Task<ActionResult<bool>> DeleteProvince(int id)
	{
		var request = new DeleteProvinceRequest() { ProvinceId = id };
		var res = await _mediator.Send(request);
		return StatusCode(res.StatusCode, res);
	}
}
