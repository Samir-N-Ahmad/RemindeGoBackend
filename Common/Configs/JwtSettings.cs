
namespace ReminderApp.Common;

public record JWtSettings()
{
    public const string SectionTitle = "Jwt";
    public required string ValidIssuer { get; init; }
    public required string SigningKeys { get; init; }
    public required string ValidAudeinces { get; init; }
}