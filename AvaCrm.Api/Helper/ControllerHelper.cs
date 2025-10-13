using AvaCrm.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Helper
{
	public static class ControllerHelper
    {
        public static IActionResult ReturnResult<T>(GlobalResponse<T> response) where T : class
        {
            return response.StatusCode switch
            {
                200 => new OkObjectResult(response),
                201 => new ObjectResult(response) { StatusCode = 201 },
                400 => new BadRequestObjectResult(response),
                404 => new NotFoundObjectResult(response),
                _ => new ObjectResult(response) { StatusCode = response.StatusCode }
            };
        }
    }
}