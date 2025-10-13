import {Component, OnInit} from '@angular/core';
import {CustomerDetailDto} from '../../../../dtos/CustomerManagment/customer.dto';
import {ActivatedRoute, Router} from '@angular/router';
import {CustomerService} from '../../../../services/customer.service';
import {CustomerType} from '../../../../dtos/CustomerManagment/customer.enum';
import {NgIf} from '@angular/common';
import {CustomerAddressListComponent} from '../customer-address-list.component/customer-address-list.component';

@Component({
  selector: 'app-customer-addresses.component',
  imports: [
    NgIf,
    CustomerAddressListComponent
  ],
  templateUrl: './customer-addresses.component.html',
  styleUrl: './customer-addresses.component.css'
})
export class CustomerAddressesComponent implements OnInit {
  customerId!: number;
  customer!: CustomerDetailDto;
  loading = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private customerService: CustomerService
  ) { }

  ngOnInit(): void {
    // گرفتن customerId از route parameters
    this.route.params.subscribe(params => {
      this.customerId = +params['id'];

      if (this.customerId) {
        this.loadCustomerDetails();
      } else {
        console.error('❌ Customer ID is missing from route');
      }
    });
  }

  loadCustomerDetails(): void {
    this.loading = true;
    this.customerService.getCustomerById(this.customerId).subscribe({
      next: (response) => {
        if (response.statusCode === 200 && response.data) {
          this.customer = response.data;
        } else {
          console.error('❌ Error loading customer details:', response.message);
        }
        this.loading = false;
      },
      error: (error) => {
        console.error('❌ Error loading customer details:', error);
        this.loading = false;
      }
    });
  }

  getCustomerName(): string {
    if (!this.customer) return '';
    if (this.customer.individualCustomer) {
      return `${this.customer.individualCustomer.firstName} ${this.customer.individualCustomer.lastName}`;
    } else if ( this.customer.organizationCustomer) {
      return this.customer.organizationCustomer.companyName;
    }

    return this.customer.code;
  }

  goBack(): void {
    this.router.navigate(['/customers']);
  }
}
