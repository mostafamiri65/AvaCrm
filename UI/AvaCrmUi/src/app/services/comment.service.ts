import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GlobalResponse, PaginatedResult} from '../models/base.model';
import {CommentDto, CreateCommentDto, UpdateCommentDto} from '../dtos/ProjectManagement/comment.models';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private apiUrl = '/Comments';

  constructor(private http: HttpClient) {}

  getCommentsByTaskId(taskId: number, pageNumber: number = 1, pageSize: number = 10): Observable<GlobalResponse<PaginatedResult<CommentDto>>> {
    return this.http.get<GlobalResponse<PaginatedResult<CommentDto>>>(
      `${this.apiUrl}/task/${taskId}?pageNumber=${pageNumber}&pageSize=${pageSize}`
    );
  }

  getCommentById(id: number): Observable<GlobalResponse<CommentDto>> {
    return this.http.get<GlobalResponse<CommentDto>>(`${this.apiUrl}/GetComment/${id}`);
  }

  createComment(createDto: CreateCommentDto): Observable<GlobalResponse<any>> {
    return this.http.post<GlobalResponse<any>>(`${this.apiUrl}/CreateComment`, createDto);
  }

  updateComment(updateDto: UpdateCommentDto): Observable<GlobalResponse<any>> {
    return this.http.put<GlobalResponse<any>>(`${this.apiUrl}/UpdateComment`, updateDto);
  }

  deleteComment(id: number): Observable<GlobalResponse<any>> {
    return this.http.delete<GlobalResponse<any>>(`${this.apiUrl}/DeleteComment/${id}`);
  }

}
