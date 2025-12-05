import {Component, OnInit} from '@angular/core';
import {CreateUnitDto, UnitCategory, UnitDto, UpdateUnitDto} from '../../../dtos/Commons/unit.dto';
import {PaginationRequest} from '../../../models/base.model';
import {UnitService} from '../../../services/unit.service';
import {NgForOf, NgIf} from '@angular/common';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-units.component',
  imports: [
    NgIf,
    NgForOf,
    FormsModule
  ],
  templateUrl: './units.component.html',
  styleUrl: './units.component.css'
})
export class UnitsComponent implements OnInit {
  // لیست واحدها
  units: UnitDto[] = [];
  isLoading = false;

  // مدل‌های فرم
  newUnit: CreateUnitDto = {
    name: '',
    code: '',
    category: UnitCategory.General
  };

  selectedUnit: UnitDto = {
    id: 0,
    name: '',
    code: '',
    category: UnitCategory.General
  };

  updatedUnit: UpdateUnitDto = {
    id: 0,
    name: '',
    code: '',
    category: UnitCategory.General
  };

  unitToDelete: UnitDto = {
    id: 0,
    name: '',
    code: '',
    category: UnitCategory.General
  };

  // حالت‌های مودال
  showAddModal = false;
  showEditModal = false;
  showDeleteModal = false;

  // پیام‌ها
  successMessage = '';
  errorMessage = '';

  // صفحه‌بندی
  pagination: PaginationRequest = {
    pageNumber: 1,
    pageSize: 10
  };

  // دسته‌بندی‌ها
  unitCategories = [
    { value: UnitCategory.General, label: 'عمومی' },
    { value: UnitCategory.Weight, label: 'وزن' },
    { value: UnitCategory.Length, label: 'طول' },
    { value: UnitCategory.Volume, label: 'حجم' },
    { value: UnitCategory.Time, label: 'زمان' },
    { value: UnitCategory.Area, label: 'مساحت' },
    { value: UnitCategory.Count, label: 'تعداد' }
  ];

  constructor(private unitService: UnitService) {}

  ngOnInit(): void {
    this.loadUnits();
  }

  // بارگذاری واحدها
  loadUnits(): void {
    this.isLoading = true;
    this.unitService.getAllUnits(this.pagination).subscribe({
      next: (response) => {
        if (response.statusCode === 200 && response.data) {
          this.units = response.data.items;
        } else {
          this.errorMessage = response.message || 'خطا در دریافت لیست واحدها';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading units:', error);
        this.errorMessage = 'خطا در بارگذاری لیست واحدها';
        this.isLoading = false;
      }
    });
  }

  // باز کردن مودال اضافه کردن
  openAddModal(): void {
    this.newUnit = { name: '', code: '', category: UnitCategory.General };
    this.showAddModal = true;
    this.clearMessages();
  }

  // اضافه کردن واحد جدید
  addUnit(): void {
    if (!this.validateUnitForm(this.newUnit)) return;

    this.isLoading = true;
    this.unitService.createUnit(this.newUnit).subscribe({
      next: (response) => {
        if (response.statusCode === 201 && response.data) {
          this.successMessage = 'واحد جدید با موفقیت اضافه شد';
          this.showAddModal = false;
          this.loadUnits();
        } else {
          this.errorMessage = response.message || 'خطا در اضافه کردن واحد';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error adding unit:', error);
        this.errorMessage = 'خطا در اضافه کردن واحد';
        this.isLoading = false;
      }
    });
  }

  // باز کردن مودال ویرایش
  openEditModal(unit: UnitDto): void {
    this.selectedUnit = { ...unit };
    this.updatedUnit = {
      id: unit.id,
      name: unit.name,
      code: unit.code,
      category: unit.category
    };
    this.showEditModal = true;
    this.clearMessages();
  }

  // ویرایش واحد
  updateUnit(): void {
    if (!this.validateUnitForm(this.updatedUnit)) return;

    this.isLoading = true;
    this.unitService.updateUnit(this.updatedUnit).subscribe({
      next: (response) => {
        if (response.statusCode === 200 && response.data) {
          this.successMessage = 'واحد با موفقیت ویرایش شد';
          this.showEditModal = false;
          this.loadUnits();
        } else {
          this.errorMessage = response.message || 'خطا در ویرایش واحد';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error updating unit:', error);
        this.errorMessage = 'خطا در ویرایش واحد';
        this.isLoading = false;
      }
    });
  }

  // باز کردن مودال حذف
  openDeleteModal(unit: UnitDto): void {
    this.unitToDelete = { ...unit };
    this.showDeleteModal = true;
    this.clearMessages();
  }

  // حذف واحد
  deleteUnit(): void {
    this.isLoading = true;
    this.unitService.deleteUnit(this.unitToDelete.id).subscribe({
      next: (response) => {
        if (response.statusCode === 200 && response.data?.doneSuccessfully) {
          this.successMessage = 'واحد با موفقیت حذف شد';
          this.showDeleteModal = false;
          this.loadUnits();
        } else {
          this.errorMessage = response.message || 'خطا در حذف واحد';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error deleting unit:', error);
        this.errorMessage = 'خطا در حذف واحد';
        this.isLoading = false;
      }
    });
  }

  // اعتبارسنجی فرم
  private validateUnitForm(unit: CreateUnitDto | UpdateUnitDto): boolean {
    if (!unit.name.trim()) {
      this.errorMessage = 'نام واحد را وارد کنید';
      return false;
    }

    if (!unit.code.trim()) {
      this.errorMessage = 'کد واحد را وارد کنید';
      return false;
    }

    if (unit.code.length > 10) {
      this.errorMessage = 'کد واحد نمی‌تواند بیشتر از 10 کاراکتر باشد';
      return false;
    }

    if (unit.name.length > 100) {
      this.errorMessage = 'نام واحد نمی‌تواند بیشتر از 100 کاراکتر باشد';
      return false;
    }

    return true;
  }

  // دریافت نام دسته‌بندی
  getCategoryName(category: UnitCategory): string {
    const found = this.unitCategories.find(c => c.value === category);
    return found ? found.label : 'نامشخص';
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
