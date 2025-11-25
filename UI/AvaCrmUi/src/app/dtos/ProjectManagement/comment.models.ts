export interface CommentDto {
  id: number;
  creationDate: Date;
  createdBy: number;
  taskId: number;
  content?: string;
}

export interface CreateCommentDto {
  taskId: number;
  content: string;
}

export interface UpdateCommentDto {
  id: number;
  taskId: number;
  content: string;
}
