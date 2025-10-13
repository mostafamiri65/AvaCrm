import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GlobalResponse, ResponseResultGlobally} from '../models/base.model';
import {
  IndividualCustomerCreateDto,
  IndividualCustomerListDto,
  IndividualCustomerUpdateDto
} from '../dtos/CustomerManagment/customer.dto';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class IndividualCustomerService {

  constructor(private http: HttpClient) { }

  // دریافت اطلاعات مشتری حقیقی بر اساس CustomerId
  getByCustomerId(customerId: number): Observable<GlobalResponse<IndividualCustomerListDto>> {
    return this.http.get<GlobalResponse<IndividualCustomerListDto>>(
      `${ApiAddressUtility.individualCustomerByCustomerId}/${customerId}`
    );
  }

  // ایجاد مشتری حقیقی
  create(createDto: IndividualCustomerCreateDto): Observable<GlobalResponse<IndividualCustomerListDto>> {
    return this.http.post<GlobalResponse<IndividualCustomerListDto>>(
      `${ApiAddressUtility.createIndividualCustomer}`,
      createDto
    );
  }

  // بروزرسانی مشتری حقیقی
  update(updateDto: IndividualCustomerUpdateDto): Observable<GlobalResponse<IndividualCustomerListDto>> {
    return this.http.put<GlobalResponse<IndividualCustomerListDto>>(
      `${ApiAddressUtility.updateIndividualCustomer}`,
      updateDto
    );
  }

  // حذف مشتری حقیقی
  delete(id: number): Observable<GlobalResponse<ResponseResultGlobally>> {
    return this.http.delete<GlobalResponse<ResponseResultGlobally>>(
      `${ApiAddressUtility.deleteIndividualCustomer}/${id}`
    );
  }
}
