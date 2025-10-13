import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GlobalResponse, PaginatedResult, PaginationRequest, ResponseResultGlobally} from '../models/base.model';
import {
  ContactPersonCreateDto,
  ContactPersonListDto,
  ContactPersonUpdateDto
} from '../dtos/CustomerManagment/contact-person.models';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class ContactPersonService {

  constructor(private http: HttpClient) { }

  getById(id: number): Observable<GlobalResponse<ContactPersonListDto>> {
    return this.http.get<GlobalResponse<ContactPersonListDto>>(
      `${ApiAddressUtility.contactPersonById}/${id}`
    );
  }

  getByCustomerId(customerId: number, request: PaginationRequest): Observable<GlobalResponse<PaginatedResult<ContactPersonListDto>>> {
    let params = new HttpParams()
      .set('pageNumber', request.pageNumber.toString())
      .set('pageSize', request.pageSize.toString());

    if (request.searchTerm) {
      params = params.set('searchTerm', request.searchTerm);
    }

    return this.http.get<GlobalResponse<PaginatedResult<ContactPersonListDto>>>(
      `${ApiAddressUtility.contactPersonByCustomerId}/${customerId}`,
      { params }
    );
  }

  create(createDto: ContactPersonCreateDto): Observable<GlobalResponse<ContactPersonListDto>> {
    return this.http.post<GlobalResponse<ContactPersonListDto>>(
      `${ApiAddressUtility.createContactPerson}`,
      createDto
    );
  }

  update(updateDto: ContactPersonUpdateDto): Observable<GlobalResponse<ContactPersonListDto>> {
    return this.http.put<GlobalResponse<ContactPersonListDto>>(
      `${ApiAddressUtility.updateContactPerson}`,
      updateDto
    );
  }

  delete(id: number): Observable<GlobalResponse<ResponseResultGlobally>> {
    return this.http.delete<GlobalResponse<ResponseResultGlobally>>(
      `${ApiAddressUtility.deleteContactPerson}/${id}`
    );
  }

}
