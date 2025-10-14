import {Component, OnInit} from '@angular/core';
import {UserDto} from '../../../dtos/accounts/user.dto';
import {AuthService} from '../../../services/auth.service';
import {Router, RouterLink} from '@angular/router';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-nav-topbar',
  imports: [
    RouterLink,
    NgIf
  ],
  templateUrl: './nav-topbar.component.html',
  styleUrl: './nav-topbar.component.css'
})
export class NavTopbarComponent implements OnInit {
  currentUser: UserDto | null = null;
  isDropdownOpen = false;

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Subscribe to current user changes
    this.authService.currentUser$.subscribe(user => {
      this.currentUser = user;
    });

    // Get initial user data
    this.currentUser = this.authService.getCurrentUser();
  }

  // Get user display name
  getUserDisplayName(): string {
    if (!this.currentUser) return 'کاربر';

    // اگر نام کامل در UserDto دارید، از آن استفاده کنید
    // در غیر این صورت از username استفاده می‌کنیم
    return this.currentUser.userName || 'کاربر';
  }

  // Get user role display name
  getUserRoleName(): string {
    if (!this.currentUser) return 'بدون نقش';
    return this.currentUser.phoneNumber  || 'کاربر';
  }

  // Logout user
  logout(): void {
    if (confirm('آیا از خروج از سیستم اطمینان دارید؟')) {
      this.authService.logout();
      this.router.navigate(['/login']);
    }
  }

  // Toggle dropdown
  toggleDropdown(): void {
    this.isDropdownOpen = !this.isDropdownOpen;
  }

  // Close dropdown
  closeDropdown(): void {
    this.isDropdownOpen = false;
  }

  // Check if user is logged in
  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }
}
