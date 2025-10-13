import {Component, OnInit} from '@angular/core';
import {NoteCreateDto, NoteListDto, NoteUpdateDto} from '../../../../dtos/CustomerManagment/note.models';
import {PaginationRequest} from '../../../../models/base.model';
import {ActivatedRoute, Router} from '@angular/router';
import {NoteService} from '../../../../services/note.service';
import {FormsModule} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';
import {CustomerDetailDto} from '../../../../dtos/CustomerManagment/customer.dto';
import {CustomerService} from '../../../../services/customer.service';

@Component({
  selector: 'app-customer-notes.component',
  imports: [
    FormsModule,
    NgIf,
    NgForOf
  ],
  templateUrl: './customer-notes.component.html',
  styleUrl: './customer-notes.component.css'
})
export class CustomerNotesComponent implements OnInit {
  customerId!: number;
  notes: NoteListDto[] = [];
  loading = false;
  customer!: CustomerDetailDto;
  // حالت‌های مختلف
  isAdding = false;
  isEditing = false;
  customerLoading = false;
  // فرم‌ها
  newNote: NoteCreateDto = {
    customerId: 0,
    content: ''
  };

  editingNote: NoteUpdateDto = {
    id: 0,
    content: ''
  };

  pagination: PaginationRequest = {
    pageNumber: 1,
    pageSize: 10
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private noteService: NoteService,
    private customerService: CustomerService
  ) {}

  ngOnInit(): void {
    this.customerId = +this.route.snapshot.paramMap.get('id')!;
    this.newNote.customerId = this.customerId;
    this.loadCustomerInfo();
    this.loadCustomerNotes();
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
  // بارگذاری یادداشت‌های مشتری
  loadCustomerNotes(): void {
    this.loading = true;
    this.noteService.getByCustomerId(this.customerId, this.pagination)
      .subscribe({
        next: (response) => {
          if (response.data) {
            this.notes = response.data.items;
          }
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading customer notes:', error);
          this.loading = false;
        }
      });
  }
  // دریافت نام مشتری
  getCustomerName(): string {
    if (!this.customer) return '';
    return this.customer.individualCustomer
      ? `${this.customer.individualCustomer.firstName} ${this.customer.individualCustomer.lastName}`  || ''
      : this.customer.organizationCustomer?.companyName || '';
  }

  // شروع افزودن یادداشت جدید
  startAddNote(): void {
    this.isAdding = true;
    this.newNote.content = '';
  }

  // لغو افزودن
  cancelAdd(): void {
    this.isAdding = false;
    this.newNote.content = '';
  }

  // ذخیره یادداشت جدید
  saveNewNote(): void {
    if (!this.newNote.content.trim()) return;

    this.noteService.create(this.newNote).subscribe({
      next: (response) => {
        if (response.statusCode === 201) {
          this.loadCustomerNotes();
          this.cancelAdd();
        }
      },
      error: (error) => {
        console.error('Error creating note:', error);
      }
    });
  }

  // شروع ویرایش یادداشت
  startEditNote(note: NoteListDto): void {
    this.isEditing = true;
    this.editingNote = {
      id: note.id,
      content: note.content
    };
  }

  // لغو ویرایش
  cancelEdit(): void {
    this.isEditing = false;
    this.editingNote = { id: 0, content: '' };
  }

  // ذخیره ویرایش
  saveEdit(): void {
    if (!this.editingNote.content.trim()) return;

    this.noteService.update(this.editingNote).subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          this.loadCustomerNotes();
          this.cancelEdit();
        }
      },
      error: (error) => {
        console.error('Error updating note:', error);
      }
    });
  }

  // حذف یادداشت
  deleteNote(noteId: number): void {
    if (confirm('آیا از حذف این یادداشت اطمینان دارید؟')) {
      this.noteService.delete(noteId).subscribe({
        next: (response) => {
          if (response.statusCode === 200) {
            this.loadCustomerNotes();
          }
        },
        error: (error) => {
          console.error('Error deleting note:', error);
        }
      });
    }
  }

  // فرمت کردن تاریخ
  formatDate(date: Date): string {
    return new Date(date).toLocaleString('fa-IR');
  }

  // بازگشت به لیست مشتریان
  backToCustomers(): void {
    this.router.navigate(['/customers']);
  }
}
