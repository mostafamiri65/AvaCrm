import {Component, OnInit} from '@angular/core';
import {CustomerListDto} from '../../../../dtos/CustomerManagment/customer.dto';
import {PaginationRequest} from '../../../../models/base.model';
import {CustomerService} from '../../../../services/customer.service';
import {CustomerType} from '../../../../dtos/CustomerManagment/customer.enum';
import {Router, RouterLink} from '@angular/router';
import {FormsModule} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-customer-list',
  imports: [
    RouterLink,
    FormsModule,
    NgIf,
    NgForOf
  ],
  templateUrl: './customer-list.component.html',
  styleUrl: './customer-list.component.css'
})
export class CustomerListComponent implements OnInit {
  customers: CustomerListDto[] = [];
  paginationRequest: PaginationRequest = {
    pageNumber: 1,
    pageSize: 10,
    sortColumn: 'createdDate',
    sortDirection: 'desc'
  };
  totalCount: number = 0;
  loading: boolean = false;
  searchTerm: string = '';

  constructor(private customerService: CustomerService,
              private router: Router) { }

  ngOnInit(): void {
    this.loadCustomers();
  }

  loadCustomers(): void {
    this.loading = true;
    this.paginationRequest.searchTerm = this.searchTerm;

    this.customerService.getAllCustomers(this.paginationRequest).subscribe({
      next: (response) => {
        if (response.statusCode === 200 && response.data) {
          this.customers = response.data.items;
          this.totalCount = response.data.totalCount;
        }
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading customers:', error);
        this.loading = false;
      }
    });
  }

  onSearch(): void {
    this.paginationRequest.pageNumber = 1;
    this.loadCustomers();
  }

  onPageChange(pageNumber: number): void {
    this.paginationRequest.pageNumber = pageNumber;
    this.loadCustomers();
  }

  deleteCustomer(id: number): void {
    if (confirm('آیا از حذف این مشتری اطمینان دارید؟')) {
      this.customerService.deleteCustomer(id).subscribe({
        next: (response) => {
          if (response.data?.doneSuccessfully) {
            this.loadCustomers();
          }
        },
        error: (error) => {
          console.error('Error deleting customer:', error);
        }
      });
    }
  }

  // رفتن به صفحه مدیریت افراد ارتباطی مشتری - جدید
  goToCustomerContactPersons(customerId: number): void {
    this.router.navigate(['/customers', customerId, 'contact-persons']);
  }
  goToCustomerTags(customerId: number): void {
    this.router.navigate(['/customers/tags', customerId]);
  }
  goToCustomerFollowUps(customerId: number): void {
    this.router.navigate(['/customers/follow-ups', customerId]);
  }
  goToCustomerNotes(customerId: number): void {
    this.router.navigate(['/customers/notes', customerId]);
  }
  goToCustomerInteractions(customerId: number): void {
    this.router.navigate(['/customers/interactions', customerId]);
  }
  // رفتن به صفحه مدیریت آدرس‌های مشتری
  goToCustomerAddresses(customerId: number): void {
    this.router.navigate(['/customers', customerId, 'addresses']);
  }
  getCustomerTypeText(type: number): string {
    return type === 1 ? 'حقیقی' : 'حقوقی';
  }

  protected readonly CustomerType = CustomerType;
  protected readonly Math = Math;
}
