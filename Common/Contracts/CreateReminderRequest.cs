namespace ReminderApp.Common.Contracts;


public record CreateReminderRequest(string Title, double Lang, double Lat, string profileId, string? Description);