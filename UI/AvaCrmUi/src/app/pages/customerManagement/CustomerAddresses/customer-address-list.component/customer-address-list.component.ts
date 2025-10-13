import {Component, Input, OnInit} from '@angular/core';
import {CustomerAddressFormComponent} from '../customer-address-form.component/customer-address-form.component';
import {CustomerAddressListDto} from '../../../../dtos/CustomerManagment/customer-address.models';
import {PaginationRequest} from '../../../../models/base.model';
import {CustomerAddressService} from '../../../../services/customer-address.service';

@Component({
  selector: 'app-customer-address-list',
  imports: [
    CustomerAddressFormComponent
  ],
  templateUrl: './customer-address-list.component.html',
  styleUrl: './customer-address-list.component.css'
})
export class CustomerAddressListComponent implements OnInit {

  @Input() customerId!: number;

  addresses: CustomerAddressListDto[] = [];
  loading = false;
  showAddressForm = false;
  editingAddressId?: number;
  paginationRequest: PaginationRequest = {
    pageNumber: 1,
    pageSize: 10
  };
  totalCount = 0;

  constructor(private customerAddressService: CustomerAddressService) { }

  ngOnInit(): void {

    if (this.customerId && this.customerId > 0) {
      this.loadAddresses();
    } else {
      console.error('❌ Invalid customerId in CustomerAddressList:', this.customerId);
    }
  }

  loadAddresses(): void {
    this.loading = true;
    this.customerAddressService.getByCustomerId(this.customerId, this.paginationRequest)
      .subscribe({
        next: (response) => {
          if (response.statusCode === 200 && response.data) {
            this.addresses = response.data.items;
            this.totalCount = response.data.totalCount;
          }
          this.loading = false;
        },
        error: (error) => {
          console.error('❌ Error loading addresses:', error);
          this.loading = false;
        }
      });
  }

  openAddressForm(addressId?: number): void {
    this.editingAddressId = addressId;
    this.showAddressForm = true;
  }

  closeAddressForm(): void {
    this.showAddressForm = false;
    this.editingAddressId = undefined;
  }

  onAddressSaved(): void {
    this.closeAddressForm();
    this.loadAddresses();
  }

  onAddressCancelled(): void {
    this.closeAddressForm();
  }

  editAddress(address: CustomerAddressListDto): void {
    this.openAddressForm(address.id);
  }

  deleteAddress(id: number): void {
    if (confirm('آیا از حذف این آدرس اطمینان دارید؟')) {
      this.customerAddressService.delete(id).subscribe({
        next: (response) => {
          if (response.data?.doneSuccessfully) {
            this.loadAddresses();
          } else {
            alert(response.message || 'خطا در حذف آدرس');
          }
        },
        error: (error) => {
          console.error('Error deleting address:', error);
          alert('خطا در حذف آدرس');
        }
      });
    }
  }

  onPageChange(pageNumber: number): void {
    this.paginationRequest.pageNumber = pageNumber;
    this.loadAddresses();
  }

  getPageNumbers(): number[] {
    const totalPages = Math.ceil(this.totalCount / this.paginationRequest.pageSize);
    return Array.from({ length: totalPages }, (_, i) => i + 1);
  }
}
