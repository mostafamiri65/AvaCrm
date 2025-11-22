export interface Project {
  id: number;
  title: string;
  description: string;
  status: ProjectStatus;
  startDate: Date;
  endDate?: Date;
  createdBy: number;
  createdAt: Date;
  updatedAt: Date;
  userIds: number[];

}

export enum ProjectStatus {
  Planning = 1,
  Active = 2,
  Completed = 3,
  Archived = 4
}

export interface CreateProjectDto {
  title: string;
  description: string;
  startDate: Date;
  endDate?: Date;
  userIds: number[];
}

export interface UpdateProjectDto {
  id: number;
  title: string;
  description: string;
  startDate: Date;
  endDate?: Date;
  userIds: number[];
}

