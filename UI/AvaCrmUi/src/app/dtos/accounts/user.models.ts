// models/user.models.ts
export interface UserListDto {
  id: number;
  username?: string;
  email?: string;
  roleId: number;
  roleTitlePersian?: string;
  roleTitleEnglish?: string;
  emailConfirmed: boolean;
  phoneNumber?: string;
  phoneNumberConfirmed: boolean;
  lockoutEnabled: boolean;
  lockoutTotal: boolean;
  createdDate: Date;
}

export interface UserDetailDto {
  id: number;
  username?: string;
  email?: string;
  roleId: number;
  roleTitlePersian?: string;
  roleTitleEnglish?: string;
  emailConfirmed: boolean;
  phoneNumber?: string;
  phoneNumberConfirmed: boolean;
  twoFactorEnabled: boolean;
  lockoutEnabled: boolean;
  lockoutTotal: boolean;
  accessFailedCount: number;
  lockoutEnd?: Date;
  createdDate: Date;
}

export interface UserCreateDto {
  username?: string;
  email?: string;
  password?: string;
  roleId: number;
  phoneNumber?: string;
}

export interface UserUpdateDto {
  id: number;
  username?: string;
  email?: string;
  roleId: number;
  phoneNumber?: string;
  lockoutEnabled: boolean;
  lockoutTotal: boolean;
}

export interface UserChangePasswordDto {
  userId: number;
  newPassword: string;
}
