import {Component, OnInit} from '@angular/core';
import {CustomerDetailDto} from '../../../../dtos/CustomerManagment/customer.dto';
import {ActivatedRoute, Router} from '@angular/router';
import {CustomerService} from '../../../../services/customer.service';
import {ContactPersonList} from '../contact-person-list/contact-person-list';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-customer-contact-persons.component',
  imports: [
    ContactPersonList,
    NgIf
  ],
  templateUrl: './customer-contact-persons.component.html',
  styleUrl: './customer-contact-persons.component.css'
})
export class CustomerContactPersonsComponent implements OnInit {
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

    if ( this.customer.individualCustomer) {
      return `${this.customer.individualCustomer.firstName} ${this.customer.individualCustomer.lastName}`;
    } else if (this.customer.organizationCustomer) {
      return this.customer.organizationCustomer.companyName;
    }

    return this.customer.code;
  }

  goBack(): void {
    this.router.navigate(['/customers']);
  }
}
