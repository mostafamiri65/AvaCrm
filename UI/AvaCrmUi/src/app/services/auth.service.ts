import { Injectable } from '@angular/core';
import {BehaviorSubject} from 'rxjs';
import {UserDto} from '../dtos/accounts/user.dto';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private currentUserSubject =
    new BehaviorSubject<UserDto | null>(this.getUserFromStorage());
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor() {
  }

  // Store authentication data
  setAuthData(token: string, user: UserDto): void {
    localStorage.setItem('authToken', token);
    localStorage.setItem('userData', JSON.stringify(user));
    this.currentUserSubject.next(user);
  }

  // Get token from storage
  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  // Get user from storage
  private getUserFromStorage(): UserDto | null {
    const userData = localStorage.getItem('userData');
    return userData ? JSON.parse(userData) : null;
  }

  // Get current user
  getCurrentUser(): UserDto | null {
    return this.currentUserSubject.value;
  }

  // Check if user is logged in
  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  // Logout
  logout(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('userData');
    this.currentUserSubject.next(null);
  }

  // Check if user has specific role
  hasRole(roleId: number): boolean {
    const user = this.getCurrentUser();
    return user ? user.roleId === roleId : false;
  }
}
