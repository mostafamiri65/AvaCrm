using AvaCrm.Application.DTOs.ProjectManagement;
using AvaCrm.Application.Features.ProjectManagement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvaCrm.Api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class AttachmentsController : BaseController
    {
        private readonly IAttachmentService _attachmentService;
        public AttachmentsController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpGet("GetAttachments/{taskItemId}")]
        public async Task<IActionResult> GetAttachments(long taskItemId)
        {
            var result = await _attachmentService.GetAttachments(taskItemId);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost("UploadFile")]
        public async Task<ActionResult> UploadFile([FromForm] UploadFileDto uploadDto)
        {
            var res = await _attachmentService
                .UploadFile(uploadDto.File, uploadDto.DownloadedFileName, 
                uploadDto.TaskId, GetCurrentUserId());
            return StatusCode(res.StatusCode, res);
        }

        [HttpDelete("DeleteAttachment/{attachmentId}")]
        public async Task<ActionResult> DeleteAttachment(long attachmentId)
        {
            var res = await _attachmentService .DeleteAttachment(attachmentId);
            return StatusCode(res.StatusCode, res);
        }
    }
}
