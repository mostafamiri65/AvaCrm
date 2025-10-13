export interface PaginationRequest {
  pageNumber: number;
  pageSize: number;
  searchTerm?: string;
  sortColumn?: string;
  sortDirection?: string;
}

export interface PaginatedResult<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
  totalPages: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
}

export interface GlobalResponse<T> {
  statusCode: number;
  message: string | null;
  data: T | null;
}

export interface ResponseResultGlobally {
  doneSuccessfully: boolean;
}
