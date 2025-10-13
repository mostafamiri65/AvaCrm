import { Injectable } from '@angular/core';
import {GlobalResponse, PaginatedResult, PaginationRequest} from '../models/base.model';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CustomerTagCreateDto, CustomerTagListDto} from '../dtos/CustomerManagment/customer-tag.dto';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class CustomerTagService {

  constructor(private http: HttpClient) { }

  // دریافت تگ‌های یک مشتری
  getByCustomerId(
    customerId: number,
    request: PaginationRequest
  ): Observable<GlobalResponse<PaginatedResult<CustomerTagListDto>>> {

    let params = new HttpParams()
      .set('PageNumber', request.pageNumber.toString())
      .set('PageSize', request.pageSize.toString());

    if (request.searchTerm) {
      params = params.set('SearchTerm', request.searchTerm);
    }
    if (request.sortColumn) {
      params = params.set('SortColumn', request.sortColumn);
    }
    if (request.sortDirection) {
      params = params.set('SortDirection', request.sortDirection);
    }

    return this.http.get<GlobalResponse<PaginatedResult<CustomerTagListDto>>>(
      ApiAddressUtility.customerTagsByCustomer(customerId),
      { params }
    );
  }

  // افزودن تگ به مشتری
  addTagToCustomer(createDto: CustomerTagCreateDto): Observable<GlobalResponse<CustomerTagListDto>> {
    return this.http.post<GlobalResponse<CustomerTagListDto>>(
      ApiAddressUtility.addCustomerTag(),
      createDto
    );
  }

  // حذف تگ از مشتری
  removeTagFromCustomer(customerId: number, tagId: number): Observable<GlobalResponse<any>> {
    const params = new HttpParams()
      .set('customerId', customerId.toString())
      .set('tagId', tagId.toString());

    return this.http.delete<GlobalResponse<any>>(
      ApiAddressUtility.removeCustomerTag(),
      { params }
    );
  }
}
