import {LoginState} from './accounts/user.enum';
import {UserDto} from './accounts/user.dto';

export interface  LoginResponseDto{
  loginState : LoginState,
  token : string,
  user : UserDto
}
