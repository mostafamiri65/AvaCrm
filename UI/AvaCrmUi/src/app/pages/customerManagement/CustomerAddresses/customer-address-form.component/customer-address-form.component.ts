import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {CountryDto} from '../../../../dtos/Commons/country.dto';
import {ProvinceDto} from '../../../../dtos/Commons/province.dto';
import {CustomerAddressService} from '../../../../services/customer-address.service';
import {CountryService} from '../../../../services/country.service';
import {ProvinceService} from '../../../../services/province.service';
import {
  CustomerAddressCreateDto,
  CustomerAddressUpdateDto
} from '../../../../dtos/CustomerManagment/customer-address.models';
import {NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-customer-address-form',
  imports: [
    ReactiveFormsModule,
    NgForOf,
    NgIf
  ],
  templateUrl: './customer-address-form.component.html',
  styleUrl: './customer-address-form.component.css'
})
export class CustomerAddressFormComponent implements OnInit{
  @Input() customerId!: number;
  @Input() addressId?: number;
  @Output() saved = new EventEmitter<void>();
  @Output() cancelled = new EventEmitter<void>();

  addressForm!: FormGroup;
  loading = false;
  countries: CountryDto[] = [];
  provinces: ProvinceDto[] = [];
  loadingCountries = false;
  loadingProvinces = false;

  constructor(
    private fb: FormBuilder,
    private customerAddressService: CustomerAddressService,
    private countryService: CountryService,
    private provinceService: ProvinceService
  ) { }

  ngOnInit(): void {
    this.initializeForm();
    this.loadCountries();

    if (this.addressId) {
      this.loadAddress();
    }
  }

  initializeForm(): void {
    this.addressForm = this.fb.group({
      countryId: ['', Validators.required],
      provinceId: ['', Validators.required],
      city: ['', Validators.maxLength(100)], // فیلد شهر اضافه شد
      street: ['', [Validators.required, Validators.maxLength(200)]],
      additionalInfo: ['', Validators.maxLength(500)]
    });

    // وقتی کشور تغییر کرد، استان‌ها را بارگذاری کن
    this.addressForm.get('countryId')?.valueChanges.subscribe(countryId => {
      if (countryId) {
        this.loadProvincesByCountry(countryId);
        this.addressForm.patchValue({ provinceId: '', city: '' });
        this.provinces = []; // ریست کردن لیست استان‌ها
      } else {
        this.provinces = [];
        this.addressForm.patchValue({ provinceId: '', city: '' });
      }
    });
  }

  loadCountries(): void {
    this.loadingCountries = true;
    this.countryService.getCountries().subscribe({
      next: (countries) => {
        this.countries = countries;
        this.loadingCountries = false;
      },
      error: (error) => {
        console.error('Error loading countries:', error);
        this.loadingCountries = false;
      }
    });
  }

  loadProvincesByCountry(countryId: number): void {
    this.loadingProvinces = true;
    this.provinceService.getProvinces(countryId).subscribe({
      next: (provinces) => {
        this.provinces = provinces;
        this.loadingProvinces = false;
      },
      error: (error) => {
        console.error('Error loading provinces:', error);
        this.loadingProvinces = false;
      }
    });
  }

  loadAddress(): void {
    if (!this.addressId) return;

    this.customerAddressService.getById(this.addressId).subscribe({
      next: (response) => {
        if (response.statusCode === 200 && response.data) {
          const address = response.data;

          // ابتدا کشور را ست کنیم
          this.addressForm.patchValue({
            countryId: address.countryId
          });

          // بعد از بارگذاری استان‌ها، بقیه فیلدها را ست کنیم
          setTimeout(() => {
            this.addressForm.patchValue({
              provinceId: address.provinceId,
              city: address.cityName || '', // استفاده از cityName یا city
              street: address.street,
              additionalInfo: address.additionalInfo
            });
          }, 100);
        }
      },
      error: (error) => {
        console.error('Error loading address:', error);
      }
    });
  }

  onSubmit(): void {
    if (this.addressForm.invalid) {
      this.markFormGroupTouched(this.addressForm);
      return;
    }

    this.loading = true;

    if (this.addressId) {
      this.updateAddress();
    } else {
      this.createAddress();
    }
  }

  createAddress(): void {
    const formValue = this.addressForm.value;

    // ساخت آبجکت کامل با تمام فیلدهای مورد نیاز
    const createDto: CustomerAddressCreateDto = {
      customerId: this.customerId, // اضافه شد
      countryId: Number(formValue.countryId), // تبدیل به number
      provinceId: Number(formValue.provinceId), // تبدیل به number
      city: formValue.city?.trim() || undefined, // ارسال نام شهر (اختیاری)
      street: formValue.street,
      additionalInfo: formValue.additionalInfo
    };

    console.log('Creating address with complete data:', createDto);

    this.customerAddressService.create(createDto).subscribe({
      next: (response) => {
        this.loading = false;
        if (response.statusCode === 201) {
          this.saved.emit();
          this.addressForm.reset();
        } else {
          alert(response.message || 'خطا در ایجاد آدرس');
        }
      },
      error: (error) => {
        this.loading = false;
        console.error('Error creating address:', error);

        if (error.error && error.error.message) {
          alert(error.error.message);
        } else {
          alert('خطا در ایجاد آدرس');
        }
      }
    });
  }

  updateAddress(): void {
    const formValue = this.addressForm.value;

    // ساخت آبجکت کامل با تمام فیلدهای مورد نیاز
    const updateDto: CustomerAddressUpdateDto = {
      id: this.addressId!,
      customerId: this.customerId, // اضافه شد
      countryId: Number(formValue.countryId), // تبدیل به number
      provinceId: Number(formValue.provinceId), // تبدیل به number
      city: formValue.city?.trim() || undefined, // ارسال نام شهر (اختیاری)
      street: formValue.street,
      additionalInfo: formValue.additionalInfo
    };

    console.log('Updating address with complete data:', updateDto);

    this.customerAddressService.update(updateDto).subscribe({
      next: (response) => {
        this.loading = false;
        if (response.statusCode === 200) {
          this.saved.emit();
        } else {
          alert(response.message || 'خطا در بروزرسانی آدرس');
        }
      },
      error: (error) => {
        this.loading = false;
        console.error('Error updating address:', error);

        if (error.error && error.error.message) {
          alert(error.error.message);
        } else {
          alert('خطا در بروزرسانی آدرس');
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
    const field = this.addressForm.get(fieldName);
    return !!(field && field.invalid && (field.dirty || field.touched));
  }

  getFieldError(fieldName: string): string {
    const field = this.addressForm.get(fieldName);
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
