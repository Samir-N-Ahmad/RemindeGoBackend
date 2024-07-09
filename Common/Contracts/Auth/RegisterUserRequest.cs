namespace ReminderApp.Common.Contracts.Auth;

public record RegisterUserRequest(string FirstName, double LastName, string Email, string EmailConfirmation, string Password, string PasswordConfirmation);