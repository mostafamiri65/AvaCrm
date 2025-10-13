using AvaCrm.Application.DTOs.Commons.Countries;
using AvaCrm.Application.Features.Commons.Countries.Requests.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.MVC.Controllers
{
	public class CountryController : Controller
	{
		private readonly IMediator _mediator;
		public CountryController(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task<ActionResult<List<CountryDto>>> Index()
		{
			var list = await _mediator.Send(new GetAllCountriesQuery());
			return View(list);
		}

		public IActionResult Create()
		{
			return View();
		}
	}
}
