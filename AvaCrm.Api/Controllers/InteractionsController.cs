using AvaCrm.Api.Helper;
using AvaCrm.Application.DTOs.CustomerManagement.Interactions;
using AvaCrm.Application.Features.CustomerManagement.Customers;
using AvaCrm.Application.Pagination;
using AvaCrm.Domain.Enums.CustomerManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class InteractionsController : BaseController
	{
		private readonly IInteractionService _interactionService;

		public InteractionsController(IInteractionService interactionService)
		{
			_interactionService = interactionService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken = default)
		{
			var response = await _interactionService.GetById(id, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpGet("by-customer/{customerId}")]
		public async Task<IActionResult> GetByCustomerId(long customerId, [FromQuery] PaginationRequest request, CancellationToken cancellationToken = default)
		{
			var response = await _interactionService.GetByCustomerId(customerId, request, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpGet("by-type/{interactionType}")]
		public async Task<IActionResult> GetByType(InteractionType interactionType, [FromQuery] PaginationRequest request, CancellationToken cancellationToken = default)
		{
			var response = await _interactionService.GetByType(interactionType, request, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] InteractionCreateDto createDto, CancellationToken cancellationToken = default)
		{
			var userId = GetCurrentUserId();
			var response = await _interactionService.Create(createDto, userId, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] InteractionUpdateDto updateDto, CancellationToken cancellationToken = default)
		{
			var userId = GetCurrentUserId();
			var response = await _interactionService.Update(updateDto, userId, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
		{
			var userId = GetCurrentUserId();
			var response = await _interactionService.Delete(id, userId, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}
	}
}
