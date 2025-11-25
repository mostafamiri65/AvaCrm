import {Component, ElementRef, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {CommentDto, CreateCommentDto, UpdateCommentDto} from '../../../dtos/ProjectManagement/comment.models';
import {UserListDto} from '../../../dtos/accounts/user.models';
import {CommentService} from '../../../services/comment.service';
import {UserService} from '../../../services/user.service';
import {DateService} from '../../../services/utility/date';
import {NgForOf, NgIf} from '@angular/common';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-comment-modal',
  imports: [
    NgIf,
    NgForOf,
    CommonModule, ReactiveFormsModule
  ],
  templateUrl: './comment-modal.component.html',
  styleUrl: './comment-modal.component.css'
})
export class CommentModalComponent implements OnInit {
  @Input() taskId!: number;
  @Input() taskTitle!: string;
  @Output() modalClose = new EventEmitter<void>();

  @ViewChild('commentsContainer') commentsContainer!: ElementRef;

  comments: CommentDto[] = [];
  users: UserListDto[] = [];
  loading = false;
  addingComment = false;

  newCommentContent = '';
  editingCommentId: number | null = null;
  editCommentContent = '';

  // اطلاعات صفحه‌بندی
  currentPage = 1;
  pageSize = 100;
  totalCount = 0;
  totalPages = 0;
  hasNextPage = false;
  hasPreviousPage = false;
  editCommentForm!: FormGroup;
  newCommentForm!: FormGroup;

  constructor(
    private commentService: CommentService,
    private userService: UserService,
    private dateService: DateService,
    private fb: FormBuilder // اضافه کردن FormBuilder
  ) {}

  ngOnInit(): void {
    this.initForms();
    this.loadComments();
    this.loadUsers();
  }

  initForms(): void {
    this.newCommentForm = this.fb.group({
      content: ['', [Validators.required, Validators.minLength(1)]]
    });

    this.editCommentForm = this.fb.group({
      content: ['', [Validators.required, Validators.minLength(1)]]
    });
  }

  loadComments(): void {
    this.loading = true;
    this.commentService.getCommentsByTaskId(this.taskId, this.currentPage, this.pageSize).subscribe({
      next: (response) => {
        if (response.statusCode === 200 && response.data) {
          this.comments = response.data.items;
          this.totalCount = response.data.totalCount;
          this.totalPages = response.data.totalPages;
          this.hasNextPage = response.data.hasNextPage;
          this.hasPreviousPage = response.data.hasPreviousPage;
          this.scrollToBottom();
        } else {
          this.comments = [];
        }
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.comments = [];
      }
    });
  }

  loadMoreComments(): void {
    if (this.hasNextPage && !this.loading) {
      this.currentPage++;
      this.loading = true;

      this.commentService.getCommentsByTaskId(this.taskId, this.currentPage, this.pageSize).subscribe({
        next: (response) => {
          if (response.statusCode === 200 && response.data) {
            this.comments = [...this.comments, ...response.data.items];
            this.totalCount = response.data.totalCount;
            this.totalPages = response.data.totalPages;
            this.hasNextPage = response.data.hasNextPage;
            this.hasPreviousPage = response.data.hasPreviousPage;
          }
          this.loading = false;
        },
        error: () => {
          this.loading = false;
        }
      });
    }
  }

  loadUsers(): void {
    this.userService.getAll().subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          this.users = response.data || [];
        }
      }
    });
  }

  addComment(): void {
    if (this.newCommentForm.invalid) return;
    const content = this.newCommentForm.get('content')?.value?.trim();
    if (!content) return;

    this.addingComment = true;
    const createDto: CreateCommentDto = {
      taskId: this.taskId,
      content: content
    };

    this.commentService.createComment(createDto).subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          this.newCommentForm.reset();
          // بارگذاری مجدد کامنت‌ها از صفحه اول
          this.currentPage = 1;
          this.loadComments();
        }
        this.addingComment = false;
      },
      error: () => {
        this.addingComment = false;
      }
    });
  }


  startEditComment(comment: CommentDto): void {
    this.editingCommentId = comment.id;
    this.editCommentForm.patchValue({
      content: comment.content || ''
    });
  }
  cancelEditComment(): void {
    this.editingCommentId = null;
    this.editCommentContent = '';
  }

  saveEditComment(comment: CommentDto): void {
    if (this.editCommentForm.invalid) return;

    const content = this.editCommentForm.get('content')?.value?.trim();
    if (!content) return;

    const updateDto: UpdateCommentDto = {
      id: comment.id,
      taskId: comment.taskId,
      content: content
    };

    this.commentService.updateComment(updateDto).subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          this.editingCommentId = null;
          this.editCommentForm.reset();
          // به‌روزرسانی محلی کامنت
          const index = this.comments.findIndex(c => c.id === comment.id);
          if (index !== -1) {
            this.comments[index] = { ...this.comments[index], content: updateDto.content };
          }
        }
      }
    });
  }

  deleteComment(comment: CommentDto): void {
    if (confirm('آیا از حذف این کامنت اطمینان دارید؟')) {
      this.commentService.deleteComment(comment.id).subscribe({
        next: (response) => {
          if (response.statusCode === 200) {
            // حذف محلی کامنت
            this.comments = this.comments.filter(c => c.id !== comment.id);
            this.totalCount--;
          }
        }
      });
    }
  }

  isEditing(comment: CommentDto): boolean {
    return this.editingCommentId === comment.id;
  }

  canEditComment(comment: CommentDto): boolean {
    // در اینجا می‌توانید منطق بررسی دسترسی کاربر را اضافه کنید
    // برای مثال، فقط کاربر ایجاد کننده می‌تواند کامنت را ویرایش کند
    return true; // فعلاً همه می‌توانند ویرایش کنند
  }

  getUserName(userId: number): string {
    const user = this.users.find(u => u.id === userId);
    return user ? (user.username || 'نامشخص') : 'نامشخص';
  }

  toPersianDate(date: string | Date): string {
    return this.dateService.gregorianToJalaliDisplay(date);
  }

  scrollToBottom(): void {
    setTimeout(() => {
      if (this.commentsContainer) {
        this.commentsContainer.nativeElement.scrollTop =
          this.commentsContainer.nativeElement.scrollHeight;
      }
    }, 100);
  }

  onClose(): void {
    this.modalClose.emit();
  }
}
