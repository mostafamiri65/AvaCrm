
export interface TagDto {
  id: number;
  title: string;
}

export interface TagListDto {
  id: number;
  title: string;
  customerCount: number;
}

// برای صفحه‌بندی
export interface TagPaginationRequest {
  pageNumber: number;
  pageSize: number;
  searchTerm?: string;
}
