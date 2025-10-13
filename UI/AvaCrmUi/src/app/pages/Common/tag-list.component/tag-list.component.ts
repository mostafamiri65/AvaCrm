import {Component, OnInit} from '@angular/core';
import {TagListDto} from '../../../dtos/Commons/tag.models';
import {TagService} from '../../../services/tag.service';
import {TagFormComponent} from '../tag-form.component/tag-form.component';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-tag-list.component',
  imports: [
    TagFormComponent,
    FormsModule
  ],
  templateUrl: './tag-list.component.html',
  styleUrl: './tag-list.component.css'
})
export class TagListComponent  implements OnInit {
  tags: TagListDto[] = [];
  loading = false;
  showTagForm = false;
  editingTag?: TagListDto;
  searchTerm: string = '';

  constructor(private tagService: TagService) { }

  ngOnInit(): void {
    this.loadTags();
  }

  loadTags(): void {
    this.loading = true;
    this.tagService.getAllTags().subscribe({
      next: (response) => {

        if (response.length>0) {
          this.tags = response;
        }
        this.loading = false;
      },
      error: (error) => {
        console.error('❌ Error loading tags:', error);
        this.loading = false;
      }
    });
  }

  searchTags(): void {
    if (this.searchTerm.trim()) {
      this.loading = true;
      this.tagService.searchTags(this.searchTerm).subscribe({
        next: (response) => {
          if (response.statusCode === 200 && response.data) {
            this.tags = response.data;
          }
          this.loading = false;
        },
        error: (error) => {
          console.error('❌ Error searching tags:', error);
          this.loading = false;
        }
      });
    } else {
      this.loadTags();
    }
  }

  openTagForm(tag?: TagListDto): void {
    this.editingTag = tag;
    this.showTagForm = true;
  }

  closeTagForm(): void {
    this.showTagForm = false;
    this.editingTag = undefined;
  }

  onTagSaved(): void {
    console.log('✅ Tag saved, reloading list...');
    this.closeTagForm();
    this.loadTags();
  }

  onTagCancelled(): void {
    console.log('❌ Tag form cancelled');
    this.closeTagForm();
  }

  deleteTag(id: number): void {
    if (confirm('آیا از حذف این تگ اطمینان دارید؟')) {
      this.tagService.delete(id).subscribe({
        next: (response) => {
          if (response.data?.doneSuccessfully) {
            this.loadTags();
          } else {
            alert(response.message || 'خطا در حذف تگ');
          }
        },
        error: (error) => {
          console.error('Error deleting tag:', error);
          alert('خطا در حذف تگ');
        }
      });
    }
  }
}
