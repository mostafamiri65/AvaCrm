import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import { Observable } from 'rxjs';
import {GlobalResponse, PaginationRequest} from '../models/base.model';
import {CreateProjectDto, Project, ProjectStatus, UpdateProjectDto} from '../dtos/ProjectManagement/project.model';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class ProjectService {
  private httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    })
  };
  constructor(private http: HttpClient) { }

  getAll(request: PaginationRequest, status?: ProjectStatus): Observable<GlobalResponse<any>> {
    let params = new HttpParams()
      .set('pageSize', request.pageSize.toString())
      .set('pageNumber', request.pageNumber.toString());

    // اضافه کردن statusNumber اگر وجود دارد
    if (status !== undefined && status !== null) {
      params = params.set('statusNumber', status.toString());
    }

    // اضافه کردن searchTerm اگر وجود دارد
    if (request.searchTerm) {
      params = params.set('searchTerm', request.searchTerm);
    }

    const url = `${ApiAddressUtility.getAllProjects}`;
    console.log('API Call:', url);
    console.log('Params:', params.toString());

    return this.http.get<GlobalResponse<any>>(url, {
      params,
      headers: this.httpOptions.headers
    });
  }


  getById(projectId: number): Observable<GlobalResponse<Project>> {
    return this.http.get<GlobalResponse<Project>>(
      `${ApiAddressUtility.projectById(projectId)}`
    );
  }

  create(project: CreateProjectDto): Observable<GlobalResponse<Project>> {
    return this.http.post<GlobalResponse<Project>>(
      `${ApiAddressUtility.createProject}`,
      project
    );
  }

  update(project: UpdateProjectDto): Observable<GlobalResponse<Project>> {
    return this.http.put<GlobalResponse<Project>>(
      `${ApiAddressUtility.updateProject}`,
      project
    );
  }

  changeStatus(projectId: number, status: ProjectStatus): Observable<GlobalResponse<any>> {
    return this.http.put<GlobalResponse<any>>(
      `${ApiAddressUtility.changeProjectStatus(projectId)}`,
      null,
      {
        params: { status: status.toString() }
      }
    );
  }

  delete(projectId: number): Observable<GlobalResponse<any>> {
    return this.http.delete<GlobalResponse<any>>(
      `${ApiAddressUtility.deleteProject}`,
      {
        params: { projectId: projectId.toString() }
      }
    );
  }
}
