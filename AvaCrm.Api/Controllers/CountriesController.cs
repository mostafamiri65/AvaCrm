using AvaCrm.Application.DTOs.Commons.Countries;
using AvaCrm.Application.Features.Commons.Countries.Requests.Commands;
using AvaCrm.Application.Features.Commons.Countries.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers;

[Route("api/[controller]")]
[Authorize]
public class CountriesController : BaseController
{
	private readonly IMediator _mediator;
	public CountriesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet("GetAllCountries")]
	public async Task<ActionResult<List<CountryDto>>> GetCountries()
	{
		var request = new GetAllCountriesQuery();
		var res = await _mediator.Send(request);
		return Ok(res);
	}

	[HttpGet("CountryById/{id}")]
	public async Task<ActionResult<CountryDto>> CountryById(int id)
	{
		var request = new GetCountryByIdQuery() { CountryId = id};
		var res = await _mediator.Send(request);
		return Ok(res);
	}

	[HttpPost("Create")]
	public async Task<ActionResult<bool>> CreateCountry(CreateCountryDto dto)
	{
		var request = new CreateCountryCommand() { CreateCountry =dto };
		var res = await _mediator.Send(request);
		if(res) return Ok(res);
		else return BadRequest(res);
	}

	[HttpPut("Update")]
	public async Task<ActionResult<bool>> UpdateCountry(UpdateCountryDto dto)
	{
		var request = new UpdateCountryCommand() { UpdateCountry = dto };
		var res = await _mediator.Send(request);
		if (res) return Ok(res);
		else return BadRequest(res);
	}

	[HttpDelete("Delete/{id}")]
	public async Task<ActionResult<bool>> DeleteCountry(int id)
	{
		var request = new DeleteCountryCommand() { CountryId = id };
		var res = await _mediator.Send(request);
		if (res) return Ok(res);
		else return BadRequest(res);
	}
}
