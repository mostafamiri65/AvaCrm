import {Component, OnInit} from '@angular/core';
import {CountryDto, CreateCountryDto, UpdateCountryDto} from '../../../dtos/Commons/country.dto';
import {CountryService} from '../../../services/country.service';
import {NgForOf, NgIf} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {Router} from '@angular/router';

@Component({
  selector: 'app-countries',
  imports: [
    NgIf,
    FormsModule,
    NgForOf,
  ],
  templateUrl: './countries.component.html',
  styleUrl: './countries.component.css'
})
export class CountriesComponent implements OnInit {
  countries: CountryDto[] = [];
  isLoading = false;
  // مدل‌ها برای فرم‌ها
  newCountry: CreateCountryDto = { name: '' };
  selectedCountry: CountryDto = { id: 0, name: '' };
  updatedCountry: UpdateCountryDto = { id: 0, name: '' };
  countryToDelete: CountryDto = { id: 0, name: '' };

  // حالت‌های مودال
  showAddModal = false;
  showEditModal = false;
  showDeleteModal = false;

  // پیام‌ها
  successMessage = '';
  errorMessage = '';

constructor(private countryService: CountryService,
            private router: Router) {
}
  ngOnInit(): void {
    this.loadCountries();
  }

  loadCountries(): void {
    this.isLoading = true;
    this.countryService.getCountries().subscribe({
      next: (countries) => {

        this.countries = countries;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading countries:', error);
        this.errorMessage = 'خطا در بارگذاری لیست کشورها';
        this.isLoading = false;
      }
    });
  }
  showProvinces(country: CountryDto): void {
    this.router.navigate(['/provinces', country.id], {
      state: { countryName: country.name }
    });
  }
  // باز کردن مودال اضافه کردن
  openAddModal(): void {
    this.newCountry = { name: '' };
    this.showAddModal = true;
    this.clearMessages();
  }

  // اضافه کردن کشور جدید
  addCountry(): void {
    if (!this.newCountry.name.trim()) {
      this.errorMessage = 'نام کشور را وارد کنید';
      return;
    }

    this.isLoading = true;
    this.countryService.createCountry(this.newCountry).subscribe({
      next: (result) => {
        if (result) {
          this.successMessage = 'کشور با موفقیت اضافه شد';
          this.showAddModal = false;
          this.loadCountries(); // بارگذاری مجدد لیست
        } else {
          this.errorMessage = 'خطا در اضافه کردن کشور';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error adding country:', error);
        this.errorMessage = 'خطا در اضافه کردن کشور';
        this.isLoading = false;
      }
    });
  }

  // باز کردن مودال ویرایش
  openEditModal(country: CountryDto): void {
    this.selectedCountry = { ...country };
    this.updatedCountry = { id: country.id, name: country.name };
    this.showEditModal = true;
    this.clearMessages();
  }

  // ویرایش کشور
  updateCountry(): void {
    if (!this.updatedCountry.name.trim()) {
      this.errorMessage = 'نام کشور را وارد کنید';
      return;
    }

    this.isLoading = true;
    this.countryService.updateCountry(this.updatedCountry).subscribe({
      next: (result) => {
        if (result) {
          this.successMessage = 'کشور با موفقیت ویرایش شد';
          this.showEditModal = false;
          this.loadCountries(); // بارگذاری مجدد لیست
        } else {
          this.errorMessage = 'خطا در ویرایش کشور';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error updating country:', error);
        this.errorMessage = 'خطا در ویرایش کشور';
        this.isLoading = false;
      }
    });
  }

  // باز کردن مودال حذف
  openDeleteModal(country: CountryDto): void {
    this.countryToDelete = { ...country };
    this.showDeleteModal = true;
    this.clearMessages();
  }

  // حذف کشور
  deleteCountry(): void {
    this.isLoading = true;
    this.countryService.deleteCountry(this.countryToDelete.id).subscribe({
      next: (result) => {
        if (result) {
          this.successMessage = 'کشور با موفقیت حذف شد';
          this.showDeleteModal = false;
          this.loadCountries(); // بارگذاری مجدد لیست
        } else {
          this.errorMessage = 'خطا در حذف کشور';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error deleting country:', error);
        this.errorMessage = 'خطا در حذف کشور';
        this.isLoading = false;
      }
    });
  }

  // بستن مودال‌ها
  closeModals(): void {
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
