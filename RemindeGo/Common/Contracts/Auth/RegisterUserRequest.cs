namespace RemindeGo.Common.Contracts.Auth;

public record RegisterUserRequest(string FirstName, string LastName, string Email, string EmailConfirmation, string Password, string PasswordConfirmation, int Role);