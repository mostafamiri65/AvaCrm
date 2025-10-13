import {Component, OnInit, Input} from '@angular/core';
import {CommonModule} from '@angular/common';
import {FormsModule} from '@angular/forms';
import {CreateProvinceDto, ProvinceDto, UpdateProvinceDto} from '../../../dtos/Commons/province.dto';
import {ProvinceService} from '../../../services/province.service';
import {ActivatedRoute, Router} from '@angular/router';

@Component({
  selector: 'app-provinces',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './provinces.component.html',
  styleUrls: ['./provinces.component.css']
})
export class ProvincesComponent implements OnInit {
  countryId!: number;
  countryName: string = '';

  provinces: ProvinceDto[] = [];
  isLoading = false;

  // مدل‌ها برای فرم‌ها
  newProvince: CreateProvinceDto = {name: '', countryId: 0};
  selectedProvince: ProvinceDto = {id: 0, name: '', countryId: 0};
  updatedProvince: UpdateProvinceDto = {id: 0, name: '', countryId: 0};
  provinceToDelete: ProvinceDto = {id: 0, name: '', countryId: 0};

  // حالت‌های مودال
  showAddModal = false;
  showEditModal = false;
  showDeleteModal = false;

  // پیام‌ها
  successMessage = '';
  errorMessage = '';

  constructor(private provinceService: ProvinceService,
              private route: ActivatedRoute,
              private router: Router) {
  }

  ngOnInit(): void {
    this.getCountryIdFromUrl();
  }

  private getCountryIdFromUrl(): void {
    this.route.paramMap.subscribe(params => {
      console.log(params);
      const id = params.get('countryId');
      console.log('Country ID from URL:', id);

      if (id) {
        this.countryId = +id; // تبدیل به number
        this.loadProvinces();

      } else {
        console.error('Country ID not found in URL');
        this.errorMessage = 'شناسه کشور یافت نشد';
      }
    });
  }

  ngOnChanges()
    :
    void {
    if (this.countryId
    ) {
      this.loadProvinces();
    }
  }

  loadProvinces()
    :
    void {
    console.log('id', this.countryId);
    this.isLoading = true;
    this.provinceService.getProvinces(this.countryId).subscribe({
      next: (provinces) => {
        this.provinces = provinces;
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error loading provinces:', error);
        this.errorMessage = 'خطا در بارگذاری لیست استان‌ها';
        this.isLoading = false;
      }
    });
  }

  // باز کردن مودال اضافه کردن
  openAddModal()
    :
    void {
    this.newProvince = {name: '', countryId: this.countryId};
    this.showAddModal = true;
    this.clearMessages();
  }

  // اضافه کردن استان جدید
  addProvince()
    :
    void {
    if (!
      this.newProvince.name.trim()
    ) {
      this.errorMessage = 'نام استان را وارد کنید';
      return;
    }

    this.isLoading = true;
    this.provinceService.createProvince(this.newProvince).subscribe({
      next: (result) => {
        if (result) {
          this.successMessage = 'استان با موفقیت اضافه شد';
          this.showAddModal = false;
          this.loadProvinces();
        } else {
          this.errorMessage = 'خطا در اضافه کردن استان';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error adding province:', error);
        this.errorMessage = 'خطا در اضافه کردن استان';
        this.isLoading = false;
      }
    });
  }

  // باز کردن مودال ویرایش
  openEditModal(province
                :
                ProvinceDto
  ):
    void {
    this.selectedProvince = {...province};
    this.updatedProvince = {
      id: province.id,
      name: province.name,
      countryId: province.countryId
    };
    this.showEditModal = true;
    this.clearMessages();
  }

  // ویرایش استان
  updateProvince()
    :
    void {
    if (!
      this.updatedProvince.name.trim()
    ) {
      this.errorMessage = 'نام استان را وارد کنید';
      return;
    }

    this.isLoading = true;
    this.provinceService.updateProvince(this.updatedProvince).subscribe({
      next: (result) => {
        if (result) {
          this.successMessage = 'استان با موفقیت ویرایش شد';
          this.showEditModal = false;
          this.loadProvinces();
        } else {
          this.errorMessage = 'خطا در ویرایش استان';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error updating province:', error);
        this.errorMessage = 'خطا در ویرایش استان';
        this.isLoading = false;
      }
    });
  }

  // باز کردن مودال حذف
  openDeleteModal(province
                  :
                  ProvinceDto
  ):
    void {
    this.provinceToDelete = {...province};
    this.showDeleteModal = true;
    this.clearMessages();
  }

  // حذف استان
  deleteProvince()
    :
    void {
    this.isLoading = true;
    this.provinceService.deleteProvince(this.provinceToDelete.id).subscribe({
      next: (result) => {
        if (result) {
          this.successMessage = 'استان با موفقیت حذف شد';
          this.showDeleteModal = false;
          this.loadProvinces();
        } else {
          this.errorMessage = 'خطا در حذف استان';
        }
        this.isLoading = false;
      },
      error: (error) => {
        console.error('Error deleting province:', error);
        this.errorMessage = 'خطا در حذف استان';
        this.isLoading = false;
      }
    });
  }

  // بستن مودال‌ها
  closeModals()
    :
    void {
    this.showAddModal = false;
    this.showEditModal = false;
    this.showDeleteModal = false;
    this.clearMessages();
  }

  // پاک کردن پیام‌ها
  clearMessages()
    :
    void {
    this.successMessage = '';
    this.errorMessage = '';
  }
}
