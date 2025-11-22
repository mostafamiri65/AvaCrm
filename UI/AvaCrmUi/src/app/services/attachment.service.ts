import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class AttachmentService {

  constructor(private http: HttpClient) {}

  getAttachments(taskItemId: number): Observable<any> {
    return this.http.get(`${ApiAddressUtility.getAttachments(taskItemId)}`);
  }

  uploadFile(file: File, taskId: number, downloadedFileName: string): Observable<any> {
    const formData = new FormData();
    formData.append('file', file, file.name);

    const params = new HttpParams()
      .set('taskId', taskId.toString())
      .set('downloadedFileName', downloadedFileName);

    return this.http.post(`${ApiAddressUtility.uploadFile}`, formData, {
      params: params
    });
  }
  deleteAttachment(attachmentId: number): Observable<any> {
    return this.http.delete(`${ApiAddressUtility.deleteAttachment(attachmentId)}`);
  }
}
