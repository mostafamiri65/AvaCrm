using AvaCrm.Api.Helper;
using AvaCrm.Application.DTOs.CustomerManagement.OrganizationCustomers;
using AvaCrm.Application.Features.CustomerManagement.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class OrganizationCustomersController : BaseController
	{
        private readonly IOrganizationCustomerService _organizationCustomerService;

        public OrganizationCustomersController(IOrganizationCustomerService organizationCustomerService)
        {
            _organizationCustomerService = organizationCustomerService;
        }

        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(long customerId, CancellationToken cancellationToken = default)
        {
            var response = await _organizationCustomerService.GetByCustomerId(customerId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrganizationCustomerCreateDto createDto, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _organizationCustomerService.Create(createDto, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OrganizationCustomerUpdateDto updateDto, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _organizationCustomerService.Update(updateDto, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _organizationCustomerService.Delete(id, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }
	}
}
