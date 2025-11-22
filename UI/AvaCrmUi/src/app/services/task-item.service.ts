import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Observable} from 'rxjs';
import {CreateTaskItemDto, UpdateTaskItemDto} from '../dtos/ProjectManagement/task-item.model';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class TaskItemService {

  constructor(private http: HttpClient) {}

  getAllTasks(projectId: number, pageSize: number, pageNumber: number): Observable<any> {
    const params = new HttpParams()
      .set('projectId', projectId.toString())
      .set('pageSize', pageSize.toString())
      .set('pageNumber', pageNumber.toString());

    return this.http.get(`${ApiAddressUtility.getAllTasks}`, { params });
  }

  getTask(taskItemId: number): Observable<any> {
    return this.http.get(`${ApiAddressUtility.getTask(taskItemId)}`);
  }

  createTask(createDto: CreateTaskItemDto): Observable<any> {
    return this.http.post(`${ApiAddressUtility.createTask}`, createDto);
  }

  updateTask(updateDto: UpdateTaskItemDto): Observable<any> {
    return this.http.put(`${ApiAddressUtility.updateTask}`, updateDto);
  }

  deleteTask(taskItemId: number): Observable<any> {
    return this.http.delete(`${ApiAddressUtility.deleteTask(taskItemId)}`);
  }
}
