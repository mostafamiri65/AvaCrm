export interface RoleListDto {
  id: number;
  titleEnglish?: string;
  titlePersian?: string;
  createdDate: Date;
}

export interface RoleCreateDto {
  titleEnglish?: string;
  titlePersian?: string;
}

export interface RoleUpdateDto {
  id: number;
  titleEnglish?: string;
  titlePersian?: string;
}
