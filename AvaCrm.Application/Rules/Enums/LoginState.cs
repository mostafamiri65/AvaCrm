namespace AvaCrm.Application.Rules.Enums;

public enum LoginState
{
    Success = 0,
    InvalidCredentials = 1,
    RequiresTwoFactor = 2,
    TemporaryLockedOut = 3,
    LockedOut = 4,
    CaptchaRequired = 5,   
    CaptchaFailed = 6
}
