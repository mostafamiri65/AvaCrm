import {Component, OnInit} from '@angular/core';
import {DashboardService} from '../../services/dashboard.service';
import {DashboardSummaryDto, RecentActivityDto, UpcomingActivityDto} from '../../dtos/Commons/dashboard.models';
import {RouterLink} from '@angular/router';
import {DecimalPipe, NgForOf, NgIf} from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [
    RouterLink,
    DecimalPipe,
    NgIf,
    NgForOf
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  summary!: DashboardSummaryDto;
  upcomingActivities: UpcomingActivityDto[] = [];
  recentActivities: RecentActivityDto[] = [];

  loading = {
    summary: true,
    upcoming: true,
    recent: true
  };

  constructor(private dashboardService: DashboardService) {}

  ngOnInit(): void {
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.loadDashboardSummary();
    this.loadUpcomingActivities();
    this.loadRecentActivities();
  }

  loadDashboardSummary(): void {
    this.loading.summary = true;
    this.dashboardService.getDashboardSummary().subscribe({
      next: (response) => {
        if (response.data) {
          this.summary = response.data;
        }
        this.loading.summary = false;
      },
      error: () => {
        this.loading.summary = false;
      }
    });
  }

  loadUpcomingActivities(): void {
    this.loading.upcoming = true;
    this.dashboardService.getUpcomingActivities(10).subscribe({
      next: (response) => {
        if (response.data) {
          this.upcomingActivities = response.data;
        }
        this.loading.upcoming = false;
      },
      error: () => {
        this.loading.upcoming = false;
      }
    });
  }

  loadRecentActivities(): void {
    this.loading.recent = true;
    this.dashboardService.getRecentActivities(7).subscribe({
      next: (response) => {
        if (response.data) {
          this.recentActivities = response.data;
        }
        this.loading.recent = false;
      },
      error: () => {
        this.loading.recent = false;
      }
    });
  }

  getActivityIcon(activityType: string): string {
    switch (activityType) {
      case 'FollowUp':
        return 'fas fa-clipboard-list';
      case 'Interaction':
        return 'fas fa-handshake';
      default:
        return 'fas fa-circle';
    }
  }

  getActivityBadgeClass(activityType: string): string {
    switch (activityType) {
      case 'FollowUp':
        return 'badge bg-warning';
      case 'Interaction':
        return 'badge bg-info';
      default:
        return 'badge bg-secondary';
    }
  }

  getActivityTypeText(activityType: string): string {
    switch (activityType) {
      case 'FollowUp':
        return 'پیگیری';
      case 'Interaction':
        return 'تعامل';
      default:
        return activityType;
    }
  }

  formatDate(date: Date): string {
    return new Date(date).toLocaleDateString('fa-IR');
  }

  formatDateTime(date: Date): string {
    return new Date(date).toLocaleString('fa-IR');
  }

  isToday(date: Date): boolean {
    const today = new Date();
    const activityDate = new Date(date);
    return today.toDateString() === activityDate.toDateString();
  }

  isTomorrow(date: Date): boolean {
    const tomorrow = new Date();
    tomorrow.setDate(tomorrow.getDate() + 1);
    const activityDate = new Date(date);
    return tomorrow.toDateString() === activityDate.toDateString();
  }

  getDateStatus(date: Date): string {
    if (this.isToday(date)) return 'امروز';
    if (this.isTomorrow(date)) return 'فردا';
    return this.formatDate(date);
  }

  refreshData(): void {
    this.loadDashboardData();
  }
}
