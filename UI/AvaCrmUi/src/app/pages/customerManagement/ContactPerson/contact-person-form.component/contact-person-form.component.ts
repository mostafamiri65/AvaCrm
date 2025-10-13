import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {ContactPersonService} from '../../../../services/contact-person.service';
import {ContactPersonCreateDto, ContactPersonUpdateDto} from '../../../../dtos/CustomerManagment/contact-person.models';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-contact-person-form',
  imports: [
    ReactiveFormsModule,
    NgIf
  ],
  templateUrl: './contact-person-form.component.html',
  styleUrl: './contact-person-form.component.css'
})
export class ContactPersonFormComponent implements OnInit {
@Input() customerId!: number;
@Input() contactPersonId?: number;
@Output() saved = new EventEmitter<void>();
@Output() cancelled = new EventEmitter<void>();

  contactPersonForm!: FormGroup;
  loading = false;

  constructor(
    private fb: FormBuilder,
    private contactPersonService: ContactPersonService
) { }

  ngOnInit(): void {
    this.initializeForm();

    if (this.contactPersonId) {
    this.loadContactPerson();
  }
}

  initializeForm(): void {
    this.contactPersonForm = this.fb.group({
      fullName: ['', [Validators.required, Validators.maxLength(100)]],
      jobTitle: ['', [Validators.required, Validators.maxLength(100)]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^[0-9+]{10,15}$/)]],
      email: ['', [Validators.required, Validators.email, Validators.maxLength(100)]]
    });

  }

  loadContactPerson(): void {
    if (!this.contactPersonId) return;

  this.contactPersonService.getById(this.contactPersonId).subscribe({
    next: (response) => {
      if (response.statusCode === 200 && response.data) {
        const contactPerson = response.data;

        this.contactPersonForm.patchValue({
          fullName: contactPerson.fullName,
          jobTitle: contactPerson.jobTitle,
          phoneNumber: contactPerson.phoneNumber,
          email: contactPerson.email
        });
      }
    },
    error: (error) => {
      console.error('❌ Error loading contact person:', error);
    }
  });
}

  onSubmit(): void {

    if (this.contactPersonForm.invalid) {
    this.markFormGroupTouched(this.contactPersonForm);
    return;
  }

  this.loading = true;

  if (this.contactPersonId) {
    this.updateContactPerson();
  } else {
    this.createContactPerson();
  }
}

  createContactPerson(): void {
    const formValue = this.contactPersonForm.value;

    const createDto: ContactPersonCreateDto = {
    customerId: this.customerId,
    fullName: formValue.fullName,
    jobTitle: formValue.jobTitle,
    phoneNumber: formValue.phoneNumber,
    email: formValue.email
  };


  this.contactPersonService.create(createDto).subscribe({
    next: (response) => {
      this.loading = false;
      if (response.statusCode === 201) {
        this.saved.emit();
        this.contactPersonForm.reset();
      } else {
        alert(response.message || 'خطا در ایجاد فرد ارتباطی');
      }
    },
    error: (error) => {
      this.loading = false;
      console.error('❌ Error creating contact person:', error);

      if (error.error && error.error.message) {
        alert(error.error.message);
      } else {
        alert('خطا در ایجاد فرد ارتباطی');
      }
    }
  });
}

  updateContactPerson(): void {
    const formValue = this.contactPersonForm.value;

    const updateDto: ContactPersonUpdateDto = {
    id: this.contactPersonId!,
    fullName: formValue.fullName,
    jobTitle: formValue.jobTitle,
    phoneNumber: formValue.phoneNumber,
    email: formValue.email
  };


  this.contactPersonService.update(updateDto).subscribe({
    next: (response) => {
      this.loading = false;
      if (response.statusCode === 200) {
        this.saved.emit();
      } else {
        alert(response.message || 'خطا در بروزرسانی فرد ارتباطی');
      }
    },
    error: (error) => {
      this.loading = false;
      console.error('❌ Error updating contact person:', error);

      if (error.error && error.error.message) {
        alert(error.error.message);
      } else {
        alert('خطا در بروزرسانی فرد ارتباطی');
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
    const field = this.contactPersonForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  getFieldError(fieldName: string): string {
    const field = this.contactPersonForm.get(fieldName);
    if (field?.errors) {
      if (field.errors['required']) {
        return 'این فیلد اجباری است';
      }
      if (field.errors['email']) {
        return 'فرمت ایمیل نامعتبر است';
      }
      if (field.errors['pattern']) {
        return 'فرمت شماره تلفن نامعتبر است';
      }
      if (field.errors['maxlength']) {
        return `حداکثر طول ${field.errors['maxlength'].requiredLength} کاراکتر است`;
      }
    }
    return '';
  }
}
