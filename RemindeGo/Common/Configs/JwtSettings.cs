
namespace RemindeGo.Common;

public record JWtSettings()
{
    public const string SectionTitle = "Jwt";
    public required string ValidIssuer { get; init; }
    public required string SigningKeys { get; init; }
    public required ICollection<string> ValidAudeinces { get; init; }
}