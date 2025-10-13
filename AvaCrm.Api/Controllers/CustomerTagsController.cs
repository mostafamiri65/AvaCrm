using AvaCrm.Api.Helper;
using AvaCrm.Application.DTOs.CustomerManagement.Customers;
using AvaCrm.Application.Features.CustomerManagement.Tags;
using AvaCrm.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class CustomerTagsController : BaseController
	{
        private readonly ICustomerTagService _customerTagService;

        public CustomerTagsController(ICustomerTagService customerTagService)
        {
            _customerTagService = customerTagService;
        }

        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(long customerId, [FromQuery] PaginationRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _customerTagService.GetByCustomerId(customerId, request, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpGet("by-tag/{tagId}")]
        public async Task<IActionResult> GetByTagId(int tagId, [FromQuery] PaginationRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _customerTagService.GetByTagId(tagId, request, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddTagToCustomer([FromBody] CustomerTagCreateDto createDto, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _customerTagService.AddTagToCustomer(createDto, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> RemoveTagFromCustomer([FromQuery] long customerId, [FromQuery] int tagId, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _customerTagService.RemoveTagFromCustomer(customerId, tagId, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }
	}
}
