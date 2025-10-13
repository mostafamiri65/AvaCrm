export interface NoteListDto {
  id: number;
  customerId: number;
  content: string;
  createdDate: Date;
  customerCode: string;
}

export interface NoteCreateDto {
  customerId: number;
  content: string;
}

export interface NoteUpdateDto {
  id: number;
  content: string;
}
