namespace Backend.Common.Contracts;


public record CreateReminderResult(string Title, double? Lang, double? Lat, string? Description);