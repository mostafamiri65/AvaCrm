import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {GlobalResponse, PaginatedResult, PaginationRequest, ResponseResultGlobally} from '../models/base.model';
import { Observable } from "rxjs";
import {CreateUnitDto, UnitCategory, UnitDto, UpdateUnitDto} from '../dtos/Commons/unit.dto';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class UnitService {
  constructor(private http: HttpClient) { }

  // دریافت لیست واحدها با صفحه‌بندی
  getAllUnits(request: PaginationRequest): Observable<GlobalResponse<PaginatedResult<UnitDto>>> {
    let params = new HttpParams()
      .set('PageNumber', request.pageNumber.toString())
      .set('PageSize', request.pageSize.toString());

    return this.http.get<GlobalResponse<PaginatedResult<UnitDto>>>(ApiAddressUtility.allUnits, { params });
  }

  // دریافت واحد بر اساس شناسه
  getUnitById(id: number): Observable<GlobalResponse<UnitDto>> {
    return this.http.get<GlobalResponse<UnitDto>>(ApiAddressUtility.unitById(id));
  }

  // دریافت واحدها بر اساس دسته‌بندی
  getUnitsByCategory(category: UnitCategory): Observable<GlobalResponse<UnitDto[]>> {
    return this.http.get<GlobalResponse<UnitDto[]>>(ApiAddressUtility.unitsByCategory(category));
  }

  // ایجاد واحد جدید
  createUnit(createDto: CreateUnitDto): Observable<GlobalResponse<UnitDto>> {
    return this.http.post<GlobalResponse<UnitDto>>(ApiAddressUtility.createUnit, createDto);
  }

  // به‌روزرسانی واحد
  updateUnit(updateDto: UpdateUnitDto): Observable<GlobalResponse<UnitDto>> {
    return this.http.put<GlobalResponse<UnitDto>>(ApiAddressUtility.updateUnit, updateDto);
  }

  // حذف واحد
  deleteUnit(id: number): Observable<GlobalResponse<ResponseResultGlobally>> {
    return this.http.delete<GlobalResponse<ResponseResultGlobally>>(ApiAddressUtility.deleteUnit(id));
  }
}
