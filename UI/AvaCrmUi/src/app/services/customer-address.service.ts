import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GlobalResponse, PaginatedResult, PaginationRequest, ResponseResultGlobally} from '../models/base.model';
import {
  CustomerAddressCreateDto,
  CustomerAddressListDto,
  CustomerAddressUpdateDto
} from '../dtos/CustomerManagment/customer-address.models';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class CustomerAddressService {

  constructor(private http: HttpClient) { }

  getById(id: number): Observable<GlobalResponse<CustomerAddressListDto>> {
    return this.http.get<GlobalResponse<CustomerAddressListDto>>(
      `${ApiAddressUtility.customerAddressById}/${id}`
    );
  }

  getByCustomerId(customerId: number, request: PaginationRequest): Observable<GlobalResponse<PaginatedResult<CustomerAddressListDto>>> {
    let params = new HttpParams()
      .set('pageNumber', request.pageNumber.toString())
      .set('pageSize', request.pageSize.toString());

    if (request.searchTerm) {
      params = params.set('searchTerm', request.searchTerm);
    }

    return this.http.get<GlobalResponse<PaginatedResult<CustomerAddressListDto>>>(
      `${ApiAddressUtility.customerAddressByCustomerId}/${customerId}`,
      { params }
    );
  }

  create(createDto: CustomerAddressCreateDto): Observable<GlobalResponse<CustomerAddressListDto>> {
    return this.http.post<GlobalResponse<CustomerAddressListDto>>(
      `${ApiAddressUtility.createCustomerAddress}`,
      createDto
    );
  }

  update(updateDto: CustomerAddressUpdateDto): Observable<GlobalResponse<CustomerAddressListDto>> {
    return this.http.put<GlobalResponse<CustomerAddressListDto>>(
      `${ApiAddressUtility.updateCustomerAddress}`,
      updateDto
    );
  }

  delete(id: number): Observable<GlobalResponse<ResponseResultGlobally>> {
    return this.http.delete<GlobalResponse<ResponseResultGlobally>>(
      `${ApiAddressUtility.deleteCustomerAddress}/${id}`
    );
  }
}
