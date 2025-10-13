import {UserGender} from './user.enum';

export interface UserDto {
  userId: number;
  userName: string;
  email: string;
  phoneNumber: string;
  fullName: string;
  avatarFile: string;
  userGender: UserGender;
  roleId: number;
}
