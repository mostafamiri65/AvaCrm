// src/app/dtos/ProjectManagement/attachment.model.ts
export interface AttachmentDto {
  taskId: number;
  fileName: string;
  filePath: string;
  downloadedFileName: string;
}

export interface UploadAttachmentDto {
  file: File;
  taskId: number;
  downloadedFileName: string;
}
