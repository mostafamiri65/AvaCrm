using AvaCrm.Api.Helper;
using AvaCrm.Application.DTOs.CustomerManagement.FollowUps;
using AvaCrm.Application.Features.CustomerManagement.Customers;
using AvaCrm.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class FollowUpsController : BaseController
	{
		private readonly IFollowUpService _followUpService;

		public FollowUpsController(IFollowUpService followUpService)
		{
			_followUpService = followUpService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken = default)
		{
			var response = await _followUpService.GetById(id, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpGet("by-customer/{customerId}")]
		public async Task<IActionResult> GetByCustomerId(long customerId, [FromQuery] PaginationRequest request, CancellationToken cancellationToken = default)
		{
			var response = await _followUpService.GetByCustomerId(customerId, request, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpGet("upcoming")]
		public async Task<IActionResult> GetUpcomingFollowUps([FromQuery] PaginationRequest request, CancellationToken cancellationToken = default)
		{
			var response = await _followUpService.GetUpcomingFollowUps(request, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] FollowUpCreateDto createDto, CancellationToken cancellationToken = default)
		{
			var userId = GetCurrentUserId();
			var response = await _followUpService.Create(createDto, userId, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] FollowUpUpdateDto updateDto, CancellationToken cancellationToken = default)
		{
			var userId = GetCurrentUserId();
			var response = await _followUpService.Update(updateDto, userId, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
		{
			var userId = GetCurrentUserId();
			var response = await _followUpService.Delete(id, userId, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}
	}
}
