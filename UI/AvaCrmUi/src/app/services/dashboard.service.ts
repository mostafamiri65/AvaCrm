import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {GlobalResponse} from '../models/base.model';
import {DashboardSummaryDto, RecentActivityDto, UpcomingActivityDto} from '../dtos/Commons/dashboard.models';
import {ApiAddressUtility} from '../utilities/api-address.utility';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private http: HttpClient) { }

  getDashboardSummary(): Observable<GlobalResponse<DashboardSummaryDto>> {
    return this.http.get<GlobalResponse<DashboardSummaryDto>>(
      ApiAddressUtility.dashboardSummary
    );
  }

  getUpcomingActivities(count: number = 10): Observable<GlobalResponse<UpcomingActivityDto[]>> {
    return this.http.get<GlobalResponse<UpcomingActivityDto[]>>(
      `${ApiAddressUtility.upcomingActivities}?count=${count}`
    );
  }

  getRecentActivities(days: number = 7): Observable<GlobalResponse<RecentActivityDto[]>> {
    return this.http.get<GlobalResponse<RecentActivityDto[]>>(
      `${ApiAddressUtility.recentActivities}?days=${days}`
    );
  }
}
