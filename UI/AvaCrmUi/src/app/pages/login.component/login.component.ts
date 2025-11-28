import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {AccountService} from '../../services/account.service';
import {AuthService} from '../../services/auth.service';
import {LoginResponseDto} from '../../dtos/receivedResponse.dto';
import {LoginState} from '../../dtos/accounts/user.enum';
import {LoginDto} from '../../dtos/accounts/login.dto';
import {UserDto} from '../../dtos/accounts/user.dto';
import {FormsModule} from '@angular/forms';
import {NgIf} from '@angular/common';
import {CaptchaChallenge, SecurityCheckResult} from '../../dtos/accounts/captcha.model';
import {CaptchaService} from '../../services/captcha.service';
import {SecurityService} from '../../services/security.service';

@Component({
  selector: 'app-login',
  imports: [
    FormsModule,
    NgIf
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  model: LoginDto = {
    username: '',
    password: ''
  };

  captchaChallenge: CaptchaChallenge | null = null;
  showCaptcha = false;
  isLoading = false;
  isCheckingSecurity = false;
  errorMessage = '';
  successMessage = '';
  securityCheckResult: SecurityCheckResult | null = null;

  constructor(
    private accountService: AccountService,
    private router: Router,
    private authService: AuthService,
    private captchaService: CaptchaService,
    private securityService: SecurityService
  ) {}

  ngOnInit(): void {
    // ریست کردن شمارشگر اشتباهات هنگام لود کامپوننت
    this.securityService.resetFailedAttempts();
  }

  async loginSubmit(): Promise<void> {
    // اعتبارسنجی اولیه
    if (!this.model.username || !this.model.password) {
      this.errorMessage = 'نام کاربری و کلمه عبور را وارد نمایید';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

    try {
      // اگر کپچا نمایش داده نشده، اول بررسی امنیت کن
      if (!this.showCaptcha) {
        await this.checkSecurity();
      } else {
        // اگر کپچا نمایش داده شده، اعتبارسنجی کن
        if (!this.model.captchaAnswer) {
          this.errorMessage = 'لطفا کد امنیتی را وارد کنید';
          this.isLoading = false;
          return;
        }
        await this.performLogin();
      }
    } catch (error) {
      this.isLoading = false;
      this.errorMessage = 'خطا در ارتباط با سرور';
    }
  }

  private async checkSecurity(): Promise<void> {
    this.isCheckingSecurity = true;

    const request = {
      username: this.model.username,
      ipAddress: await this.getClientIp()
    };

    this.captchaService.checkSecurity(request).subscribe({
      next: (result: SecurityCheckResult) => {
        this.isCheckingSecurity = false;
        this.securityCheckResult = result;

        if (result.isBlocked) {
          this.errorMessage = result.message;
          this.isLoading = false;
        } else if (result.requiresCaptcha) {
          this.showCaptcha = true;
          this.loadCaptcha();
        } else {
          this.performLogin();
        }
      },
      error: (error) => {
        this.isCheckingSecurity = false;
        console.error('Security check error:', error);
        // در صورت خطا، بدون کپچا ادامه بده
        this.performLogin();
      }
    });
  }

  private loadCaptcha(): void {
    this.captchaService.generateCaptcha().subscribe({
      next: (response) => {
        if (response.success && response.challenge) {
          this.captchaChallenge = response.challenge;
          this.model.captchaId = response.challenge.id;
          this.isLoading = false;
        } else {
          this.errorMessage = 'خطا در تولید کد امنیتی';
          this.isLoading = false;
        }
      },
      error: (error) => {
        console.error('Captcha generation error:', error);
        this.errorMessage = 'خطا در بارگذاری کد امنیتی';
        this.isLoading = false;
      }
    });
  }

  refreshCaptcha(): void {
    this.model.captchaAnswer = '';
    this.loadCaptcha();
  }

  selectCaptchaOption(option: string): void {
    this.model.captchaAnswer = option;
  }

  private performLogin(): void {
    this.accountService.login(this.model).subscribe({
      next: (response: LoginResponseDto) => {
        this.isLoading = false;
        this.handleApiResponse(response);
      },
      error: (error) => {
        this.isLoading = false;
        this.handleLoginError(error);
      }
    });
  }

  private handleApiResponse(response: LoginResponseDto): void {
    if (!response) {
      this.errorMessage = 'پاسخ سرور نامعتبر است';
      return;
    }

    if (response.loginState === LoginState.Success && response.token) {
      this.handleSuccessfulLogin(response);
    } else {
      this.handleLoginState(response.loginState, response.message);
    }
  }

  private handleLoginState(loginState: LoginState, message?: string): void {
    switch (loginState) {
      case LoginState.InvalidCredentials:
        this.securityService.incrementFailedAttempts();
        this.errorMessage = 'نام کاربری یا رمز عبور اشتباه است';
        break;
      case LoginState.LockedOut:
        this.errorMessage = 'حساب کاربری شما مسدود شده است. لطفا با پشتیبانی تماس بگیرید';
        break;
      case LoginState.TemporaryLockedOut:
        this.errorMessage = message || 'حساب کاربری شما موقتا مسدود شده است. لطفا چند دقیقه دیگر تلاش کنید';
        break;
      case LoginState.RequiresTwoFactor:
        this.errorMessage = 'لطفا کد تأیید دو مرحله‌ای را وارد کنید';
        break;
      case LoginState.CaptchaRequired:
        this.errorMessage = 'لطفا کد امنیتی را وارد کنید';
        this.showCaptcha = true;
        this.loadCaptcha();
        break;
      case LoginState.CaptchaFailed:
        this.securityService.incrementFailedAttempts();
        this.errorMessage = 'کد امنیتی نادرست است';
        this.refreshCaptcha();
        break;
      default:
        this.errorMessage = message || 'خطای ناشناخته در ورود به سیستم';
        break;
    }
  }

  private handleSuccessfulLogin(response: LoginResponseDto): void {
    this.securityService.resetFailedAttempts();
    this.authService.setAuthData(response.token!, response.user!);
    this.successMessage = 'ورود موفقیت‌آمیز بود';
    this.showWelcomeMessage(response.user!);

    setTimeout(() => {
      this.redirectAfterLogin();
    }, 1500);
  }

  private showWelcomeMessage(user: UserDto): void {
    const welcomeName = user.fullName || user.userName || 'کاربر';
    console.log(`خوش آمدید ${welcomeName}`);
  }

  private redirectAfterLogin(): void {
    const intendedUrl = localStorage.getItem('intendedUrl');
    if (intendedUrl) {
      localStorage.removeItem('intendedUrl');
      this.router.navigateByUrl(intendedUrl);
    } else {
      this.router.navigate(['/']);
    }
  }

  private handleLoginError(error: any): void {
    console.error('Login error:', error);
    this.securityService.incrementFailedAttempts();

    if (error.status === 0) {
      this.errorMessage = 'اتصال به سرور برقرار نشد. لطفا اتصال اینترنت خود را بررسی کنید';
    } else if (error.status === 400) {
      if (error.error && error.error.loginState) {
        this.handleLoginState(error.error.loginState, error.error.message);
      } else {
        this.errorMessage = 'درخواست نامعتبر. لطفا اطلاعات ورود را بررسی کنید';
      }
    } else if (error.status === 401) {
      this.errorMessage = 'عدم احراز هویت. لطفا مجددا تلاش کنید';
    } else if (error.status >= 500) {
      this.errorMessage = 'خطای سرور. لطفا مجددا تلاش کنید';
    } else {
      this.errorMessage = 'خطای ناشناخته در ارتباط با سرور';
    }
  }

  private async getClientIp(): Promise<string> {
    // در محیط واقعی می‌توانید از سرویس‌های دریافت IP استفاده کنید
    // این یک پیاده‌سازی ساده است
    try {
      // می‌توانید از سرویس external استفاده کنید یا از سرور خود IP را بگیرید
      return 'unknown';
    } catch {
      return 'unknown';
    }
  }
}
