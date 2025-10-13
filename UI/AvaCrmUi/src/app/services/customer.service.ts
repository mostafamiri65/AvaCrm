import { Injectable } from '@angular/core';
import {GlobalResponse, PaginatedResult, PaginationRequest, ResponseResultGlobally} from '../models/base.model';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {
  CustomerCreateDto,
  CustomerDetailDto,
  CustomerListDto,
  CustomerUpdateDto
} from '../dtos/CustomerManagment/customer.dto';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class CustomerService {

  constructor(private http: HttpClient) { }

  // دریافت لیست مشتریان با صفحه‌بندی
  getAllCustomers(request: PaginationRequest): Observable<GlobalResponse<PaginatedResult<CustomerListDto>>> {
    let params = new HttpParams()
      .set('pageNumber', request.pageNumber.toString())
      .set('pageSize', request.pageSize.toString());

    if (request.searchTerm) {
      params = params.set('searchTerm', request.searchTerm);
    }
    if (request.sortColumn) {
      params = params.set('sortColumn', request.sortColumn);
    }
    if (request.sortDirection) {
      params = params.set('sortDirection', request.sortDirection);
    }

    return this.http.get<GlobalResponse<PaginatedResult<CustomerListDto>>>(
      `${ApiAddressUtility.allCustomers}`,
      { params }
    );
  }

  // دریافت مشتری بر اساس ID
  getCustomerById(id: number): Observable<GlobalResponse<CustomerDetailDto>> {
    return this.http.get<GlobalResponse<CustomerDetailDto>>(
      `${ApiAddressUtility.customerById}/${id}`
    );
  }

  // ایجاد مشتری جدید
  createCustomer(createDto: CustomerCreateDto): Observable<GlobalResponse<CustomerListDto>> {
    return this.http.post<GlobalResponse<CustomerListDto>>(
      `${ApiAddressUtility.createCustomer}`,
      createDto
    );
  }

  // بروزرسانی مشتری
  updateCustomer(updateDto: CustomerUpdateDto): Observable<GlobalResponse<CustomerListDto>> {
    return this.http.put<GlobalResponse<CustomerListDto>>(
      `${ApiAddressUtility.updateCustomer}`,
      updateDto
    );
  }

  // حذف مشتری
  deleteCustomer(id: number): Observable<GlobalResponse<ResponseResultGlobally>> {
    return this.http.delete<GlobalResponse<ResponseResultGlobally>>(
      `${ApiAddressUtility.deleteCustomer}/${id}`
    );
  }

  // بررسی یکتا بودن کد مشتری
  checkCodeUnique(code: string, excludeCustomerId?: number): Observable<GlobalResponse<boolean>> {
    let params = new HttpParams().set('code', code);
    if (excludeCustomerId) {
      params = params.set('excludeCustomerId', excludeCustomerId.toString());
    }

    return this.http.get<GlobalResponse<boolean>>(
      `${ApiAddressUtility.checkCodeUnique}`,
      { params }
    );
  }
}
