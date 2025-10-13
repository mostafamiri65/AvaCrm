using AvaCrm.Api.Helper;
using AvaCrm.Application.DTOs.CustomerManagement.ContactPersons;
using AvaCrm.Application.Features.CustomerManagement.Customers;
using AvaCrm.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class ContactPersonsController : BaseController
	{
        private readonly IContactPersonService _contactPersonService;

        public ContactPersonsController(IContactPersonService contactPersonService)
        {
            _contactPersonService = contactPersonService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken = default)
        {
            var response = await _contactPersonService.GetById(id, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(long customerId, [FromQuery] PaginationRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _contactPersonService.GetByCustomerId(customerId, request, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContactPersonCreateDto createDto, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _contactPersonService.Create(createDto, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ContactPersonUpdateDto updateDto, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _contactPersonService.Update(updateDto, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _contactPersonService.Delete(id, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }
	}
}
