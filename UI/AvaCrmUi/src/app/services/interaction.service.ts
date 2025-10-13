import { Injectable } from '@angular/core';
import {catchError, Observable, throwError} from 'rxjs';
import {GlobalResponse, PaginatedResult, PaginationRequest} from '../models/base.model';
import {ApiAddressUtility} from '../utilities/api-address.utility';
import {
  InteractionCreateDto,
  InteractionListDto,
  InteractionUpdateDto
} from '../dtos/CustomerManagment/interaction.models';
import {HttpClient, HttpErrorResponse, HttpParams} from '@angular/common/http';
import {InteractionType} from '../dtos/CustomerManagment/customer.enum';

@Injectable({
  providedIn: 'root'
})
export class InteractionService {


  constructor(private http: HttpClient) { }

  // دریافت تعامل بر اساس ID
  getById(id: number): Observable<GlobalResponse<InteractionListDto>> {
    return this.http.get<GlobalResponse<InteractionListDto>>(
      ApiAddressUtility.interactionById(id)
    );
  }

  // دریافت تعامل‌های یک مشتری
  getByCustomerId(
    customerId: number,
    request: PaginationRequest
  ): Observable<GlobalResponse<PaginatedResult<InteractionListDto>>> {

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

    return this.http.get<GlobalResponse<PaginatedResult<InteractionListDto>>>(
      ApiAddressUtility.interactionsByCustomer(customerId),
      { params }
    );
  }

  // دریافت تعامل‌های بر اساس نوع
  getByType(
    interactionType: InteractionType,
    request: PaginationRequest
  ): Observable<GlobalResponse<PaginatedResult<InteractionListDto>>> {

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

    return this.http.get<GlobalResponse<PaginatedResult<InteractionListDto>>>(
      ApiAddressUtility.interactionsByType(interactionType),
      { params }
    );
  }

  create(createDto: InteractionCreateDto): Observable<GlobalResponse<InteractionListDto>> {
    console.log('Creating interaction with data:', createDto);

    return this.http.post<GlobalResponse<InteractionListDto>>(
      ApiAddressUtility.createInteraction(),
      createDto
    ).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error('HTTP Error in create:', error);
        console.error('Error response:', error.error);
        return throwError(() => error);
      })
    );
  }

  // بروزرسانی تعامل - با هندلینگ خطا
  update(updateDto: InteractionUpdateDto): Observable<GlobalResponse<InteractionListDto>> {
    console.log('Updating interaction with data:', updateDto);

    return this.http.put<GlobalResponse<InteractionListDto>>(
      ApiAddressUtility.updateInteraction(),
      updateDto
    ).pipe(
      catchError((error: HttpErrorResponse) => {
        console.error('HTTP Error in update:', error);
        console.error('Error response:', error.error);
        return throwError(() => error);
      })
    );
  }


  // حذف تعامل
  delete(id: number): Observable<GlobalResponse<any>> {
    return this.http.delete<GlobalResponse<any>>(
      ApiAddressUtility.deleteInteraction(id)
    );
  }
}
