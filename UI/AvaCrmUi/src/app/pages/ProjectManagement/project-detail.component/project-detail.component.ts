// project-detail.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProjectService } from '../../../services/project.service';
import { Project, ProjectStatus } from '../../../dtos/ProjectManagement/project.model';
import {CdkDrag, CdkDragDrop, CdkDropList, moveItemInArray, transferArrayItem} from '@angular/cdk/drag-drop';
import { NgForOf, NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  TaskItemDto,
  TaskPriority,
  TaskStatus,
  UpdateTaskItemDto
} from '../../../dtos/ProjectManagement/task-item.model';
import {TaskItemService} from '../../../services/task-item.service';
import {DateService} from '../../../services/utility/date';
import {UserListDto} from '../../../dtos/accounts/user.models';
import {UserService} from '../../../services/user.service';
import {TaskFormComponent} from '../task-form.component/task-form.component';
import {AttachmentModalComponent} from '../attachment-modal.component/attachment-modal.component';

@Component({
  selector: 'app-project-detail',
  imports: [NgIf, NgForOf, FormsModule, CdkDropList, TaskFormComponent, CdkDrag, AttachmentModalComponent],
  templateUrl: './project-detail.component.html',
  styleUrl: './project-detail.component.css'
})
export class ProjectDetailComponent implements OnInit {
  project: Project | null = null;
  tasks: TaskItemDto[] = [];
  users: UserListDto[] = [];
  loading = true;
  tasksLoading = false;

  // Columns for Trello-like board
  todoTasks: TaskItemDto[] = [];
  inProgressTasks: TaskItemDto[] = [];
  doneTasks: TaskItemDto[] = [];
  cancelledTasks: TaskItemDto[] = [];

  // Modal states
  showCreateTaskModal = false;
  showEditTaskModal = false;
  selectedTask: TaskItemDto | null = null;
  connectedDropLists: string[] = ['todoList', 'inProgressList', 'doneList', 'cancelledList'];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private projectService: ProjectService,
    private taskItemService: TaskItemService,
    private userService: UserService,
    private dateService: DateService
  ) {}

  ngOnInit(): void {
    this.loadProject();
    this.loadUsers();
  }

  loadProject(): void {
    const projectId = this.route.snapshot.paramMap.get('id');
    if (!projectId) {
      this.router.navigate(['/projects']);
      return;
    }

    this.loading = true;
    this.projectService.getById(+projectId).subscribe({
      next: (response) => {
        if (response.statusCode === 200 && response.data) {
          this.project = response.data;
          this.loadTasks();
        } else {
          this.router.navigate(['/projects']);
        }
        this.loading = false;
      },
      error: () => {
        this.router.navigate(['/projects']);
        this.loading = false;
      }
    });
  }

  loadTasks(): void {
    if (!this.project) return;

    this.tasksLoading = true;
    this.taskItemService.getAllTasks(
      this.project.id,
      100, // Load more tasks for board view
      1
    ).subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          this.tasks = response.data.items || response.data;
          this.organizeTasksIntoColumns();
        }
        this.tasksLoading = false;
      },
      error: () => {
        this.tasksLoading = false;
      }
    });
  }

  organizeTasksIntoColumns(): void {
    this.todoTasks = this.tasks.filter(task => task.status === TaskStatus.ToDo);
    this.inProgressTasks = this.tasks.filter(task => task.status === TaskStatus.InProgress);
    this.doneTasks = this.tasks.filter(task => task.status === TaskStatus.Done);
    this.cancelledTasks = this.tasks.filter(task => task.status === TaskStatus.Cancelled);
  }

  loadUsers(): void {
    this.userService.getAll().subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          this.users = response.data || [];
        }
      }
    });
  }

  // Drag and Drop Handler
  onTaskDrop(event: CdkDragDrop<TaskItemDto[]>): void {
    console.log('Drop event:', event);

    if (event.previousContainer === event.container) {
      // حرکت در همان ستون
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      // حرکت به ستون دیگر
      const task = event.previousContainer.data[event.previousIndex];
      const newStatus = this.getStatusFromContainerId(event.container.id);

      console.log('Moving task to new status:', newStatus);

      // انتقال آیتم بین آرایه‌ها
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex
      );

      // آپدیت وضعیت تسک در backend
      this.updateTaskStatus(task, newStatus);
    }
  }
  getStatusFromContainerId(containerId: string): TaskStatus {
    switch (containerId) {
      case 'todoList': return TaskStatus.ToDo;
      case 'inProgressList': return TaskStatus.InProgress;
      case 'doneList': return TaskStatus.Done;
      case 'cancelledList': return TaskStatus.Cancelled;
      default: return TaskStatus.ToDo;
    }
  }

  updateTaskStatus(task: TaskItemDto, newStatus: TaskStatus): void {
    const updateDto: UpdateTaskItemDto = {
      id: task.id,
      title: task.title,
      description: task.description,
      projectId: task.projectId,
      assignedTo: task.assignedTo,
      priority: task.priority,
      status: newStatus,
      dueDate: task.dueDate
    };

    this.taskItemService.updateTask(updateDto).subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          console.log('Task status updated successfully');
        } else {
          console.error('Failed to update task status:', response);
          // اگر آپدیت شکست خورد، تسک را به ستون قبلی برگردانید
          this.loadTasks(); // بارگذاری مجدد برای همگام‌سازی
        }
      },
      error: (error) => {
        console.error('Error updating task status:', error);
        this.loadTasks(); // بارگذاری مجدد برای همگام‌سازی
      }
    });
  }
  getStatusText(status: ProjectStatus): string {
    switch (status) {
      case ProjectStatus.Planning:
        return 'در حال برنامه‌ریزی';
      case ProjectStatus.Active:
        return 'فعال';
      case ProjectStatus.Completed:
        return 'تکمیل شده';
      case ProjectStatus.Archived:
        return 'آرشیو شده';
      default:
        return 'نامشخص';
    }
  }

  getStatusClass(status: ProjectStatus): string {
    switch (status) {
      case ProjectStatus.Planning:
        return 'status-planning';
      case ProjectStatus.Active:
        return 'status-active';
      case ProjectStatus.Completed:
        return 'status-completed';
      case ProjectStatus.Archived:
        return 'status-archived';
      default:
        return 'status-unknown';
    }
  }

  getTaskPriorityText(priority: TaskPriority): string {
    switch (priority) {
      case TaskPriority.Low:
        return 'کم';
      case TaskPriority.Medium:
        return 'متوسط';
      case TaskPriority.High:
        return 'بالا';
      case TaskPriority.Critical:
        return 'بحرانی';
      default:
        return 'نامشخص';
    }
  }

  getTaskPriorityClass(priority: TaskPriority): string {
    switch (priority) {
      case TaskPriority.Low:
        return 'priority-low';
      case TaskPriority.Medium:
        return 'priority-medium';
      case TaskPriority.High:
        return 'priority-high';
      case TaskPriority.Critical:
        return 'priority-critical';
      default:
        return 'priority-unknown';
    }
  }

  getTaskStatusText(status: TaskStatus): string {
    switch (status) {
      case TaskStatus.ToDo:
        return 'برای انجام';
      case TaskStatus.InProgress:
        return 'در حال انجام';
      case TaskStatus.Done:
        return 'انجام شده';
      case TaskStatus.Cancelled:
        return 'لغو شده';
      default:
        return 'نامشخص';
    }
  }
// اضافه کردن properties
  showAttachmentModal = false;
  selectedTaskForAttachment: TaskItemDto | null = null;

// اضافه کردن متدها
  openAttachmentModal(task: TaskItemDto): void {
    this.selectedTaskForAttachment = task;
    this.showAttachmentModal = true;
  }

  closeAttachmentModal(): void {
    this.showAttachmentModal = false;
    this.selectedTaskForAttachment = null;
  }
  getUserName(userId: number): string {
    const user = this.users.find(u => u.id === userId);
    if (user) {
      return <string>user.username ;
    } else {
      return 'نامشخص';
    }
  }

  toPersianDate(date: string | Date): string {
    return this.dateService.gregorianToJalaliDisplay(date);
  }

  // Task Modal Methods
  openCreateTaskModal(): void {
    this.showCreateTaskModal = true;
  }

  closeCreateTaskModal(): void {
    this.showCreateTaskModal = false;
  }

  openEditTaskModal(task: TaskItemDto): void {
    this.selectedTask = task;
    this.showEditTaskModal = true;
  }

  closeEditTaskModal(): void {
    this.showEditTaskModal = false;
    this.selectedTask = null;
  }

  onTaskCreated(taskData: any): void {
    if (!this.project) return;

    const createDto = {
      title: taskData.title,
      description: taskData.description,
      projectId: this.project.id,
      assignedTo: taskData.assignedTo,
      priority: taskData.priority,
      status: TaskStatus.ToDo, // Always start in ToDo
      dueDate: taskData.dueDate ? this.dateService.jalaliStringToGregorian(taskData.dueDate) : undefined
    };

    this.taskItemService.createTask(createDto).subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          this.closeCreateTaskModal();
          this.loadTasks();
        }
      },
      error: (error) => {
        console.error('Error creating task:', error);
      }
    });
  }

  onTaskUpdated(taskData: any): void {
    if (!this.selectedTask) return;

    const updateDto = {
      id: this.selectedTask.id,
      title: taskData.title,
      description: taskData.description,
      projectId: this.project!.id,
      assignedTo: taskData.assignedTo,
      priority: taskData.priority,
      status: this.selectedTask.status, // Keep current status
      dueDate: taskData.dueDate ? this.dateService.jalaliStringToGregorian(taskData.dueDate) : undefined
    };

    this.taskItemService.updateTask(updateDto).subscribe({
      next: (response) => {
        if (response.statusCode === 200) {
          this.closeEditTaskModal();
          this.loadTasks();
        }
      },
      error: (error) => {
        console.error('Error updating task:', error);
      }
    });
  }

  deleteTask(task: TaskItemDto): void {
    if (confirm('آیا از حذف این تسک اطمینان دارید؟')) {
      this.taskItemService.deleteTask(task.id).subscribe({
        next: (response) => {
          if (response.statusCode === 200) {
            this.loadTasks();
          }
        },
        error: (error) => {
          console.error('Error deleting task:', error);
        }
      });
    }
  }

  goBack(): void {
    this.router.navigate(['/projects']);
  }

}
