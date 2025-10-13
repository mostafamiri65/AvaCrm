import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CountryDto, CreateCountryDto, UpdateCountryDto} from '../dtos/Commons/country.dto';
import {ApiAddressUtility} from '../utilities/api-address.utility';
import {CreateProvinceDto, ProvinceDto, UpdateProvinceDto} from '../dtos/Commons/province.dto';

@Injectable({
  providedIn: 'root'
})
export class ProvinceService {
  constructor(private http: HttpClient) { }
  getProvinces(countryId: number): Observable<ProvinceDto[]> {
    return this.http.get<ProvinceDto[]>(`${ApiAddressUtility.allProvinces}/${countryId}`);
  }

  getProvinceById(id: number): Observable<ProvinceDto> {
    return this.http.get<ProvinceDto>(`${ApiAddressUtility.ProvinceById}/${id}`);
  }

  createProvince(province: CreateProvinceDto): Observable<boolean> {
    return this.http.post<boolean>(`${ApiAddressUtility.createProvince}`, province);
  }

  updateProvince(province: UpdateProvinceDto): Observable<boolean> {
    return this.http.put<boolean>(`${ApiAddressUtility.updateProvince}`, province);
  }

  deleteProvince(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${ApiAddressUtility.deleteProvince}/${id}`);
  }
}
