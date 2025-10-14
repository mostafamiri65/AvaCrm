import {Component, OnInit} from '@angular/core';
import {UserChangePasswordDto, UserCreateDto, UserListDto, UserUpdateDto} from '../../../dtos/accounts/user.models';
import {RoleListDto} from '../../../dtos/accounts/role.models';
import {UserService} from '../../../services/user.service';
import {RoleService} from '../../../services/role.service';
import {NgForOf, NgIf} from '@angular/common';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-user-list.component',
  imports: [
    NgIf,
    FormsModule,
    NgForOf
  ],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.css'
})
export class UserListComponent  implements OnInit {
  users: UserListDto[] = [];
  roles: RoleListDto[] = [];
  loading = false;
  rolesLoading = false;

  // حالت‌های فرم
  isAdding = false;
  isEditing = false;
  isChangingPassword = false;

  // فرم‌ها
  newUser: UserCreateDto = {
    username: '',
    email: '',
    password: '',
    roleId: 0,
    phoneNumber: ''
  };

  editingUser: UserUpdateDto = {
    id: 0,
    username: '',
    email: '',
    roleId: 0,
    phoneNumber: '',
    lockoutEnabled: false,
    lockoutTotal: false
  };

  changePasswordDto: UserChangePasswordDto = {
    userId: 0,
    newPassword: ''
  };

  constructor(
    private userService: UserService,
    private roleService: RoleService
  ) {}

  ngOnInit(): void {
    this.loadUsers();
    this.loadRoles();
  }

  loadUsers(): void {
    this.loading = true;
    this.userService.getAll().subscribe({
      next: (response) => {
        if (response.data) {
          this.users = response.data;
        }
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  loadRoles(): void {
    this.rolesLoading = true;
    this.roleService.getAll().subscribe({
      next: (response) => {
        if (response.data) {
          this.roles = response.data;
        }
        this.rolesLoading = false;
      },
      error: () => {
        this.rolesLoading = false;
      }
    });
  }

  startAddUser(): void {
    this.isAdding = true;
    this.newUser = {
      username: '',
      email: '',
      password: '',
      roleId: 0,
      phoneNumber: ''
    };
  }

  cancelAdd(): void {
    this.isAdding = false;
  }

  saveNewUser(): void {
    if (!this.newUser.username?.trim() || !this.newUser.email?.trim() || !this.newUser.password?.trim()) return;

    this.userService.create(this.newUser).subscribe({
      next: (response) => {
        if (response.statusCode === 201) {
          this.loadUsers();
          this.cancelAdd();
        }
      },
      error: (error) => {
        console.error('Error creating user:', error);
      }
    });
  }

  startEditUser(user: UserListDto): void {
    this.isEditing = true;
    this.editingUser = {
      id: user.id,
      username: user.username,
      email: user.email,
      roleId: user.roleId,
      phoneNumber: user.phoneNumber,
      lockoutEnabled: user.lockoutEnabled,
      lockoutTotal: user.lockoutTotal
    };
  }

  cancelEdit(): void {
    this.isEditing = false;
  }

  saveEdit(): void {
    if (!this.editingUser.username?.trim() || !this.editingUser.email?.trim()) return;

    this.userService.update(this.editingUser).subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          this.loadUsers();
          this.cancelEdit();
        }
      },
      error: (error) => {
        console.error('Error updating user:', error);
      }
    });
  }

  startChangePassword(userId: number): void {
    this.isChangingPassword = true;
    this.changePasswordDto = {
      userId: userId,
      newPassword: ''
    };
  }

  cancelChangePassword(): void {
    this.isChangingPassword = false;
  }

  savePassword(): void {
    if (!this.changePasswordDto.newPassword.trim()) return;

    this.userService.changePassword(this.changePasswordDto).subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          alert('رمز عبور با موفقیت تغییر یافت');
          this.cancelChangePassword();
        }
      },
      error: (error) => {
        console.error('Error changing password:', error);
      }
    });
  }

  toggleLockout(user: UserListDto): void {
    const action = user.lockoutTotal ? 'آزاد کردن' : 'قفل کردن';
    if (confirm(`آیا از ${action} کاربر اطمینان دارید؟`)) {
      this.userService.toggleLockout(user.id).subscribe({
        next: (response) => {
          if (response.statusCode === 200) {
            this.loadUsers();
          }
        },
        error: (error) => {
          console.error('Error toggling lockout:', error);
        }
      });
    }
  }

  deleteUser(userId: number): void {
    if (confirm('آیا از حذف این کاربر اطمینان دارید؟')) {
      this.userService.delete(userId).subscribe({
        next: (response) => {
          if (response.statusCode === 200) {
            this.loadUsers();
          }
        },
        error: (error) => {
          console.error('Error deleting user:', error);
        }
      });
    }
  }

  formatDate(date: Date): string {
    return new Date(date).toLocaleDateString('fa-IR');
  }

  getLockoutBadgeClass(user: UserListDto): string {
    if (user.lockoutTotal) return 'badge bg-danger';
    if (!user.lockoutEnabled) return 'badge bg-secondary';
    return 'badge bg-success';
  }

  getLockoutText(user: UserListDto): string {
    if (user.lockoutTotal) return 'قفل شده';
    if (!user.lockoutEnabled) return 'غیرفعال';
    return 'فعال';
  }

  getEmailVerificationBadgeClass(user: UserListDto): string {
    return user.emailConfirmed ? 'badge bg-success' : 'badge bg-warning';
  }

  getEmailVerificationText(user: UserListDto): string {
    return user.emailConfirmed ? 'تایید شده' : 'در انتظار تایید';
  }
}
