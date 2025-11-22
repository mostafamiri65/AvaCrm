using System;
using System.Collections.Generic;
using System.Text;

namespace AvaCrm.Application.DTOs.ProjectManagement;

public class AttachmentDto
{
    public long TaskId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public string DownloadedFileName { get; set; } = string.Empty;
}
