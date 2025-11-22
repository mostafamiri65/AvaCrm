import { Component, HostListener, OnInit } from '@angular/core';
import {
  CreateProjectDto,
  Project,
  ProjectStatus,
  UpdateProjectDto
} from '../../../dtos/ProjectManagement/project.model';
import { PaginationRequest } from '../../../models/base.model';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProjectService } from '../../../services/project.service';
import { UserService } from '../../../services/user.service';
import { NgForOf, NgIf } from '@angular/common';
import { ProjectFormComponent } from '../project-form.component/project-form.component';
import {DateService} from '../../../services/utility/date';
import {Router} from '@angular/router';


@Component({
  selector: 'app-project-list',
  imports: [
    FormsModule,
    NgIf,
    NgForOf,
    ReactiveFormsModule,
    ProjectFormComponent
  ],
  templateUrl: './project-list.component.html',
  styleUrl: './project-list.component.css'
})
export class ProjectListComponent implements OnInit {
  projects: Project[] = [];
  loading = false;
  loadingMore = false;
  hasMore = true;

  paginationRequest: PaginationRequest = {
    pageNumber: 1,
    pageSize: 10,
    searchTerm: ''
  };

  // Modal states
  showCreateModal = false;
  showEditModal = false;
  showStatusModal = false;
  showDeleteModal = false;

  // Selected project for operations
  selectedProject: Project | null = null;

  // فقط statusForm را نگه می‌داریم
  statusForm: FormGroup;

  statusFilter: ProjectStatus | null = null;
  projectStatus = ProjectStatus;
  expandedDescriptions: Set<number> = new Set();

  constructor(
    private router: Router,
    private projectService: ProjectService,
    private userService: UserService,
    private dateService: DateService,
    private fb: FormBuilder
  ) {
    this.statusForm = this.createStatusForm();
  }

  ngOnInit(): void {
    this.loadProjects();
  }

  createStatusForm(): FormGroup {
    return this.fb.group({
      status: ['', Validators.required]
    });
  }

  loadProjects(): void {
    this.loading = true;
    this.projectService.getAll(this.paginationRequest, this.statusFilter || undefined).subscribe({
      next: (response) => {
        if (response.statusCode == 200) {
          this.projects = response.data.items || response.data;
          this.hasMore = (response.data.items || response.data).length === this.paginationRequest.pageSize;
        }
        this.loading = false;
      },
      error: () => {
        this.loading = false;
      }
    });
  }

  loadMore(): void {
    if (this.loadingMore || !this.hasMore) return;

    this.loadingMore = true;
    this.paginationRequest.pageNumber++;

    this.projectService.getAll(this.paginationRequest, this.statusFilter || undefined).subscribe({
      next: (response) => {
        if (response.statusCode == 200) {
          const newProjects = response.data.items || response.data;
          this.projects = [...this.projects, ...newProjects];
          this.hasMore = newProjects.length === this.paginationRequest.pageSize;
        }
        this.loadingMore = false;
      },
      error: () => {
        this.loadingMore = false;
      }
    });
  }

  @HostListener('window:scroll')
  onScroll(): void {
    const threshold = 100;
    const position = window.scrollY + window.innerHeight;
    const height = document.documentElement.scrollHeight;

    if (position >= height - threshold && !this.loadingMore && this.hasMore) {
      this.loadMore();
    }
  }

  toggleDescription(projectId: number): void {
    if (this.expandedDescriptions.has(projectId)) {
      this.expandedDescriptions.delete(projectId);
    } else {
      this.expandedDescriptions.add(projectId);
    }
  }

  isDescriptionExpanded(projectId: number): boolean {
    return this.expandedDescriptions.has(projectId);
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

  // تبدیل تاریخ میلادی به شمسی برای نمایش
  toPersianDate(date: string | Date): string {
    return this.dateService.gregorianToJalaliDisplay(date);
  }

  // Modal Methods
  openCreateModal(): void {
    this.showCreateModal = true;
  }

  closeCreateModal(): void {
    this.showCreateModal = false;
  }

  openEditModal(project: Project): void {
    this.selectedProject = project;
    this.showEditModal = true;
  }

  closeEditModal(): void {
    this.showEditModal = false;
    this.selectedProject = null;
  }

  openStatusModal(project: Project): void {
    this.selectedProject = project;
    this.statusForm.patchValue({
      status: project.status
    });
    this.showStatusModal = true;
  }

  closeStatusModal(): void {
    this.showStatusModal = false;
    this.selectedProject = null;
  }

  openDeleteModal(project: Project): void {
    this.selectedProject = project;
    this.showDeleteModal = true;
  }

  closeDeleteModal(): void {
    this.showDeleteModal = false;
    this.selectedProject = null;
  }

  // Form Submissions - دریافت داده از کامپوننت فرزند
  onCreateSubmit(formData: any): void {
    console.log('onCreateSubmit called with data:', formData);

    // تبدیل تاریخ شمسی به میلادی برای ارسال به سرور
    const startDateGregorian = this.dateService.jalaliStringToGregorian(formData.startDate);
    const endDateGregorian = formData.endDate ?
      this.dateService.jalaliStringToGregorian(formData.endDate) : undefined;

    const createDto: CreateProjectDto = {
      title: formData.title,
      description: formData.description,
      startDate: startDateGregorian,
      endDate: endDateGregorian,
      userIds: formData.userIds || []
    };

    console.log('Sending to API:', createDto);

    this.projectService.create(createDto).subscribe({
      next: (response) => {
        console.log('API Response:', response);
        if (response.statusCode == 200) {
          this.closeCreateModal();
          this.resetAndReload();
        } else {
          console.error('API returned non-200 status:', response);
        }
      },
      error: (error) => {
        console.error('API Error:', error);
      }
    });
  }

  onEditSubmit(formData: any): void {
    console.log('onEditSubmit called with data:', formData);

    if (!this.selectedProject) {
      console.error('No project selected for edit');
      return;
    }

    // تبدیل تاریخ شمسی به میلادی برای ارسال به سرور
    const startDateGregorian = this.dateService.jalaliStringToGregorian(formData.startDate);
    const endDateGregorian = formData.endDate ?
      this.dateService.jalaliStringToGregorian(formData.endDate) : undefined;

    const updateDto: UpdateProjectDto = {
      id: this.selectedProject.id,
      title: formData.title,
      description: formData.description,
      startDate: startDateGregorian,
      endDate: endDateGregorian,
      userIds: formData.userIds || []
    };

    this.projectService.update(updateDto).subscribe({
      next: (response) => {
        if (response.statusCode == 200) {
          this.closeEditModal();
          this.resetAndReload();
        }
      }
    });
  }

  onStatusSubmit(): void {
    if (this.statusForm.invalid || !this.selectedProject) return;

    const newStatus = this.statusForm.value.status;
    this.projectService.changeStatus(this.selectedProject.id, newStatus).subscribe({
      next: (response) => {
        if (response.statusCode == 200) {
          this.closeStatusModal();
          this.resetAndReload();
        }
      }
    });
  }

  onDeleteConfirm(): void {
    if (!this.selectedProject) return;

    this.projectService.delete(this.selectedProject.id).subscribe({
      next: (response) => {
        if (response.statusCode == 200) {
          this.closeDeleteModal();
          this.resetAndReload();
        }
      }
    });
  }

  private resetAndReload(): void {
    this.paginationRequest.pageNumber = 1;
    this.loadProjects();
  }

  onSearch(searchTerm: string): void {
    this.paginationRequest.searchTerm = searchTerm;
    this.paginationRequest.pageNumber = 1;
    this.loadProjects();
  }

  onStatusFilterChange(status: ProjectStatus | null): void {
    this.statusFilter = status;
    this.paginationRequest.pageNumber = 1;
    this.loadProjects();
  }

  navigateToProject(projectId: number): void {
    this.router.navigate(['/projects', projectId]);
  }

  onProjectAction(projectId: number, action: string): void {
    if (action === 'view') {
      this.navigateToProject(projectId);
    }
  }
}
