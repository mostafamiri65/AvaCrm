import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GlobalResponse, ResponseResultGlobally} from '../models/base.model';
import {
  OrganizationCustomerCreateDto,
  OrganizationCustomerListDto,
  OrganizationCustomerUpdateDto
} from '../dtos/CustomerManagment/customer.dto';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class OrganizationCustomerService {
  constructor(private http: HttpClient) { }

  // دریافت اطلاعات مشتری حقوقی بر اساس CustomerId
  getByCustomerId(customerId: number): Observable<GlobalResponse<OrganizationCustomerListDto>> {
    return this.http.get<GlobalResponse<OrganizationCustomerListDto>>(
      `${ApiAddressUtility.organizationCustomerByCustomerId}/${customerId}`
    );
  }

  // ایجاد مشتری حقوقی
  create(createDto: OrganizationCustomerCreateDto): Observable<GlobalResponse<OrganizationCustomerListDto>> {
    return this.http.post<GlobalResponse<OrganizationCustomerListDto>>(
      `${ApiAddressUtility.createOrganizationCustomer}`,
      createDto
    );
  }

  // بروزرسانی مشتری حقوقی
  update(updateDto: OrganizationCustomerUpdateDto): Observable<GlobalResponse<OrganizationCustomerListDto>> {
    return this.http.put<GlobalResponse<OrganizationCustomerListDto>>(
      `${ApiAddressUtility.updateOrganizationCustomer}`,
      updateDto
    );
  }

  // حذف مشتری حقوقی
  delete(id: number): Observable<GlobalResponse<ResponseResultGlobally>> {
    return this.http.delete<GlobalResponse<ResponseResultGlobally>>(
      `${ApiAddressUtility.deleteOrganizationCustomer}/${id}`
    );
  }

}
