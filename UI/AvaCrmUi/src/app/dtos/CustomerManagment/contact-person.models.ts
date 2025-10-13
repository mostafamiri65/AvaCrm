export interface ContactPersonListDto {
  id: number;
  customerId: number;
  fullName: string;
  jobTitle: string;
  phoneNumber: string;
  email: string;
  customerCode: string;
}

export interface ContactPersonCreateDto {
  customerId: number;
  fullName: string;
  jobTitle: string;
  phoneNumber: string;
  email: string;
}

export interface ContactPersonUpdateDto {
  id: number;
  fullName: string;
  jobTitle: string;
  phoneNumber: string;
  email: string;
}
