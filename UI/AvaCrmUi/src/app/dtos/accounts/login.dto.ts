import {UserDto} from './user.dto';

export interface LoginDto {
  username: string;
  password: string;
  captchaId?: string;
  captchaAnswer?: string;
}


export interface AuthResponse {
  loginState: LoginState;
  token?: string;
  user?: UserDto;
  message?: string;
}

export enum LoginState {
  Success = 0,
  InvalidCredentials = 1,
  RequiresTwoFactor = 2,
  TemporaryLockedOut = 3,
  LockedOut = 4,
  CaptchaRequired = 5,
  CaptchaFailed = 6
}
