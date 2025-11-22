using AvaCrm.Application.DTOs.ProjectManagement;
using AvaCrm.Application.Responses;
using AvaCrm.Domain.Contracts.ProjectManagement;
using AvaCrm.Domain.Entities.ProjectManagement;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Generic;
using System.Net;

namespace AvaCrm.Application.Features.ProjectManagement;

public class AttachmentService : IAttachmentService
{
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IFileStorage _fileStorage;
    private readonly IFileValidator _fileValidator;
    private readonly IMapper _mapper;
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IActivityLogRepository _activityLogRepository;
    public AttachmentService(IAttachmentRepository attachmentRepository, IFileValidator fileValidator,
        IFileStorage fileStorage, IMapper mapper, ITaskItemRepository taskItemRepository, IActivityLogRepository activityLogRepository)
    {
        _attachmentRepository = attachmentRepository;
        _fileValidator = fileValidator;
        _fileStorage = fileStorage;
        _mapper = mapper;
        _taskItemRepository = taskItemRepository;
        _activityLogRepository = activityLogRepository;
    }

    public async Task<GlobalResponse<ResponseResultGlobally>> DeleteAttachment(long attachmentId)
    {
        var attachment = await _attachmentRepository.GetById(attachmentId);
        if (attachment == null)
        {
            return new GlobalResponse<ResponseResultGlobally>()
            {
                Message = "فایل مورد نظر یافت نشد",
                StatusCode = (int)HttpStatusCode.NotFound
            };
        }
        var delete = await _fileStorage.DeleteAsync(attachment.FileName);
        if (delete)
        {
            return new GlobalResponse<ResponseResultGlobally>()
            {
                Message = "فایل مورد نظر حذف شد",
                StatusCode = (int)HttpStatusCode.OK
            };
        }
        return new GlobalResponse<ResponseResultGlobally>()
        {
            Message = "مسیر فایل مورد نظر یافت نشد",
            StatusCode = (int)HttpStatusCode.BadRequest
        };
    }

    public async Task<GlobalResponse<List<AttachmentDto>>> GetAttachments(long taskItemId)
    {
        try
        {
            var list = await _attachmentRepository.GetAttachmentsByTaskId(taskItemId);

            return new GlobalResponse<List<AttachmentDto>>()
            {
                Data = _mapper.Map<List<AttachmentDto>>(list),
                StatusCode = (int)HttpStatusCode.OK,
                Message = "با موفقیت دریافت شد"

            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<List<AttachmentDto>>()
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = $"خطا : {ex.Message}"

            };
        }
    }

    public async Task<GlobalResponse<ResponseResultGlobally>> UploadFile(IFormFile file,
        string downloadedFileName, long taskId, long userId)
    {
        try
        {
            var task = await _taskItemRepository.GetById(taskId);
            if (task == null)
            {
                return new GlobalResponse<ResponseResultGlobally>()
                {
                    Message = $"تسک یافت نشد",
                    StatusCode = (int)HttpStatusCode.NotFound,

                };
            }
            var validator = await _fileValidator.ValidateAsync(file);
            if (!validator.IsValid)
            {
                return new GlobalResponse<ResponseResultGlobally>()
                {
                    Message = $"{string.Join(',', validator.Errors)}",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                };
            }
            var fileExtension = Path.GetExtension(file.FileName);

            var fileName = Guid.NewGuid().ToString().Replace("-", "");
            fileName = fileName  + fileExtension;
            var path = await _fileStorage.SaveAsync(file, fileName);
            Attachment attachment = new Attachment()
            {
                FileName = fileName,
                DownloadedFileName = downloadedFileName,
                FilePath = path,
                TaskId = taskId
            };
            await _attachmentRepository.Create(attachment, userId);

            ActivityLog activityLog = new ActivityLog()
            {
                Action = "آپلود فایل",
                Details = $"کاربر با شناسه {userId} به تسک با شناسه {taskId} فایل اضافه کرد",
                ProjectId = task.ProjectId
            };
            await _activityLogRepository.Create(activityLog, userId);

            return new GlobalResponse<ResponseResultGlobally>()
            {
                Message = "آپلود با موفقیت انجام شد",
                StatusCode = (int)HttpStatusCode.OK,
            };
        }
        catch (Exception ex)
        {
            return new GlobalResponse<ResponseResultGlobally>()
            {
                Message = "خطا در آپلود فایل " + ex.Message,
                StatusCode = (int)HttpStatusCode.InternalServerError,
            };
        }

    }
}
