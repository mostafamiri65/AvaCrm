import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {
  CaptchaRequest,
  CaptchaResponse,
  SecurityCheckRequest,
  SecurityCheckResult
} from '../dtos/accounts/captcha.model';

@Injectable({
  providedIn: 'root'
})
export class CaptchaService {
  private apiUrl = '/captcha';

  constructor(private http: HttpClient) { }

  generateCaptcha(): Observable<CaptchaResponse> {
    return this.http.post<CaptchaResponse>(`${this.apiUrl}/generate`, {});
  }

  verifyCaptcha(request: CaptchaRequest): Observable<CaptchaResponse> {
    return this.http.post<CaptchaResponse>(`${this.apiUrl}/verify`, request);
  }

  checkSecurity(request: SecurityCheckRequest): Observable<SecurityCheckResult> {
    return this.http.post<SecurityCheckResult>(`${this.apiUrl}/check-security`, request);
  }
}
