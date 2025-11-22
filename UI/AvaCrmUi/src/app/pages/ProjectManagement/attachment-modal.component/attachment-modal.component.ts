import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {AttachmentDto} from '../../../dtos/ProjectManagement/attachment.model';
import {AttachmentService} from '../../../services/attachment.service';
import {ApiAddressUtility} from '../../../utilities/api-address.utility';
import {FormsModule} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';
import {DomSanitizer, SafeResourceUrl} from '@angular/platform-browser';

@Component({
  selector: 'app-attachment-modal',
  imports: [
    FormsModule,
    NgIf,
    NgForOf
  ],
  templateUrl: './attachment-modal.component.html',
  styleUrl: './attachment-modal.component.css'
})
export class AttachmentModalComponent implements OnInit {
  @Input() taskId!: number;
  @Input() taskTitle: string = '';
  @Output() modalClose = new EventEmitter<void>();

  attachments: AttachmentDto[] = [];
  loading = false;
  uploading = false;
  uploadProgress = 0;

  // Upload properties
  selectedFile: File | null = null;
  downloadedFileName: string = '';

  // Preview properties
  selectedAttachment: AttachmentDto | null = null;
  previewUrl: SafeResourceUrl | null = null;
  previewType: 'image' | 'pdf' | 'text' | 'unsupported' = 'unsupported';

  constructor(
    private attachmentService: AttachmentService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    this.loadAttachments();
  }

  loadAttachments(): void {
    this.loading = true;
    this.attachmentService.getAttachments(this.taskId).subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          this.attachments = response.data || [];
        }
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading attachments:', error);
        this.loading = false;
      }
    });
  }

  onFileSelected(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
      // پیش‌فرض نام فایل برای دانلود
      if (!this.downloadedFileName) {
        this.downloadedFileName = file.name;
      }
    }
  }

  uploadFile(): void {
    if (!this.selectedFile) {
      alert('لطفا یک فایل انتخاب کنید');
      return;
    }

    if (!this.downloadedFileName.trim()) {
      alert('لطفا نام نمایشی فایل را وارد کنید');
      return;
    }

    this.uploading = true;
    this.uploadProgress = 0;

    this.attachmentService.uploadFile(
      this.selectedFile,
      this.taskId,
      this.downloadedFileName.trim()
    ).subscribe({
      next: (event: any) => {
        if (event.type === 1) { // HttpEventType.UploadProgress
          this.uploadProgress = Math.round(100 * event.loaded / event.total);
        } else if (event.type === 4) { // HttpEventType.Response
          const response = event.body;

          if (response.statusCode === 200) {
            // Reset form
            this.selectedFile = null;
            this.downloadedFileName = '';
            this.uploadProgress = 0;
            // Reset file input
            const fileInput = document.getElementById('fileInput') as HTMLInputElement;
            if (fileInput) fileInput.value = '';

            this.loadAttachments(); // بارگذاری مجدد لیست
            alert('فایل با موفقیت آپلود شد');
          } else {
            alert('خطا در آپلود فایل: ' + (response.message || 'خطای ناشناخته'));
          }
          this.uploading = false;
        }
      },
      error: (error) => {
        console.error('Upload error:', error);
        alert('خطا در آپلود فایل');
        this.uploading = false;
        this.uploadProgress = 0;
      }
    });
  }

  // پیش‌نمایش فایل
  previewAttachment(attachment: AttachmentDto): void {
    this.selectedAttachment = attachment;
    this.determinePreviewType(attachment.fileName);

    // ایجاد URL برای پیش‌نمایش
    // فرض می‌کنیم فایل‌ها از طریق API در دسترس هستند
    const fileUrl = this.getFileUrl(attachment);
    this.previewUrl = this.sanitizer.bypassSecurityTrustResourceUrl(fileUrl);
  }

  // تعیین نوع فایل برای پیش‌نمایش
  private determinePreviewType(fileName: string): void {
    const extension = fileName.toLowerCase().split('.').pop();

    if (['jpg', 'jpeg', 'png', 'gif', 'bmp', 'webp'].includes(extension || '')) {
      this.previewType = 'image';
    } else if (extension === 'pdf') {
      this.previewType = 'pdf';
    } else if (['txt', 'csv', 'json', 'xml'].includes(extension || '')) {
      this.previewType = 'text';
    } else {
      this.previewType = 'unsupported';
    }
  }

  // ایجاد URL فایل برای پیش‌نمایش و دانلود
  private getFileUrl(attachment: AttachmentDto): string {
    // این آدرس بستگی به endpoint دانلود در backend شما دارد
    // فرض می‌کنیم endpoint ای به نام DownloadFile دارید
    return `${ApiAddressUtility.BaseAddress}/Attachments/DownloadFile/${this.extractAttachmentId(attachment.filePath)}`;
  }

  closePreview(): void {
    this.selectedAttachment = null;
    this.previewUrl = null;
  }

  downloadAttachment(attachment: AttachmentDto): void {
    const fileUrl = this.getFileUrl(attachment);
    window.open(fileUrl, '_blank');
  }

  deleteAttachment(attachment: AttachmentDto): void {
    if (confirm(`آیا از حذف فایل "${attachment.downloadedFileName}" اطمینان دارید؟`)) {
      const attachmentId = this.extractAttachmentId(attachment.filePath);

      if (attachmentId) {
        this.attachmentService.deleteAttachment(attachmentId).subscribe({
          next: (response) => {
            if (response.statusCode === 200) {
              // اگر فایل در حال پیش‌نمایش حذف شد، پیش‌نمایش را ببند
              if (this.selectedAttachment?.filePath === attachment.filePath) {
                this.closePreview();
              }
              this.loadAttachments(); // بارگذاری مجدد لیست
              alert('فایل با موفقیت حذف شد');
            } else {
              alert('خطا در حذف فایل: ' + response.message);
            }
          },
          error: (error) => {
            console.error('Delete error:', error);
            alert('خطا در حذف فایل');
          }
        });
      } else {
        alert('خطا در شناسایی فایل');
      }
    }
  }

  private extractAttachmentId(filePath: string): number {
    try {
      const parts = filePath.split('/');
      const lastPart = parts[parts.length - 1];
      const fileNameWithoutExtension = lastPart.split('.')[0];
      return parseInt(fileNameWithoutExtension) || 0;
    } catch {
      return 0;
    }
  }

  getFileIcon(fileName: string): string {
    const extension = fileName.split('.').pop()?.toLowerCase();

    switch (extension) {
      case 'pdf':
        return 'fas fa-file-pdf text-danger';
      case 'doc':
      case 'docx':
        return 'fas fa-file-word text-primary';
      case 'xls':
      case 'xlsx':
        return 'fas fa-file-excel text-success';
      case 'ppt':
      case 'pptx':
        return 'fas fa-file-powerpoint text-warning';
      case 'jpg':
      case 'jpeg':
      case 'png':
      case 'gif':
      case 'bmp':
      case 'webp':
        return 'fas fa-file-image text-info';
      case 'zip':
      case 'rar':
      case '7z':
        return 'fas fa-file-archive text-warning';
      case 'txt':
      case 'csv':
      case 'json':
      case 'xml':
        return 'fas fa-file-alt text-secondary';
      case 'mp4':
      case 'avi':
      case 'mov':
        return 'fas fa-file-video text-danger';
      case 'mp3':
      case 'wav':
        return 'fas fa-file-audio text-success';
      default:
        return 'fas fa-file text-muted';
    }
  }

  getFileSize(bytes: number): string {
    if (!bytes || bytes === 0) return '0 Bytes';

    const k = 1024;
    const sizes = ['Bytes', 'KB', 'MB', 'GB'];
    const i = Math.floor(Math.log(bytes) / Math.log(k));

    return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i];
  }

  closeModal(): void {
    this.modalClose.emit();
  }

  getFileNameFromPath(filePath: string): string {
    return filePath.split('/').pop() || filePath;
  }

  // بررسی اینکه آیا فایل قابل پیش‌نمایش است
  isPreviewable(fileName: string): boolean {
    const extension = fileName.toLowerCase().split('.').pop();
    const previewableExtensions = ['jpg', 'jpeg', 'png', 'gif', 'bmp', 'webp', 'pdf', 'txt', 'csv', 'json', 'xml'];
    return previewableExtensions.includes(extension || '');
  }
}
