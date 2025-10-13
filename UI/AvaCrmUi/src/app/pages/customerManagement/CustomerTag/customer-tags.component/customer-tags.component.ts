import {Component, OnInit} from '@angular/core';
import {CustomerTagCreateDto, CustomerTagListDto} from '../../../../dtos/CustomerManagment/customer-tag.dto';
import {PaginationRequest} from '../../../../models/base.model';
import {ActivatedRoute, Router} from '@angular/router';
import {CustomerTagService} from '../../../../services/customer-tag-service';
import {TagService} from '../../../../services/tag.service';
import {FormsModule} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-customer-tags',
  imports: [
    FormsModule,
    NgForOf,
    NgIf
  ],
  templateUrl: './customer-tags.component.html',
  styleUrl: './customer-tags.component.css'
})
export class CustomerTagsComponent implements OnInit {
  customerId!: number;
  customerTags: CustomerTagListDto[] = [];
  availableTags: any[] = []; // لیست تگ‌های موجود
  selectedTagId?: number;
  loading = false;
  pagination: PaginationRequest = {
    pageNumber: 1,
    pageSize: 10
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private customerTagService: CustomerTagService,
    private tagService: TagService
  ) {}

  ngOnInit(): void {
    this.customerId = +this.route.snapshot.paramMap.get('id')!;
    this.loadCustomerTags();
    this.loadAvailableTags();
  }

  // بارگذاری تگ‌های مشتری
  loadCustomerTags(): void {
    this.loading = true;
    this.customerTagService.getByCustomerId(this.customerId, this.pagination)
      .subscribe({
        next: (response) => {
          if (response.data) {
            this.customerTags = response.data.items;
          }
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading customer tags:', error);
          this.loading = false;
        }
      });
  }

  // بارگذاری تگ‌های موجود
  loadAvailableTags(): void {
    this.tagService.getAllTags().subscribe({
      next: (response) => {
        if (response) {
          this.availableTags = response;
        }
      },
      error: (error) => {
        console.error('Error loading available tags:', error);
      }
    });
  }

  // افزودن تگ به مشتری
  addTag(): void {
    if (!this.selectedTagId) return;

    const createDto: CustomerTagCreateDto = {
      customerId: this.customerId,
      tagId: this.selectedTagId
    };

    this.customerTagService.addTagToCustomer(createDto).subscribe({
      next: (response) => {
        if (response.statusCode === 201) {
          this.loadCustomerTags(); // رفرش لیست
          this.selectedTagId = undefined;
        }
      },
      error: (error) => {
        console.error('Error adding tag:', error);
      }
    });
  }

  // حذف تگ از مشتری
  removeTag(tagId: number): void {
    if (confirm('آیا از حذف این تگ اطمینان دارید؟')) {
      this.customerTagService.removeTagFromCustomer(this.customerId, tagId)
        .subscribe({
          next: (response) => {
            if (response.statusCode === 200) {
              this.loadCustomerTags(); // رفرش لیست
            }
          },
          error: (error) => {
            console.error('Error removing tag:', error);
          }
        });
    }
  }

  // بازگشت به لیست مشتریان
  backToCustomers(): void {
    this.router.navigate(['/customers']);
  }
}
