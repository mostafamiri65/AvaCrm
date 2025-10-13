import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {TagDto, TagListDto} from '../../../dtos/Commons/tag.models';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {TagService} from '../../../services/tag.service';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-tag-form',
  imports: [
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './tag-form.component.html',
  styleUrl: './tag-form.component.css'
})
export class TagFormComponent implements OnInit {
  @Input() tag?: TagListDto;
  @Output() saved = new EventEmitter<void>();
  @Output() cancelled = new EventEmitter<void>();

  tagForm!: FormGroup;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private tagService: TagService
  ) { }

  ngOnInit(): void {
    console.log('🔧 TagForm initialized with tag:', this.tag);
    this.initializeForm();
  }

  initializeForm(): void {
    this.tagForm = this.fb.group({
      title: [this.tag?.title || '', [Validators.required, Validators.maxLength(50)]]
    });

    console.log('📝 Tag form initialized:', this.tagForm.value);
  }

  onSubmit(): void {

    if (this.tagForm.invalid) {
      this.markFormGroupTouched(this.tagForm);
      return;
    }

    this.loading = true;

    if (this.tag) {
      this.updateTag();
    } else {
      this.createTag();
    }
  }

  createTag(): void {
    const title = this.tagForm.get('title')?.value;

    // ایجاد آبجکت کامل برای ارسال
    const createData = { title: title };

    console.log('🎯 Sending CREATE request with data:', createData);
    console.log('📤 Full payload:', JSON.stringify(createData, null, 2));

    this.tagService.create(title).subscribe({
      next: (response) => {
        this.loading = false;
        if (response.statusCode === 201) {
          this.saved.emit();
          this.tagForm.reset();
        } else {
          alert(response.message || 'خطا در ایجاد تگ');
        }
      },
      error: (error) => {
        this.loading = false;
        console.error('❌ Error creating tag:', error);

        // نمایش خطای کامل
        console.error('❌ Full error details:', {
          status: error.status,
          message: error.message,
          error: error.error
        });

        if (error.error && error.error.message) {
        } else if (error.error && error.error.title) {
          alert(error.error.title);
        } else {
          alert('خطا در ایجاد تگ');
        }
      }
    });
  }
  updateTag(): void {
    const updateDto: TagDto = {
      id: this.tag!.id,
      title: this.tagForm.get('title')?.value
    };

    this.tagService.update(updateDto).subscribe({
      next: (response) => {
        this.loading = false;
        if (response.statusCode === 200) {
          this.saved.emit();
        } else {
          alert(response.message || 'خطا در بروزرسانی تگ');
        }
      },
      error: (error) => {
        this.loading = false;
        console.error('❌ Error updating tag:', error);

        if (error.error && error.error.message) {
          alert(error.error.message);
        } else {
          alert('خطا در بروزرسانی تگ');
        }
      }
    });
  }

  onCancel(): void {
    this.cancelled.emit();
  }

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.get(key);
      control?.markAsTouched();
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.tagForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  getFieldError(fieldName: string): string {
    const field = this.tagForm.get(fieldName);
    if (field?.errors) {
      if (field.errors['required']) {
        return 'این فیلد اجباری است';
      }
      if (field.errors['maxlength']) {
        return `حداکثر طول ${field.errors['maxlength'].requiredLength} کاراکتر است`;
      }
    }
    return '';
  }
}
