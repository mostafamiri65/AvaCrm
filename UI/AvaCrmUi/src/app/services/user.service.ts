import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GlobalResponse, ResponseResultGlobally} from '../models/base.model';
import {
  UserChangePasswordDto,
  UserCreateDto,
  UserDetailDto,
  UserListDto,
  UserUpdateDto
} from '../dtos/accounts/user.models';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class UserService {


  constructor(private http: HttpClient) { }

  getAll(): Observable<GlobalResponse<UserListDto[]>> {
    return this.http.get<GlobalResponse<UserListDto[]>>(ApiAddressUtility.users);
  }

  getById(id: number): Observable<GlobalResponse<UserListDto>> {
    return this.http.get<GlobalResponse<UserListDto>>(
      `${ApiAddressUtility.userById}/${id}`
    );
  }

  getDetailById(id: number): Observable<GlobalResponse<UserDetailDto>> {
    return this.http.get<GlobalResponse<UserDetailDto>>(
      `${ApiAddressUtility.userById}/${id}/detail`
    );
  }

  create(createDto: UserCreateDto): Observable<GlobalResponse<UserListDto>> {
    return this.http.post<GlobalResponse<UserListDto>>(
      ApiAddressUtility.users,
      createDto
    );
  }

  update(updateDto: UserUpdateDto): Observable<GlobalResponse<UserListDto>> {
    return this.http.put<GlobalResponse<UserListDto>>(
      ApiAddressUtility.users,
      updateDto
    );
  }

  delete(id: number): Observable<GlobalResponse<ResponseResultGlobally>> {
    return this.http.delete<GlobalResponse<ResponseResultGlobally>>(
      `${ApiAddressUtility.userById}/${id}`
    );
  }

  changePassword(changePasswordDto: UserChangePasswordDto): Observable<GlobalResponse<ResponseResultGlobally>> {
    return this.http.post<GlobalResponse<ResponseResultGlobally>>(
      ApiAddressUtility.userChangePassword,
      changePasswordDto
    );
  }

  toggleLockout(id: number): Observable<GlobalResponse<ResponseResultGlobally>> {
    return this.http.post<GlobalResponse<ResponseResultGlobally>>(
      `${ApiAddressUtility.userToggleLockout}/${id}`,
      {}
    );
  }
}
