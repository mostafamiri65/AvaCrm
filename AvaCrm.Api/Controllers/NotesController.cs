using AvaCrm.Api.Helper;
using AvaCrm.Application.DTOs.CustomerManagement.Notes;
using AvaCrm.Application.Features.CustomerManagement.Customers;
using AvaCrm.Application.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers
{
	[Route("api/[controller]")]
	[Authorize]
	public class NotesController : BaseController
	{
        private readonly INoteService _noteService;

        public NotesController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken = default)
        {
            var response = await _noteService.GetById(id, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpGet("by-customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(long customerId, [FromQuery] PaginationRequest request, CancellationToken cancellationToken = default)
        {
            var response = await _noteService.GetByCustomerId(customerId, request, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NoteCreateDto createDto, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _noteService.Create(createDto, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] NoteUpdateDto updateDto, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _noteService.Update(updateDto, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken = default)
        {
            var userId = GetCurrentUserId();
            var response = await _noteService.Delete(id, userId, cancellationToken);
            return ControllerHelper.ReturnResult(response);
        }
	}
}
