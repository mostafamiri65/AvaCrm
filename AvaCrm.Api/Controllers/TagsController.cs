using AvaCrm.Api.Helper;
using AvaCrm.Application.DTOs.CustomerManagement.Tags;
using AvaCrm.Application.Features.CustomerManagement.Tags;
using AvaCrm.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class TagsController : BaseController
	{
		private readonly ITagService _tagService;

		public TagsController(ITagService tagService)
		{
			_tagService = tagService;
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken = default)
		{
			var response = await _tagService.GetById(id);
			return Ok(response);
		}

		[HttpGet]
		public async Task<IActionResult> GetAllTags([FromQuery] PaginationRequest request, CancellationToken cancellationToken = default)
		{
			var response = await _tagService.GetAllTags();
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] string title)
		{
			var userId = GetCurrentUserId();
			var response = await _tagService.CreateTag(title);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] TagDto updateDto, CancellationToken cancellationToken = default)
		{
			var userId = GetCurrentUserId();
			var response = await _tagService.UpdateTag(updateDto);
			return ControllerHelper.ReturnResult(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var userId = GetCurrentUserId();
			var response = await _tagService.DeleteTag(id);
			return ControllerHelper.ReturnResult(response);
		}
	}
}
