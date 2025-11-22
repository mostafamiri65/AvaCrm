// src/app/services/utility/date.service.ts
import { Injectable } from '@angular/core';
import { toJalaali, toGregorian, isValidJalaaliDate } from 'jalaali-js';

@Injectable({
  providedIn: 'root'
})
export class DateService {

  // تبدیل تاریخ میلادی به رشته شمسی (YYYY-MM-DD)
  gregorianToJalaliString(gregorianDate: string | Date): string {
    if (!gregorianDate) return '';

    const date = new Date(gregorianDate);
    const jalaali = toJalaali(
      date.getFullYear(),
      date.getMonth() + 1,
      date.getDate()
    );

    return `${jalaali.jy}-${jalaali.jm.toString().padStart(2, '0')}-${jalaali.jd.toString().padStart(2, '0')}`;
  }

  // تبدیل رشته شمسی (YYYY-MM-DD) به تاریخ میلادی
  jalaliStringToGregorian(jalaliDate: string): Date {
    if (!jalaliDate) return new Date();

    const parts = jalaliDate.split('-');
    if (parts.length !== 3) return new Date();

    const year = parseInt(parts[0]);
    const month = parseInt(parts[1]);
    const day = parseInt(parts[2]);

    // بررسی معتبر بودن تاریخ شمسی
    if (!isValidJalaaliDate(year, month, day)) {
      return new Date();
    }

    const gregorian = toGregorian(year, month, day);
    return new Date(gregorian.gy, gregorian.gm - 1, gregorian.gd);
  }

  // تبدیل تاریخ میلادی به رشته شمسی برای نمایش
  gregorianToJalaliDisplay(gregorianDate: string | Date): string {
    if (!gregorianDate) return '-';

    const date = new Date(gregorianDate);
    const jalaali = toJalaali(
      date.getFullYear(),
      date.getMonth() + 1,
      date.getDate()
    );

    return `${jalaali.jy}/${jalaali.jm.toString().padStart(2, '0')}/${jalaali.jd.toString().padStart(2, '0')}`;
  }

  // بررسی معتبر بودن رشته تاریخ شمسی
  isValidJalaliDateString(dateString: string): boolean {
    if (!dateString) return false;

    const parts = dateString.split('-');
    if (parts.length !== 3) return false;

    const year = parseInt(parts[0]);
    const month = parseInt(parts[1]);
    const day = parseInt(parts[2]);

    return isValidJalaaliDate(year, month, day);
  }
}
