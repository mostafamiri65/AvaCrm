namespace AvaCrm.Application.Rules.Enums;

public enum LoginState
{
	Success,
	InvalidCredentials,
	LockedOut,
	RequiresTwoFactor,
	TempararyLockedOut
}
