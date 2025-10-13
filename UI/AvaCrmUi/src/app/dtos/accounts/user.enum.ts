export enum UserGender {
  Male = 1,
  Female = 2
}

export enum LoginState
{
  Success,
  InvalidCredentials,
  LockedOut,
  RequiresTwoFactor,
  TemporaryLockedOut
}
