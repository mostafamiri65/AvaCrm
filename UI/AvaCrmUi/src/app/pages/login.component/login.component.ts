import {Component} from '@angular/core';
import {Router} from '@angular/router';
import {AccountService} from '../../services/account.service';
import {AuthService} from '../../services/auth.service';
import {LoginResponseDto} from '../../dtos/receivedResponse.dto';
import {LoginState} from '../../dtos/accounts/user.enum';
import {LoginDto} from '../../dtos/accounts/login.dto';
import {UserDto} from '../../dtos/accounts/user.dto';
import {FormsModule} from '@angular/forms';
import {NgIf} from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [
    FormsModule,
    NgIf
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  model: LoginDto = {
    username: '',
    password: ''
  };

  isLoading = false;
  errorMessage = '';
  successMessage: string = '';

  constructor(
    private accountService: AccountService,
    private router: Router,
    private authService: AuthService,
  ) {
  }
  loginSubmit(): void {
    // Basic validation
    if (!this.model.username || !this.model.password) {
      this.errorMessage = 'نام کاربری و کلمه عبور را وارد نمایید';
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';
    this.successMessage = '';

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
    // بررسی اینکه response وجود دارد
    if (!response) {
      this.errorMessage = 'پاسخ سرور نامعتبر است';
      return;
    }

    // از آنجایی که API شما هم در success و هم در error response برمی‌گرداند
    // باید بر اساس loginState تصمیم بگیریم
    if (response.loginState === LoginState.Success && response.token) {
      this.handleSuccessfulLogin(response);
    } else {
      // اینجا response از BadRequest می‌آید اما داده معتبر دارد
      this.handleLoginState(response.loginState);
    }
  }

  private handleSuccessfulLogin(response: LoginResponseDto): void {
    // ذخیره توکن و اطلاعات کاربر
    this.authService.setAuthData(response.token, response.user);

    // نمایش پیام موفقیت
    this.successMessage = 'ورود موفقیت‌آمیز بود';

    // نمایش پیام خوش‌آمدگویی
    this.showWelcomeMessage(response.user);

    // انتقال بعد از تأخیر کوتاه برای نمایش پیام موفقیت
    setTimeout(() => {
      this.redirectAfterLogin();
    }, 1500);
  }

  private handleLoginState(loginState: LoginState): void {
    switch (loginState) {
      case LoginState.InvalidCredentials:
        this.errorMessage = 'نام کاربری یا رمز عبور اشتباه است';
        break;
      case LoginState.LockedOut:
        this.errorMessage = 'حساب کاربری شما مسدود شده است. لطفا با پشتیبانی تماس بگیرید';
        break;
      case LoginState.TemporaryLockedOut:
        this.errorMessage = 'حساب کاربری شما موقتا مسدود شده است. لطفا چند دقیقه دیگر تلاش کنید';
        break;
      case LoginState.RequiresTwoFactor:
        this.errorMessage = 'لطفا کد تأیید دو مرحله‌ای را وارد کنید';
        break;

      default:
        this.errorMessage = 'خطای ناشناخته در ورود به سیستم';
        break;
    }
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
      const user = this.authService.getCurrentUser();

        this.router.navigate(['/']);

    }
  }

  private handleLoginError(error: any): void {
    console.error('Login error:', error);

    if (error.status === 0) {
      this.errorMessage = 'اتصال به سرور برقرار نشد. لطفا اتصال اینترنت خود را بررسی کنید';
    } else if (error.status === 400) {
      // در این حالت ممکن است response معتبری داشته باشیم که در handleApiResponse بررسی شده
      if (error.error && error.error.loginState) {
        this.handleLoginState(error.error.loginState);
      } else {
        this.errorMessage = 'درخواست نامعتبر. لطفا اطلاعات ورود را بررسی کنید';
      }
    } else if (error.status === 401) {
      this.errorMessage = 'عدم احراز هویت. لطفا مجددا تلاش کنید';
    } else if (error.status === 403) {
      this.errorMessage = 'دسترسی غیرمجاز';
    } else if (error.status === 404) {
      this.errorMessage = 'سرویس ورود یافت نشد';
    } else if (error.status >= 500) {
      this.errorMessage = 'خطای سرور. لطفا稍后 مجددا تلاش کنید';
    } else {
      this.errorMessage = 'خطای ناشناخته در ارتباط با سرور';
    }
  }

}
