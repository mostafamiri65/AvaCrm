using AvaCrm.Application.DTOs.ProjectManagement;
using Microsoft.AspNetCore.Http;

namespace AvaCrm.Application.Features.ProjectManagement;

public interface IAttachmentService
{
    Task<GlobalResponse<ResponseResultGlobally>> UploadFile(IFormFile file, string downloadedFileName, long taskId, long userId);
    Task<GlobalResponse<List<AttachmentDto>>> GetAttachments(long taskItemId);
    Task<GlobalResponse<ResponseResultGlobally>> DeleteAttachment(long attachmentId);
}
