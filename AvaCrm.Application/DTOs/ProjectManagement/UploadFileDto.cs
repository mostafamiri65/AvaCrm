using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaCrm.Application.DTOs.ProjectManagement;

public class UploadFileDto
{
    public IFormFile File { get; set; }
    public long TaskId { get; set; }
    public string DownloadedFileName { get; set; }
}
