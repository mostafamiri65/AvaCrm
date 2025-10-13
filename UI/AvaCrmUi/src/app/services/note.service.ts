import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GlobalResponse, PaginatedResult, PaginationRequest} from '../models/base.model';
import {NoteCreateDto, NoteListDto, NoteUpdateDto} from '../dtos/CustomerManagment/note.models';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class NoteService {


  constructor(private http: HttpClient) { }

  // دریافت یادداشت بر اساس ID
  getById(id: number): Observable<GlobalResponse<NoteListDto>> {
    return this.http.get<GlobalResponse<NoteListDto>>(
      ApiAddressUtility.noteById(id)
    );
  }

  // دریافت یادداشت‌های یک مشتری
  getByCustomerId(
    customerId: number,
    request: PaginationRequest
  ): Observable<GlobalResponse<PaginatedResult<NoteListDto>>> {

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

    return this.http.get<GlobalResponse<PaginatedResult<NoteListDto>>>(
      ApiAddressUtility.notesByCustomer(customerId),
      { params }
    );
  }

  // ایجاد یادداشت جدید
  create(createDto: NoteCreateDto): Observable<GlobalResponse<NoteListDto>> {
    return this.http.post<GlobalResponse<NoteListDto>>(
      ApiAddressUtility.createNote(),
      createDto
    );
  }

  // بروزرسانی یادداشت
  update(updateDto: NoteUpdateDto): Observable<GlobalResponse<NoteListDto>> {
    return this.http.put<GlobalResponse<NoteListDto>>(
      ApiAddressUtility.updateNote(),
      updateDto
    );
  }

  // حذف یادداشت
  delete(id: number): Observable<GlobalResponse<any>> {
    return this.http.delete<GlobalResponse<any>>(
      ApiAddressUtility.deleteNote(id)
    );
  }
}
