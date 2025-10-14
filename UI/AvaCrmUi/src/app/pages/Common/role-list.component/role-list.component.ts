import {Component, OnInit} from '@angular/core';
import {RoleCreateDto, RoleListDto, RoleUpdateDto} from '../../../dtos/accounts/role.models';
import {RoleService} from '../../../services/role.service';
import {FormsModule} from '@angular/forms';
import {NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-role-list.component',
  imports: [
    FormsModule,
    NgIf,
    NgForOf
  ],
  templateUrl: './role-list.component.html',
  styleUrl: './role-list.component.css'
})
export class RoleListComponent implements OnInit {
  roles: RoleListDto[] = [];
  loading = false;

  // حالت‌های فرم
  isAdding = false;
  isEditing = false;

  // فرم‌ها
  newRole: RoleCreateDto = {
    titleEnglish: '',
    titlePersian: ''
  };

  editingRole: RoleUpdateDto = {
    id: 0,
    titleEnglish: '',
    titlePersian: ''
  };

  constructor(private roleService: RoleService) {}

  ngOnInit(): void {
    this.loadRoles();
  }

  loadRoles(): void {
    this.loading = true;
    this.roleService.getAll().subscribe({
      next: (response) => {
        if (response.data) {
          this.roles = response.data;
        }
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  startAddRole(): void {
    this.isAdding = true;
    this.newRole = { titleEnglish: '', titlePersian: '' };
  }

  cancelAdd(): void {
    this.isAdding = false;
  }

  saveNewRole(): void {
    if (!this.newRole.titlePersian?.trim()) return;

    this.roleService.create(this.newRole).subscribe({
      next: (response) => {
        if (response.statusCode === 201) {
          this.loadRoles();
          this.cancelAdd();
        }
      },
      error: (error) => {
        console.error('Error creating role:', error);
      }
    });
  }

  startEditRole(role: RoleListDto): void {
    this.isEditing = true;
    this.editingRole = {
      id: role.id,
      titleEnglish: role.titleEnglish,
      titlePersian: role.titlePersian
    };
  }

  cancelEdit(): void {
    this.isEditing = false;
  }

  saveEdit(): void {
    if (!this.editingRole.titlePersian?.trim()) return;

    this.roleService.update(this.editingRole).subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          this.loadRoles();
          this.cancelEdit();
        }
      },
      error: (error) => {
        console.error('Error updating role:', error);
      }
    });
  }

  deleteRole(roleId: number): void {
    if (confirm('آیا از حذف این نقش اطمینان دارید؟')) {
      this.roleService.delete(roleId).subscribe({
        next: (response) => {
          if (response.statusCode === 200) {
            this.loadRoles();
          }
        },
        error: (error) => {
          console.error('Error deleting role:', error);
        }
      });
    }
  }

  formatDate(date: Date): string {
    return new Date(date).toLocaleDateString('fa-IR');
  }
}
