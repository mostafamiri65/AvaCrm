import {Component, OnInit} from '@angular/core';
import {CreateCurrencyDto, CurrencyDto, UpdateCurrencyDto} from '../../../dtos/Commons/currency.dto';
import {PaginationRequest} from '../../../models/base.model';
import {CurrencyService} from '../../../services/currency.service';
import {FormsModule} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-currencies',
  imports: [
    FormsModule,
    NgIf,
    NgForOf
  ],
  templateUrl: './currencies.component.html',
  styleUrl: './currencies.component.css'
})
export class CurrenciesComponent implements OnInit {
  // لیست ارزها
  currencies: CurrencyDto[] = [];
  isLoading = false;

  // مدل‌های فرم
  newCurrency: CreateCurrencyDto = {
    code: '',
    name: '',
    symbol: '',
    decimalPlaces: 0,
    isDefault: false
  };

  selectedCurrency: CurrencyDto = {
    id: 0,
    code: '',
    name: '',
    symbol: '',
    decimalPlaces: 0,
    isActive: true,
    isDefault: false
  };

  updatedCurrency: UpdateCurrencyDto = {
    id: 0,
    code: '',
    name: '',
    symbol: '',
    decimalPlaces: 0,
    isDefault: false
  };

  currencyToDelete: CurrencyDto = {
    id: 0,
    code: '',
    name: '',
    symbol: '',
    decimalPlaces: 0,
    isActive: true,
    isDefault: false
  };

  // حالت‌های مودال
  showAddModal = false;
  showEditModal = false;
  showDeleteModal = false;

  // پیام‌ها
  successMessage: string | null = '';
  errorMessage = '';

  // صفحه‌بندی
  pagination: PaginationRequest = {
    pageNumber: 1,
    pageSize: 10
  };

  constructor(private currencyService: CurrencyService) {}

  ngOnInit(): void {
    this.loadCurrencies();
  }

  // بارگذاری ارزها
  loadCurrencies(): void {
    this.isLoading = true;
    this.currencyService.getAllCurrencies(this.pagination).subscribe({
      next: (response) => {
        if (response.statusCode === 200 && response.data) {
          this.currencies = response.data.items;
        } else {
          this.errorMessage = response.message || 'خطا در دریافت لیست ارزها';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading currencies:', error);
        this.errorMessage = 'خطا در بارگذاری لیست ارزها';
        this.isLoading = false;
      }
    });
  }

  // باز کردن مودال اضافه کردن
  openAddModal(): void {
    this.newCurrency = {
      code: '',
      name: '',
      symbol: '',
      decimalPlaces: 0,
      isDefault: false
    };
    this.showAddModal = true;
    this.clearMessages();
  }

  // اضافه کردن ارز جدید
  addCurrency(): void {
    if (!this.validateCurrencyForm(this.newCurrency)) return;

    this.isLoading = true;
    this.currencyService.createCurrency(this.newCurrency).subscribe({
      next: (response) => {
        if (response.statusCode === 201 && response.data) {
          this.successMessage = 'ارز جدید با موفقیت اضافه شد';
          this.showAddModal = false;
          this.loadCurrencies();
        } else {
          this.errorMessage = response.message || 'خطا در اضافه کردن ارز';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error adding currency:', error);
        this.errorMessage = 'خطا در اضافه کردن ارز';
        this.isLoading = false;
      }
    });
  }

  // باز کردن مودال ویرایش
  openEditModal(currency: CurrencyDto): void {
    this.selectedCurrency = { ...currency };
    this.updatedCurrency = {
      id: currency.id,
      code: currency.code,
      name: currency.name,
      symbol: currency.symbol,
      decimalPlaces: currency.decimalPlaces,
      isDefault: currency.isDefault
    };
    this.showEditModal = true;
    this.clearMessages();
  }

  // ویرایش ارز
  updateCurrency(): void {
    if (!this.validateCurrencyForm(this.updatedCurrency)) return;

    this.isLoading = true;
    this.currencyService.updateCurrency(this.updatedCurrency).subscribe({
      next: (response) => {
        if (response.statusCode === 200 && response.data) {
          this.successMessage = 'ارز با موفقیت ویرایش شد';
          this.showEditModal = false;
          this.loadCurrencies();
        } else {
          this.errorMessage = response.message || 'خطا در ویرایش ارز';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error updating currency:', error);
        this.errorMessage = 'خطا در ویرایش ارز';
        this.isLoading = false;
      }
    });
  }

  // باز کردن مودال حذف
  openDeleteModal(currency: CurrencyDto): void {
    this.currencyToDelete = { ...currency };
    this.showDeleteModal = true;
    this.clearMessages();
  }

  // حذف ارز
  deleteCurrency(): void {
    this.isLoading = true;
    this.currencyService.deleteCurrency(this.currencyToDelete.id).subscribe({
      next: (response) => {
        if (response.statusCode === 200 && response.data?.doneSuccessfully) {
          this.successMessage = 'ارز با موفقیت حذف شد';
          this.showDeleteModal = false;
          this.loadCurrencies();
        } else {
          this.errorMessage = response.message || 'خطا در حذف ارز';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error deleting currency:', error);
        this.errorMessage = 'خطا در حذف ارز';
        this.isLoading = false;
      }
    });
  }

  // تغییر وضعیت پیش‌فرض
  changeDefaultStatus(currency: CurrencyDto, isDefault: boolean): void {
    if (!confirm(`آیا می‌خواهید ارز "${currency.name}" را ${isDefault ? 'پیش‌فرض' : 'غیرپیش‌فرض'} کنید؟`)) {
      return;
    }

    this.isLoading = true;
    this.currencyService.changeDefaultCurrency(currency.id, isDefault).subscribe({
      next: (response) => {
        if (response.statusCode === 200 && response.data?.doneSuccessfully) {
          this.successMessage = response.message;
          this.loadCurrencies();
        } else {
          this.errorMessage = response.message || 'خطا در تغییر وضعیت ارز';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error changing default status:', error);
        this.errorMessage = 'خطا در تغییر وضعیت ارز';
        this.isLoading = false;
      }
    });
  }

  // اعتبارسنجی فرم
  private validateCurrencyForm(currency: CreateCurrencyDto | UpdateCurrencyDto): boolean {
    if (!currency.code.trim()) {
      this.errorMessage = 'کد ارز را وارد کنید';
      return false;
    }

    if (!currency.name.trim()) {
      this.errorMessage = 'نام ارز را وارد کنید';
      return false;
    }

    if (currency.code.length > 10) {
      this.errorMessage = 'کد ارز نمی‌تواند بیشتر از 10 کاراکتر باشد';
      return false;
    }

    if (currency.name.length > 100) {
      this.errorMessage = 'نام ارز نمی‌تواند بیشتر از 100 کاراکتر باشد';
      return false;
    }

    if (currency.symbol && currency.symbol.length > 5) {
      this.errorMessage = 'نماد ارز نمی‌تواند بیشتر از 5 کاراکتر باشد';
      return false;
    }

    if (currency.decimalPlaces < 0 || currency.decimalPlaces > 8) {
      this.errorMessage = 'تعداد رقم اعشار باید بین 0 و 8 باشد';
      return false;
    }

    return true;
  }

  // بستن مودال‌ها
  closeModal(): void {
    this.showAddModal = false;
    this.showEditModal = false;
    this.showDeleteModal = false;
    this.clearMessages();
  }

  // پاک کردن پیام‌ها
  clearMessages(): void {
    this.successMessage = '';
    this.errorMessage = '';
  }
}
