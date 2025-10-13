export interface DashboardSummaryDto {
  totalCustomers: number;
  recentFollowUpsCount: number;
  recentInteractionsCount: number;
  upcomingFollowUpsCount: number;
  upcomingInteractionsCount: number;
}

export interface UpcomingActivityDto {
  id: number;
  customerId: number;
  customerName: string;
  customerCode: string;
  activityType: string;
  title: string;
  description: string;
  activityDate: Date;
  createdDate: Date;
}

export interface RecentActivityDto {
  id: number;
  customerId: number;
  customerName: string;
  customerCode: string;
  activityType: string;
  title: string;
  description: string;
  activityDate: Date;
}
