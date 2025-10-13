import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GlobalResponse, ResponseResultGlobally} from '../models/base.model';
import {TagDto, TagListDto} from '../dtos/Commons/tag.models';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class TagService {

  constructor(private http: HttpClient) {
  }

  getById(id: number): Observable<GlobalResponse<TagDto>> {
    return this.http.get<GlobalResponse<TagDto>>(
      `${ApiAddressUtility.tagById}/${id}`
    );
  }

  getAllTags(): Observable<TagListDto[]> {
    return this.http.get<TagListDto[]>(
      `${ApiAddressUtility.allTags}`
    );
  }

  create(title: string): Observable<GlobalResponse<TagListDto>> {
    // ارسال فقط رشته title به صورت ساده (بدون آبجکت JSON)
    console.log('📤 Sending plain string title:', title);

    return this.http.post<GlobalResponse<TagListDto>>(
      `${ApiAddressUtility.createTag}`,
      `"${title}"`, // ارسال به صورت رشته JSON
      {
        headers: {
          'Content-Type': 'application/json'
        }
      }
    );
  }

  update(updateDto: TagDto): Observable<GlobalResponse<TagListDto>> {
    return this.http.put<GlobalResponse<TagListDto>>(
      `${ApiAddressUtility.updateTag}`,
      updateDto
    );
  }

  delete(id: number): Observable<GlobalResponse<ResponseResultGlobally>> {
    return this.http.delete<GlobalResponse<ResponseResultGlobally>>(
      `${ApiAddressUtility.deleteTag}/${id}`
    );
  }

  searchTags(searchTerm: string): Observable<GlobalResponse<TagListDto[]>> {
    const params = new HttpParams().set('searchTerm', searchTerm);
    return this.http.get<GlobalResponse<TagListDto[]>>(
      `${ApiAddressUtility.allTags}`,
      {params}
    );
  }
}
