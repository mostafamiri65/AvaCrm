import {Component, Input, OnInit} from '@angular/core';
import {ContactPersonListDto} from '../../../../dtos/CustomerManagment/contact-person.models';
import {PaginationRequest} from '../../../../models/base.model';
import {ContactPersonService} from '../../../../services/contact-person.service';
import {ContactPersonFormComponent} from '../contact-person-form.component/contact-person-form.component';

@Component({
  selector: 'app-contact-person-list',
  imports: [
    ContactPersonFormComponent
  ],
  templateUrl: './contact-person-list.html',
  styleUrl: './contact-person-list.css'
})
export class ContactPersonList implements  OnInit {

  @Input() customerId!: number;

  contactPersons: ContactPersonListDto[] = [];
  loading = false;
  showContactPersonForm = false;
  editingContactPersonId?: number;
  paginationRequest: PaginationRequest = {
    pageNumber: 1,
    pageSize: 10
  };
  totalCount = 0;

  constructor(private contactPersonService: ContactPersonService) { }

  ngOnInit(): void {

    if (this.customerId && this.customerId > 0) {
      this.loadContactPersons();
    } else {
      console.error('❌ Invalid customerId in ContactPersonList:', this.customerId);
    }
  }

  loadContactPersons(): void {
    this.loading = true;
    this.contactPersonService.getByCustomerId(this.customerId, this.paginationRequest)
      .subscribe({
        next: (response) => {
          if (response.statusCode === 200 && response.data) {
            this.contactPersons = response.data.items;
            this.totalCount = response.data.totalCount;
          }
          this.loading = false;
        },
        error: (error) => {
          console.error('❌ Error loading contact persons:', error);
          this.loading = false;
        }
      });
  }

  openContactPersonForm(contactPersonId?: number): void {
   this.editingContactPersonId = contactPersonId;
    this.showContactPersonForm = true;
  }

  closeContactPersonForm(): void {
    this.showContactPersonForm = false;
    this.editingContactPersonId = undefined;
  }

  onContactPersonSaved(): void {
    this.closeContactPersonForm();
    this.loadContactPersons();
  }

  onContactPersonCancelled(): void {
    this.closeContactPersonForm();
  }

  editContactPerson(contactPerson: ContactPersonListDto): void {
    this.openContactPersonForm(contactPerson.id);
  }

  deleteContactPerson(id: number): void {
    if (confirm('آیا از حذف این فرد ارتباطی اطمینان دارید؟')) {
      this.contactPersonService.delete(id).subscribe({
        next: (response) => {
          if (response.data?.doneSuccessfully) {
            this.loadContactPersons();
          } else {
            alert(response.message || 'خطا در حذف فرد ارتباطی');
          }
        },
        error: (error) => {
          console.error('Error deleting contact person:', error);
          alert('خطا در حذف فرد ارتباطی');
        }
      });
    }
  }

  onPageChange(pageNumber: number): void {
    this.paginationRequest.pageNumber = pageNumber;
    this.loadContactPersons();
  }

  getPageNumbers(): number[] {
    const totalPages = Math.ceil(this.totalCount / this.paginationRequest.pageSize);
    return Array.from({ length: totalPages }, (_, i) => i + 1);
  }
}
