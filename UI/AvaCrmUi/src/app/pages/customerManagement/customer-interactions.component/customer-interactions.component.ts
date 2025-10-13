import {Component, OnInit} from '@angular/core';
import {CustomerDetailDto} from '../../../dtos/CustomerManagment/customer.dto';
import {
  InteractionCreateDto,
  InteractionListDto,
  InteractionType, InteractionUpdateDto
} from '../../../dtos/CustomerManagment/interaction.models';
import {PaginationRequest} from '../../../models/base.model';
import {ActivatedRoute, Router} from '@angular/router';
import {InteractionService} from '../../../services/interaction.service';
import {CustomerService} from '../../../services/customer.service';
import moment from 'moment-jalaali';
import {NgForOf, NgIf} from '@angular/common';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-customer-interactions.component',
  imports: [
    NgIf,
    FormsModule,
    NgForOf
  ],
  templateUrl: './customer-interactions.component.html',
  styleUrl: './customer-interactions.component.css'
})
export class CustomerInteractionsComponent  implements OnInit {
  customerId!: number;
  customer!: CustomerDetailDto;
  interactions: InteractionListDto[] = [];
  loading = false;
  customerLoading = false;

  // حالت‌های مختلف
  isAdding = false;
  isEditing = false;

  // فرم‌ها
  newInteraction: InteractionCreateDto = {
    customerId: 0,
    interactionType: InteractionType.Call,
    subject: '',
    description: '',
    nextInteraction: undefined
  };

  editingInteraction: InteractionUpdateDto = {
    id: 0,
    interactionType: InteractionType.Call,
    subject: '',
    description: '',
    nextInteraction: undefined
  };

  // تاریخ‌های شمسی
  nextInteractionJalali: string = '';
  editingNextInteractionJalali: string = '';

  // enumها برای select
  interactionTypes = [
    { value: InteractionType.Call, label: 'تماس تلفنی', icon: 'fa-phone', color: 'success' },
    { value: InteractionType.Email, label: 'ایمیل', icon: 'fa-envelope', color: 'primary' },
    { value: InteractionType.Meeting, label: 'جلسه', icon: 'fa-handshake', color: 'info' },
    { value: InteractionType.Sms, label: 'پیامک', icon: 'fa-sms', color: 'warning' },
    { value: InteractionType.Online, label: 'آنلاین', icon: 'fa-globe', color: 'secondary' }
  ];

  pagination: PaginationRequest = {
    pageNumber: 1,
    pageSize: 10
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private interactionService: InteractionService,
    private customerService: CustomerService
  ) {}

  ngOnInit(): void {
    this.customerId = +this.route.snapshot.paramMap.get('id')!;
    this.newInteraction.customerId = this.customerId;
    this.loadCustomerInfo();
    this.loadCustomerInteractions();
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

  // بارگذاری تعامل‌های مشتری
  loadCustomerInteractions(): void {
    this.loading = true;
    this.interactionService.getByCustomerId(this.customerId, this.pagination)
      .subscribe({
        next: (response) => {
          if (response.data) {
            this.interactions = response.data.items;
          }
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading customer interactions:', error);
          this.loading = false;
        }
      });
  }

  // شروع افزودن تعامل جدید
  startAddInteraction(): void {
    this.isAdding = true;
    this.newInteraction = {
      customerId: this.customerId,
      interactionType: InteractionType.Call,
      subject: '',
      description: '',
      nextInteraction: undefined
    };
    this.nextInteractionJalali = '';
  }

  // لغو افزودن
  cancelAdd(): void {
    this.isAdding = false;
    this.nextInteractionJalali = '';
  }

  // تبدیل تاریخ شمسی به میلادی
  convertJalaliToGregorian(jalaliDate: string): Date | undefined {
    if (!jalaliDate) return undefined;

    try {
      const [year, month, day] = jalaliDate.split('/').map(Number);
      const momentDate = moment(`${year}/${month}/${day}`, 'jYYYY/jM/jD');
      return momentDate.toDate();
    } catch (error) {
      console.error('Error converting jalali date:', error);
      return undefined;
    }
  }

  // ذخیره تعامل جدید
  saveNewInteraction(): void {
    if (!this.newInteraction.subject.trim() || !this.newInteraction.description.trim()) return;

    // تبدیل تاریخ شمسی به میلادی
    this.newInteraction.nextInteraction = this.convertJalaliToGregorian(this.nextInteractionJalali);

    // ایجاد یک کپی از داده‌ها با تضمین نوع صحیح
    const createData: InteractionCreateDto = {
      customerId: this.newInteraction.customerId,
      interactionType: Number(this.newInteraction.interactionType), // تضمین عددی بودن
      subject: this.newInteraction.subject.trim(),
      description: this.newInteraction.description.trim(),
      nextInteraction: this.newInteraction.nextInteraction
    };

    console.log('Sending data:', createData); // برای دیباگ

    this.interactionService.create(createData).subscribe({
      next: (response) => {
        if (response.statusCode === 201) {
          this.loadCustomerInteractions();
          this.cancelAdd();
        }
      },
      error: (error) => {
        console.error('Error creating interaction:', error);
        console.error('Error details:', error.error);
      }
    });
  }

  // شروع ویرایش تعامل
  startEditInteraction(interaction: InteractionListDto): void {
    this.isEditing = true;
    this.editingInteraction = {
      id: interaction.id,
      interactionType: interaction.interactionType,
      subject: interaction.subject,
      description: interaction.description,
      nextInteraction: interaction.nextInteraction
    };

    // تبدیل تاریخ میلادی به شمسی برای نمایش
    if (interaction.nextInteraction) {
      this.editingNextInteractionJalali = this.formatToJalali(interaction.nextInteraction);
    } else {
      this.editingNextInteractionJalali = '';
    }
  }

  // لغو ویرایش
  cancelEdit(): void {
    this.isEditing = false;
    this.editingNextInteractionJalali = '';
  }

  // ذخیره ویرایش
  saveEdit(): void {
    if (!this.editingInteraction.subject.trim() || !this.editingInteraction.description.trim()) return;

    // تبدیل تاریخ شمسی به میلادی
    this.editingInteraction.nextInteraction = this.convertJalaliToGregorian(this.editingNextInteractionJalali);

    // ایجاد یک کپی از داده‌ها با تضمین نوع صحیح
    const updateData: InteractionUpdateDto = {
      id: this.editingInteraction.id,
      interactionType: Number(this.editingInteraction.interactionType), // تضمین عددی بودن
      subject: this.editingInteraction.subject.trim(),
      description: this.editingInteraction.description.trim(),
      nextInteraction: this.editingInteraction.nextInteraction
    };

    console.log('Sending update data:', updateData); // برای دیباگ

    this.interactionService.update(updateData).subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          this.loadCustomerInteractions();
          this.cancelEdit();
        }
      },
      error: (error) => {
        console.error('Error updating interaction:', error);
        console.error('Error details:', error.error);
      }
    });
  }

  // حذف تعامل
  deleteInteraction(interactionId: number): void {
    if (confirm('آیا از حذف این تعامل اطمینان دارید؟')) {
      this.interactionService.delete(interactionId).subscribe({
        next: (response) => {
          if (response.statusCode === 200) {
            this.loadCustomerInteractions();
          }
        },
        error: (error) => {
          console.error('Error deleting interaction:', error);
        }
      });
    }
  }

  // دریافت متن نوع تعامل
  getInteractionTypeText(type: InteractionType): string {
    const interactionType = this.interactionTypes.find(t => t.value === type);
    return interactionType ? interactionType.label : 'نامشخص';
  }

  // دریافت آیکون نوع تعامل
  getInteractionTypeIcon(type: InteractionType): string {
    const interactionType = this.interactionTypes.find(t => t.value === type);
    return interactionType ? interactionType.icon : 'fa-question';
  }

  // دریافت کلاس رنگ نوع تعامل
  getInteractionTypeColor(type: InteractionType): string {
    const interactionType = this.interactionTypes.find(t => t.value === type);
    return interactionType ? `bg-${interactionType.color}` : 'bg-secondary';
  }

  // فرمت تاریخ به شمسی
  formatToJalali(date: Date): string {
    return moment(date).format('jYYYY/jMM/jDD');
  }

  // فرمت تاریخ و زمان به شمسی
  formatToJalaliDateTime(date: Date): string {
    return moment(date).format('jYYYY/jMM/jDD HH:mm');
  }

  // بازگشت به لیست مشتریان
  backToCustomers(): void {
    this.router.navigate(['/customers']);
  }

  // دریافت نام مشتری
  getCustomerName(): string {
    if (!this.customer) return '';
    return this.customer.individualCustomer
      ? `${this.customer.individualCustomer.firstName} ${this.customer.individualCustomer.lastName}`  || ''
      : this.customer.organizationCustomer?.companyName || '';

  }

  // دریافت متن نوع مشتری
  getCustomerTypeText(type: number): string {
    return type === 1 ? 'حقیقی' : 'حقوقی';
  }
}
