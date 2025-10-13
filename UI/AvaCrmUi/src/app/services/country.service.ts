import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ApiAddressUtility} from '../utilities/api-address.utility';
import {CountryDto, CreateCountryDto, UpdateCountryDto} from '../dtos/Commons/country.dto';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CountryService {
  constructor(private http: HttpClient) { }
  getCountries(): Observable<CountryDto[]> {
    return this.http.get<CountryDto[]>(`${ApiAddressUtility.allCountries}`);
  }

  getCountryById(id: number): Observable<CountryDto> {
    return this.http.get<CountryDto>(`${ApiAddressUtility.countryById}/${id}`);
  }

  createCountry(country: CreateCountryDto): Observable<boolean> {
    return this.http.post<boolean>(`${ApiAddressUtility.createCountry}`, country);
  }

  updateCountry(country: UpdateCountryDto): Observable<boolean> {
    return this.http.put<boolean>(`${ApiAddressUtility.updateCountry}`, country);
  }

  deleteCountry(id: number): Observable<boolean> {
    return this.http.delete<boolean>(`${ApiAddressUtility.deleteCountry}/${id}`);
  }
}
