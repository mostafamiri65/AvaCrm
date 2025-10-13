using AvaCrm.Api.Helper;
using AvaCrm.Application.DTOs.CustomerManagement.Customers;
using AvaCrm.Application.Features.CustomerManagement.Customers;
using AvaCrm.Application.Pagination;
using AvaCrm.Domain.Enums.CustomerManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers
{
    [Route("api/[controller]"),Authorize]
	public class CustomersController : BaseController
    {
		private readonly ICustomerService _customerService;

		public CustomersController(ICustomerService customerService)
		{
			_customerService = customerService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetCustomer(long id, CancellationToken cancellationToken = default)
		{
			var response = await _customerService.GetCustomerById(id, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllCustomers([FromQuery] PaginationRequest request, CancellationToken cancellationToken = default)
		{
            var userId = GetCurrentUserId();
			var response = await _customerService.GetAllCustomers(request,userId, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateCustomer([FromBody] CustomerCreateDto createDto, CancellationToken cancellationToken = default)
        {
            if (createDto.TypeOfCustomer == 1) createDto.CustomerType = CustomerType.Individual;
            else createDto.CustomerType = CustomerType.Organization;
			
			var userId = GetCurrentUserId();
			var response = await _customerService.CreateCustomer(createDto, userId, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateCustomer([FromBody] CustomerUpdateDto updateDto, CancellationToken cancellationToken = default)
		{
			var userId = GetCurrentUserId();
			var response = await _customerService.UpdateCustomer(updateDto, userId, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCustomer(long id, CancellationToken cancellationToken = default)
		{
			var userId = GetCurrentUserId();
			var response = await _customerService.DeleteCustomer(id, userId, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpGet("check-code-unique")]
		public async Task<IActionResult> CheckCodeUnique([FromQuery] string code, [FromQuery] long? excludeCustomerId = null, CancellationToken cancellationToken = default)
		{
			var response = await _customerService.IsCodeUnique(code, excludeCustomerId, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}
	}
}