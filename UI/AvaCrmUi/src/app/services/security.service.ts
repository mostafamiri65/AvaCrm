import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SecurityService {
  private failedAttempts = new BehaviorSubject<number>(0);
  public failedAttempts$ = this.failedAttempts.asObservable();

  constructor() {}

  incrementFailedAttempts(): void {
    const current = this.failedAttempts.value;
    this.failedAttempts.next(current + 1);
  }

  resetFailedAttempts(): void {
    this.failedAttempts.next(0);
  }

  getFailedAttempts(): number {
    return this.failedAttempts.value;
  }
}
