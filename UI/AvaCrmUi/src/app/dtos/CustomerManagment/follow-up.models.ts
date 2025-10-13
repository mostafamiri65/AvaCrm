
export interface FollowUpCreateDto {
  customerId: number;
  description: string;
  nextFollowUpDate?: Date;
  nextFollowUpDescription?: string;
}

export interface FollowUpUpdateDto {
  id: number;
  description: string;
  nextFollowUpDate?: Date;
  nextFollowUpDescription?: string;
}

export interface FollowUpListDto {
  id: number;
  customerId: number;
  description: string;
  nextFollowUpDate?: Date;
  nextFollowUpDescription?: string;
  createdDate: Date;
  customerCode: string;
}
