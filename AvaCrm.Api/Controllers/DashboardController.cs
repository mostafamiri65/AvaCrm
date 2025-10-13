using AvaCrm.Api.Helper;
using AvaCrm.Application.Features.Dashboard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class DashboardController : BaseController
	{
		private readonly IDashboardService _dashboardService;

		public DashboardController(IDashboardService dashboardService)
		{
			_dashboardService = dashboardService;
		}

		[HttpGet("summary")]
		public async Task<IActionResult> GetDashboardSummary(CancellationToken cancellationToken = default)
		{
			var userId = GetCurrentUserId();
			var response = await _dashboardService.GetDashboardSummaryAsync(userId, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpGet("upcoming-activities")]
		public async Task<IActionResult> GetUpcomingActivities([FromQuery] int count = 10, CancellationToken cancellationToken = default)
		{
			var userId = GetCurrentUserId();
			var response = await _dashboardService.GetUpcomingActivitiesAsync(userId, count, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpGet("recent-activities")]
		public async Task<IActionResult> GetRecentActivities([FromQuery] int days = 7, CancellationToken cancellationToken = default)
		{
			var userId = GetCurrentUserId();
			var response = await _dashboardService.GetRecentActivitiesAsync(userId, days, cancellationToken);
			return ControllerHelper.ReturnResult(response);
		}

	}
}
