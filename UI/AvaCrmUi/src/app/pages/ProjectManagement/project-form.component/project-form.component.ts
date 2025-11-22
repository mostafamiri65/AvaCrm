import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Project } from '../../../dtos/ProjectManagement/project.model';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { UserService } from '../../../services/user.service';
import { UserListDto } from '../../../dtos/accounts/user.models';
import { NgForOf, NgIf } from '@angular/common';
import {DateService} from '../../../services/utility/date';


@Component({
  selector: 'app-project-form',
  imports: [
    ReactiveFormsModule,
    NgIf,
    NgForOf
  ],
  templateUrl: './project-form.component.html',
  styleUrl: './project-form.component.css'
})
export class ProjectFormComponent implements OnInit {
  @Input() isEditMode = false;
  @Input() project: Project | null = null;
  @Output() formSubmit = new EventEmitter<any>(); // تغییر به any برای ارسال داده
  @Output() formCancel = new EventEmitter<void>();

  projectForm: FormGroup;
  users: UserListDto[] = [];
  selectedUsers: UserListDto[] = [];
  submitting = false;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private dateService: DateService
  ) {
    this.projectForm = this.createForm();
  }

  ngOnInit(): void {
    this.loadUsers();

    if (this.isEditMode && this.project) {
      // تبدیل تاریخ‌ها به شمسی برای نمایش در فرم
      const startDateJalali = this.dateService.gregorianToJalaliString(this.project.startDate);
      const endDateJalali = this.project.endDate ?
        this.dateService.gregorianToJalaliString(this.project.endDate) : null;

      this.projectForm.patchValue({
        title: this.project.title,
        description: this.project.description,
        startDate: startDateJalali,
        endDate: endDateJalali,
        userIds: this.project.userIds || []
      });

      // بارگذاری کاربران انتخاب شده
      if (this.project.userIds && this.project.userIds.length > 0) {
        this.loadSelectedUsers(this.project.userIds);
      }
    }
  }

  createForm(): FormGroup {
    return this.fb.group({
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', [Validators.required, Validators.minLength(10)]],
      startDate: ['', Validators.required],
      endDate: [''],
      userIds: [[]]
    });
  }

  loadUsers(): void {
    this.userService.getAll().subscribe({
      next: (response) => {
        if (response.statusCode != 200) {
          return;
        }
        this.users = response.data ? response.data : [];
      }
    });
  }

  loadSelectedUsers(userIds: number[]): void {
    this.selectedUsers = this.users.filter(user => userIds.includes(user.id));
  }

  onUserSelectionChange(user: UserListDto, event: any): void {
    const isChecked = event.target.checked;
    const userIds = this.projectForm.get('userIds')?.value || [];

    if (isChecked) {
      if (!userIds.includes(user.id)) {
        userIds.push(user.id);
        this.selectedUsers.push(user);
      }
    } else {
      const index = userIds.indexOf(user.id);
      if (index > -1) {
        userIds.splice(index, 1);
        this.selectedUsers = this.selectedUsers.filter(u => u.id !== user.id);
      }
    }

    this.projectForm.patchValue({ userIds });
  }

  isUserSelected(userId: number): boolean {
    return this.projectForm.get('userIds')?.value.includes(userId);
  }

  removeUser(user: UserListDto): void {
    const userIds = this.projectForm.get('userIds')?.value || [];
    const index = userIds.indexOf(user.id);

    if (index > -1) {
      userIds.splice(index, 1);
      this.selectedUsers = this.selectedUsers.filter(u => u.id !== user.id);
      this.projectForm.patchValue({ userIds });
    }
  }

  onSubmit(): void {
    console.log('Form submitted in child component');

    if (this.projectForm.invalid) {
      console.log('Form is invalid', this.projectForm.errors);
      this.markFormGroupTouched();
      return;
    }

    // اعتبارسنجی تاریخ شمسی
    const startDate = this.projectForm.get('startDate')?.value;
    const endDate = this.projectForm.get('endDate')?.value;

    if (startDate && !this.dateService.isValidJalaliDateString(startDate)) {
      alert('تاریخ شروع معتبر نیست');
      return;
    }

    if (endDate && !this.dateService.isValidJalaliDateString(endDate)) {
      alert('تاریخ پایان معتبر نیست');
      return;
    }

    console.log('Form is valid, emitting data:', this.projectForm.value);
    this.submitting = true;

    // ارسال داده‌های فرم به والد
    this.formSubmit.emit(this.projectForm.value);
  }

  onCancel(): void {
    this.formCancel.emit();
  }

  private markFormGroupTouched(): void {
    Object.keys(this.projectForm.controls).forEach(key => {
      const control = this.projectForm.get(key);
      control?.markAsTouched();
    });
  }
}
