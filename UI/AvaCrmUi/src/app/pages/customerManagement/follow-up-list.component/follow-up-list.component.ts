import {Component, OnInit} from '@angular/core';
import {FollowUpCreateDto, FollowUpListDto, FollowUpUpdateDto} from '../../../dtos/CustomerManagment/follow-up.models';
import {PaginatedResult, PaginationRequest} from '../../../models/base.model';
import {FollowUpService} from '../../../services/follow-up.service';
import {ActivatedRoute, Router} from '@angular/router';
import {CustomerService} from '../../../services/customer.service';
import {CustomerDetailDto} from '../../../dtos/CustomerManagment/customer.dto';
import {FormsModule} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-follow-up-list.component',
  imports: [
    FormsModule,
    NgIf,
    NgForOf
  ],
  templateUrl: './follow-up-list.component.html',
  styleUrl: './follow-up-list.component.css'
})
export class FollowUpListComponent  implements OnInit {

  customerId!: number;
  followUps: FollowUpListDto[] = [];
  loading = false;
  customer!: CustomerDetailDto;

  // حالت‌های مختلف
  isAdding = false;
  isEditing = false;
  customerLoading = false;

  // فرم‌ها
  newFollowUp: FollowUpCreateDto = {
    customerId: 0,
    description: '',
    nextFollowUpDate: undefined,
    nextFollowUpDescription: ''
  };

  editingFollowUp: FollowUpUpdateDto = {
    id: 0,
    description: '',
    nextFollowUpDate: undefined,
    nextFollowUpDescription: ''
  };

  pagination: PaginationRequest = {
    pageNumber: 1,
    pageSize: 100
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private followUpService: FollowUpService,
    private customerService: CustomerService
  ) {}

  ngOnInit(): void {
    this.customerId = +this.route.snapshot.paramMap.get('id')!;
    this.newFollowUp.customerId = this.customerId;
    this.loadCustomerInfo();
    this.loadCustomerFollowUps();
  }

  // بارگذاری اطلاعات مشتری
  loadCustomerInfo(): void {
    this.customerLoading = true;
    this.customerService.getCustomerById(this.customerId)
      .subscribe({
        next: (response) => {
          if (response.data) {
            this.customer = response.data;
          }
          this.customerLoading = false;
        },
        error: (error) => {
          console.error('Error loading customer info:', error);
          this.customerLoading = false;
        }
      });
  }

  // بارگذاری پیگیری‌های مشتری
  loadCustomerFollowUps(): void {
    this.loading = true;
    this.followUpService.getByCustomerId(this.customerId, this.pagination)
      .subscribe({
        next: (response) => {
          if (response.data) {
            this.followUps = response.data.items;
          }
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading customer follow-ups:', error);
          this.loading = false;
        }
      });
  }

  // دریافت نام مشتری
  getCustomerName(): string {
    if (!this.customer) return '';
    return this.customer.individualCustomer
      ? `${this.customer.individualCustomer.firstName} ${this.customer.individualCustomer.lastName}` || ''
      : this.customer.organizationCustomer?.companyName || '';
  }

  // شروع افزودن پیگیری جدید
  startAddFollowUp(): void {
    this.isAdding = true;
    this.newFollowUp = {
      customerId: this.customerId,
      description: '',
      nextFollowUpDate: undefined,
      nextFollowUpDescription: ''
    };
  }

  // لغو افزودن
  cancelAdd(): void {
    this.isAdding = false;
  }

  // ذخیره پیگیری جدید
  saveNewFollowUp(): void {
    if (!this.newFollowUp.description.trim()) return;

    this.followUpService.create(this.newFollowUp).subscribe({
      next: (response) => {
        if (response.statusCode === 201) {
          this.loadCustomerFollowUps();
          this.cancelAdd();
        }
      },
      error: (error) => {
        console.error('Error creating follow-up:', error);
      }
    });
  }

  // شروع ویرایش پیگیری
  startEditFollowUp(followUp: FollowUpListDto): void {
    this.isEditing = true;
    this.editingFollowUp = {
      id: followUp.id,
      description: followUp.description,
      nextFollowUpDate: followUp.nextFollowUpDate,
      nextFollowUpDescription: followUp.nextFollowUpDescription
    };
  }

  // لغو ویرایش
  cancelEdit(): void {
    this.isEditing = false;
    this.editingFollowUp = {
      id: 0,
      description: '',
      nextFollowUpDate: undefined,
      nextFollowUpDescription: ''
    };
  }

  // ذخیره ویرایش
  saveEdit(): void {
    if (!this.editingFollowUp.description.trim()) return;

    this.followUpService.update(this.editingFollowUp).subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          this.loadCustomerFollowUps();
          this.cancelEdit();
        }
      },
      error: (error) => {
        console.error('Error updating follow-up:', error);
      }
    });
  }

  // حذف پیگیری
  deleteFollowUp(followUpId: number): void {
    if (confirm('آیا از حذف این پیگیری اطمینان دارید؟')) {
      this.followUpService.delete(followUpId).subscribe({
        next: (response) => {
          if (response.statusCode === 200) {
            this.loadCustomerFollowUps();
          }
        },
        error: (error) => {
          console.error('Error deleting follow-up:', error);
        }
      });
    }
  }
  formatDateForInput(date: Date): string {
    const d = new Date(date);
    return d.toISOString().slice(0, 16);
  }
  // فرمت کردن تاریخ
  formatDate(date: Date): string {
    return new Date(date).toLocaleString('fa-IR');
  }
  onEditingFollowUpDateChange(event: any): void {
    const value = event.target.value;
    this.editingFollowUp.nextFollowUpDate = value ? new Date(value) : undefined;
  }
  // بررسی تاریخ گذشته
  isOverdue(followUp: FollowUpListDto): boolean {
    if (!followUp.nextFollowUpDate) return false;
    return new Date(followUp.nextFollowUpDate) < new Date();
  }

  // بازگشت به لیست مشتریان
  backToCustomers(): void {
    this.router.navigate(['/customers']);
  }

  protected readonly Date = Date;
}
