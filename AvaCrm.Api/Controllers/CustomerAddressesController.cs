using AvaCrm.Api.Helper;
using AvaCrm.Application.DTOs.CustomerManagement.CustomerAddresses;
using AvaCrm.Application.Features.CustomerManagement.Customers;
using AvaCrm.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class CustomerAddressesController : BaseController
	{
        private readonly ICustomerAddressService _customerAddressService;

        public CustomerAddressesController(ICustomerAddressService customerAddressService)
        {
            _customerAddressService = customerAddressService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken = default)
        {
            var response = await _customerAddressService.GetById(id, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(long customerId, [FromQuery] PaginationRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _customerAddressService.GetByCustomerId(customerId, request, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CustomerAddressCreateDto createDto, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _customerAddressService.Create(createDto, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] CustomerAddressUpdateDto updateDto, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _customerAddressService.Update(updateDto, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _customerAddressService.Delete(id, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }
	}
}
