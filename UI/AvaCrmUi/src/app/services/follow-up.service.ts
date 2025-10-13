import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GlobalResponse, PaginatedResult, PaginationRequest, ResponseResultGlobally} from '../models/base.model';
import {FollowUpCreateDto, FollowUpListDto, FollowUpUpdateDto} from '../dtos/CustomerManagment/follow-up.models';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class FollowUpService {


  constructor(private http: HttpClient) { }

  getById(id: number): Observable<GlobalResponse<FollowUpListDto>> {
    return this.http.get<GlobalResponse<FollowUpListDto>>(
      `${ApiAddressUtility.followUpById}/${id}`
    );
  }

  getByCustomerId(customerId: number, request: PaginationRequest): Observable<GlobalResponse<PaginatedResult<FollowUpListDto>>> {
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

    return this.http.get<GlobalResponse<PaginatedResult<FollowUpListDto>>>(
      `${ApiAddressUtility.followUpsByCustomer}/${customerId}`,
      { params }
    );
  }

  getUpcomingFollowUps(request: PaginationRequest): Observable<GlobalResponse<PaginatedResult<FollowUpListDto>>> {
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

    return this.http.get<GlobalResponse<PaginatedResult<FollowUpListDto>>>(
      ApiAddressUtility.upcomingFollowUps,
      { params }
    );
  }

  create(createDto: FollowUpCreateDto): Observable<GlobalResponse<FollowUpListDto>> {
    return this.http.post<GlobalResponse<FollowUpListDto>>(
      ApiAddressUtility.followUps,
      createDto
    );
  }

  update(updateDto: FollowUpUpdateDto): Observable<GlobalResponse<FollowUpListDto>> {
    return this.http.put<GlobalResponse<FollowUpListDto>>(
      ApiAddressUtility.followUps,
      updateDto
    );
  }

  delete(id: number): Observable<GlobalResponse<ResponseResultGlobally>> {
    return this.http.delete<GlobalResponse<ResponseResultGlobally>>(
      `${ApiAddressUtility.followUpById}/${id}`
    );
  }
}
