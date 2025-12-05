import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {GlobalResponse, PaginatedResult, PaginationRequest, ResponseResultGlobally} from '../models/base.model';
import {Observable} from 'rxjs';
import {CreateCurrencyDto, CurrencyDto, UpdateCurrencyDto} from '../dtos/Commons/currency.dto';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {
  constructor(private http: HttpClient) { }

  // دریافت لیست ارزها با صفحه‌بندی
  getAllCurrencies(request: PaginationRequest): Observable<GlobalResponse<PaginatedResult<CurrencyDto>>> {
    let params = new HttpParams()
      .set('PageNumber', request.pageNumber.toString())
      .set('PageSize', request.pageSize.toString());

    return this.http.get<GlobalResponse<PaginatedResult<CurrencyDto>>>(ApiAddressUtility.allCurrencies, { params });
  }

  // دریافت ارز بر اساس شناسه
  getCurrencyById(id: number): Observable<GlobalResponse<CurrencyDto>> {
    return this.http.get<GlobalResponse<CurrencyDto>>(ApiAddressUtility.currencyById(id));
  }

  // دریافت ارز پیش‌فرض
  getDefaultCurrency(): Observable<GlobalResponse<CurrencyDto>> {
    return this.http.get<GlobalResponse<CurrencyDto>>(ApiAddressUtility.defaultCurrency);
  }

  // ایجاد ارز جدید
  createCurrency(createDto: CreateCurrencyDto): Observable<GlobalResponse<CurrencyDto>> {
    return this.http.post<GlobalResponse<CurrencyDto>>(ApiAddressUtility.createCurrency, createDto);
  }

  // به‌روزرسانی ارز
  updateCurrency(updateDto: UpdateCurrencyDto): Observable<GlobalResponse<CurrencyDto>> {
    return this.http.put<GlobalResponse<CurrencyDto>>(ApiAddressUtility.updateCurrency, updateDto);
  }

  // تغییر وضعیت پیش‌فرض بودن ارز
  changeDefaultCurrency(currencyId: number, isDefault: boolean = true): Observable<GlobalResponse<ResponseResultGlobally>> {
    let params = new HttpParams().set('isDefault', isDefault.toString());
    return this.http.patch<GlobalResponse<ResponseResultGlobally>>(
      ApiAddressUtility.changeDefaultCurrency(currencyId),
      {},
      { params }
    );
  }

  // حذف ارز
  deleteCurrency(id: number): Observable<GlobalResponse<ResponseResultGlobally>> {
    return this.http.delete<GlobalResponse<ResponseResultGlobally>>(ApiAddressUtility.deleteCurrency(id));
  }
}
