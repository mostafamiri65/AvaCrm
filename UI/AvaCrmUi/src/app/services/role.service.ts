import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GlobalResponse, ResponseResultGlobally} from '../models/base.model';
import {RoleCreateDto, RoleListDto, RoleUpdateDto} from '../dtos/accounts/role.models';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<GlobalResponse<RoleListDto[]>> {
    return this.http.get<GlobalResponse<RoleListDto[]>>(ApiAddressUtility.roles);
  }

  getById(id: number): Observable<GlobalResponse<RoleListDto>> {
    return this.http.get<GlobalResponse<RoleListDto>>(
      `${ApiAddressUtility.roleById}/${id}`
    );
  }

  create(createDto: RoleCreateDto): Observable<GlobalResponse<RoleListDto>> {
    return this.http.post<GlobalResponse<RoleListDto>>(
      ApiAddressUtility.roles,
      createDto
    );
  }

  update(updateDto: RoleUpdateDto): Observable<GlobalResponse<RoleListDto>> {
    return this.http.put<GlobalResponse<RoleListDto>>(
      ApiAddressUtility.roles,
      updateDto
    );
  }

  delete(id: number): Observable<GlobalResponse<ResponseResultGlobally>> {
    return this.http.delete<GlobalResponse<ResponseResultGlobally>>(
      `${ApiAddressUtility.roleById}/${id}`
    );
  }
}
