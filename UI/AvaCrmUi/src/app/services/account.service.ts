import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {LoginDto} from '../dtos/accounts/login.dto';
import {ApiAddressUtility} from '../utilities/api-address.utility';
import {LoginResponseDto} from '../dtos/receivedResponse.dto';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  constructor(private httpClient : HttpClient) {  }
  login(loginDto : LoginDto){
    return this.httpClient.post<LoginResponseDto>(ApiAddressUtility.login, loginDto );
  }
}
