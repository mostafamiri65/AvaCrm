import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { UserListDto } from '../../../dtos/accounts/user.models';
import {TaskItemDto, TaskPriority, TaskStatus} from '../../../dtos/ProjectManagement/task-item.model';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {DateService} from '../../../services/utility/date';
import {NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-task-form',
  imports: [
    ReactiveFormsModule,
    NgIf,
    NgForOf
  ],
  templateUrl: './task-form.component.html',
  styleUrl: './task-form.component.css'
})
export class TaskFormComponent implements OnInit {
  @Input() projectId!: number;
  @Input() users: UserListDto[] = [];
  @Input() isEditMode = false;
  @Input() task: TaskItemDto | null = null;
  @Output() formSubmit = new EventEmitter<any>();
  @Output() formCancel = new EventEmitter<void>();

  taskForm: FormGroup;
  submitting = false;

  // Enums for template
  taskPriority = TaskPriority;
  taskStatus = TaskStatus;

  constructor(
    private fb: FormBuilder,
    private dateService: DateService
  ) {
    this.taskForm = this.createForm();
  }

  ngOnInit(): void {
    if (this.isEditMode && this.task) {
      // تبدیل تاریخ میلادی به شمسی برای نمایش در فرم
      const dueDateJalali = this.task.dueDate ?
        this.dateService.gregorianToJalaliString(this.task.dueDate) : null;

      this.taskForm.patchValue({
        title: this.task.title,
        description: this.task.description || '',
        assignedTo: this.task.assignedTo,
        priority: this.task.priority,
        status: this.task.status,
        dueDate: dueDateJalali
      });
    } else {
      // مقادیر پیش‌فرض برای ایجاد تسک جدید
      this.taskForm.patchValue({
        projectId: this.projectId,
        status: TaskStatus.ToDo,
        priority: TaskPriority.Medium
      });
    }
  }

  createForm(): FormGroup {
    return this.fb.group({
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: [''],
      projectId: [this.projectId, Validators.required],
      assignedTo: ['', Validators.required],
      priority: [TaskPriority.Medium, Validators.required],
      status: [TaskStatus.ToDo, Validators.required],
      dueDate: ['']
    });
  }

  onSubmit(): void {
    if (this.taskForm.invalid) {
      this.markFormGroupTouched();
      return;
    }

    const formValue = this.taskForm.value;

    // اعتبارسنجی تاریخ شمسی
    const dueDate = formValue.dueDate;
    if (dueDate && !this.dateService.isValidJalaliDateString(dueDate)) {
      alert('تاریخ مهلت معتبر نیست. فرمت صحیح: 1403-01-01');
      return;
    }

    console.log('Form submitted with data:', formValue);
    this.submitting = true;

    // ارسال داده‌های فرم به والد
    this.formSubmit.emit(formValue);
  }

  onCancel(): void {
    this.formCancel.emit();
  }

  private markFormGroupTouched(): void {
    Object.keys(this.taskForm.controls).forEach(key => {
      const control = this.taskForm.get(key);
      control?.markAsTouched();
    });
  }

  getPriorityText(priority: TaskPriority): string {
    switch (priority) {
      case TaskPriority.Low:
        return 'کم';
      case TaskPriority.Medium:
        return 'متوسط';
      case TaskPriority.High:
        return 'بالا';
      case TaskPriority.Critical:
        return 'بحرانی';
      default:
        return 'نامشخص';
    }
  }

  getStatusText(status: TaskStatus): string {
    switch (status) {
      case TaskStatus.ToDo:
        return 'برای انجام';
      case TaskStatus.InProgress:
        return 'در حال انجام';
      case TaskStatus.Done:
        return 'انجام شده';
      case TaskStatus.Cancelled:
        return 'لغو شده';
      default:
        return 'نامشخص';
    }
  }

  // متد کمکی برای نمایش خطاها
  getFieldError(fieldName: string): string {
    const field = this.taskForm.get(fieldName);
    if (field?.touched && field.errors) {
      if (field.errors['required']) {
        return 'این فیلد الزامی است';
      }
      if (field.errors['minlength']) {
        return `حداقل ${field.errors['minlength'].requiredLength} کاراکتر required`;
      }
    }
    return '';
  }

}
