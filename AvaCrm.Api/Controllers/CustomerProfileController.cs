using AvaCrm.Api.Helper;
using AvaCrm.Application.Features.CustomerManagement.Customers;
using AvaCrm.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerProfileController : BaseController
    {
        private readonly ICustomerProfileService _customerProfileService;

        public CustomerProfileController(ICustomerProfileService customerProfileService)
        {
            _customerProfileService = customerProfileService;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetCustomerProfile(long customerId, CancellationToken cancellationToken = default)
        {
            var response = await _customerProfileService.GetCustomerProfile(customerId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpGet("{customerId}/activities")]
        public async Task<IActionResult> GetCustomerActivities(
            long customerId,
            [FromQuery] PaginationRequest request,
            CancellationToken cancellationToken = default)
        {
            var response = await _customerProfileService.GetCustomerActivities(customerId, request, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpGet("{customerId}/stats")]
        public async Task<IActionResult> GetCustomerStats(long customerId, CancellationToken cancellationToken = default)
        {
            var response = await _customerProfileService.GetCustomerStats(customerId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }
    }
}
