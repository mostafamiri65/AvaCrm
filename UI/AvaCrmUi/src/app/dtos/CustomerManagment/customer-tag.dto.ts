export interface CustomerTagListDto {
  tagId: number;
  customerId: number;
  tagTitle: string;
  customerCode?: string;
}

export interface CustomerTagCreateDto {
  customerId: number;
  tagId: number;
}
