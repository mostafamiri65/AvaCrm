using AvaCrm.Api.Helper;
using AvaCrm.Application.DTOs.CustomerManagement.IndividualCustomers;
using AvaCrm.Application.Features.CustomerManagement.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class IndividualCustomersController : BaseController
	{
        private readonly IIndividualCustomerService _individualCustomerService;

        public IndividualCustomersController(IIndividualCustomerService individualCustomerService)
        {
            _individualCustomerService = individualCustomerService;
        }

        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(long customerId, CancellationToken cancellationToken = default)
        {
            var response = await _individualCustomerService.GetByCustomerId(customerId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IndividualCustomerCreateDto createDto, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _individualCustomerService.Create(createDto, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] IndividualCustomerUpdateDto updateDto, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _individualCustomerService.Update(updateDto, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _individualCustomerService.Delete(id, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }
	}
}
