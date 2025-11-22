// src/app/dtos/ProjectManagement/task-item.model.ts
export enum TaskPriority {
  Low = 0,
  Medium = 1,
  High = 2,
  Critical = 3
}

export enum TaskStatus {
  ToDo = 1,
  InProgress = 2,
  Done = 3,
  Cancelled = 4
}

export interface TaskItemDto {
  id: number;
  title: string;
  description?: string;
  projectId: number;
  assignedTo: number;
  priority: TaskPriority;
  status: TaskStatus;
  dueDate?: Date;
  creationDate: Date;
}

export interface CreateTaskItemDto {
  title: string;
  description?: string;
  projectId: number;
  assignedTo: number;
  priority: TaskPriority;
  status: TaskStatus;
  dueDate?: Date;
}

export interface UpdateTaskItemDto {
  id: number;
  title: string;
  description?: string;
  projectId: number;
  assignedTo: number;
  priority: TaskPriority;
  status: TaskStatus;
  dueDate?: Date;
}
