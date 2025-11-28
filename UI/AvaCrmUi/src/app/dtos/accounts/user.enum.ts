export enum UserGender {
  Male = 1,
  Female = 2
}

export enum LoginState
{
  Success = 0,
  InvalidCredentials = 1,
  RequiresTwoFactor = 2,
  TemporaryLockedOut = 3,
  LockedOut = 4,
  CaptchaRequired = 5,
  CaptchaFailed = 6
}
