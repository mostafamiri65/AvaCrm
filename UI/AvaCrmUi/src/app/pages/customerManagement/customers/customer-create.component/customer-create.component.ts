import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {CustomerType} from '../../../../dtos/CustomerManagment/customer.enum';
import {CustomerService} from '../../../../services/customer.service';
import {Router} from '@angular/router';
import {
  CustomerCreateDto,
} from '../../../../dtos/CustomerManagment/customer.dto';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-customer-create.component',
  imports: [
    ReactiveFormsModule,
    NgIf,
    FormsModule
  ],
  templateUrl: './customer-create.component.html',
  styleUrl: './customer-create.component.css'
})
export class CustomerCreateComponent implements OnInit {
  customerForm!: FormGroup;
  individualForm!: FormGroup;
  organizationForm!: FormGroup;
  loading = false;
  customerType: CustomerType = CustomerType.Individual;
  CustomerType = CustomerType;
  isCodeUnique = true;
  checkingCode = false;

  constructor(
    private fb: FormBuilder,
    private customerService: CustomerService,
    protected router: Router
  ) { }

  ngOnInit(): void {
    this.initializeForms();
    this.setupCustomerTypeChange();
  }

  initializeForms(): void {
    // فرم اصلی مشتری
    this.customerForm = this.fb.group({
      code: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(50)]],
      customerType: [CustomerType.Individual, Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: ['', [Validators.required, Validators.pattern(/^[0-9+]{10,15}$/)]]
    });

    // فرم مشتری حقیقی
    this.individualForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.maxLength(100)]],
      lastName: ['', [Validators.required, Validators.maxLength(100)]],
      birthDate: ['']
    });

    // فرم مشتری حقوقی
    this.organizationForm = this.fb.group({
      companyName: ['', [Validators.required, Validators.maxLength(200)]],
      registrationNumber: ['', [Validators.required, Validators.maxLength(50)]],
      industry: ['', [Validators.required, Validators.maxLength(100)]]
    });
  }

  setupCustomerTypeChange(): void {
    // گوش دادن به تغییرات نوع مشتری
    this.customerForm.get('customerType')?.valueChanges.subscribe((value: CustomerType) => {
      try {

        this.customerType = value;

        // وقتی نوع مشتری تغییر کرد، فرم مربوطه را ریست کنیم
        if (value === CustomerType.Individual) {
          this.organizationForm.reset();
        } else {
           this.individualForm.reset();
        }
      } catch (error) {
        console.error(error);
        // Handle the error appropriately
      }
    });
  }

  checkCodeUnique(): void {
    const code = this.customerForm.get('code')?.value;
    if (code && code.length >= 3) {
      this.checkingCode = true;
      this.customerService.checkCodeUnique(code).subscribe({
        next: (response) => {
          this.isCodeUnique = response.data || false;
          this.checkingCode = false;
        },
        error: () => {
          this.checkingCode = false;
        }
      });
    }
  }

  onSubmit(): void {

    // مارک کردن همه فیلدها как touched برای نمایش خطاها
    this.markFormGroupTouched(this.customerForm);

    if (this.customerType === CustomerType.Individual) {
      this.markFormGroupTouched(this.individualForm);
    } else {
      this.markFormGroupTouched(this.organizationForm);
    }

    // اعتبارسنجی فرم اصلی
    if (this.customerForm.invalid) {
      alert('لطفا اطلاعات اصلی مشتری را تکمیل کنید');
      return;
    }

    // اعتبارسنجی فرم مربوطه
    if (this.customerType === CustomerType.Individual && this.individualForm.invalid) {
      alert('لطفا اطلاعات مشتری حقیقی را تکمیل کنید');
      return;
    }

    if (this.customerType === CustomerType.Organization && this.organizationForm.invalid) {
      alert('لطفا اطلاعات مشتری حقوقی را تکمیل کنید');
      return;
    }

    if (!this.isCodeUnique) {
      alert('کد مشتری باید یکتا باشد');
      return;
    }

    this.loading = true;

    // ساخت آبجکت کامل برای ارسال
    const customerData: CustomerCreateDto = {
      code: this.customerForm.get('code')?.value,

      email: this.customerForm.get('email')?.value,
      phoneNumber: this.customerForm.get('phoneNumber')?.value,
      individualCustomer: undefined,
      organizationCustomer: undefined,
      customerAddresses: [],
      tagIds: [],
      typeOfCustomer : this.customerForm.get('customerType')?.value as number
    };

    // اضافه کردن اطلاعات مشتری حقیقی یا حقوقی
    if (this.customerType === CustomerType.Individual) {
      customerData.individualCustomer = {
        customerId: 0, // این مقدار در سمت سرور پر خواهد شد
        firstName: this.individualForm.get('firstName')?.value,
        lastName: this.individualForm.get('lastName')?.value,
        birthDate: this.individualForm.get('birthDate')?.value
      };
    } else {
      customerData.organizationCustomer = {
        customerId: 0, // این مقدار در سمت سرور پر خواهد شد
        companyName: this.organizationForm.get('companyName')?.value,
        registrationNumber: this.organizationForm.get('registrationNumber')?.value,
        industry: this.organizationForm.get('industry')?.value
      };
    }


    // ارسال همه اطلاعات در یک درخواست
    this.customerService.createCustomer(customerData).subscribe({
      next: (response) => {
        this.loading = false;

        if (response.statusCode === 201) {
          const customerTypeText = this.customerType === CustomerType.Individual ? 'حقیقی' : 'حقوقی';
          alert(`مشتری ${customerTypeText} با موفقیت ایجاد شد`);
          this.router.navigate(['/customers']);
        } else {
          alert(response.message || 'خطا در ایجاد مشتری');
        }
      },
      error: (error) => {
        this.loading = false;
        console.error('Error creating customer:', error);

        if (error.error && error.error.message) {
          alert(error.error.message);
        } else {
          alert('خطا در ایجاد مشتری');
        }
      }
    });
  }

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.keys(formGroup.controls).forEach(key => {
      const control = formGroup.get(key);
      control?.markAsTouched();

      if (control instanceof FormGroup) {
        this.markFormGroupTouched(control);
      }
    });
  }

  isFieldInvalid(form: FormGroup, fieldName: string): boolean {
    const field = form.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  getFieldError(form: FormGroup, fieldName: string): string {
    const field = form.get(fieldName);
    if (field?.errors) {
      if (field.errors['required']) {
        return 'این فیلد اجباری است';
      }
      if (field.errors['email']) {
        return 'فرمت ایمیل نامعتبر است';
      }
      if (field.errors['pattern']) {
        return 'فرمت شماره تلفن نامعتبر است (۱۰-۱۵ رقم)';
      }
      if (field.errors['minlength']) {
        return `حداقل طول ${field.errors['minlength'].requiredLength} کاراکتر است`;
      }
      if (field.errors['maxlength']) {
        return `حداکثر طول ${field.errors['maxlength'].requiredLength} کاراکتر است`;
      }
    }
    return '';
  }


   convertToCustomerType(value: string): CustomerType {
    switch (value) {
      case '1':
        return CustomerType.Individual;
      case '2':
        return CustomerType.Organization;
      default:
        throw new Error('Invalid customer type value');
    }
  }
}
