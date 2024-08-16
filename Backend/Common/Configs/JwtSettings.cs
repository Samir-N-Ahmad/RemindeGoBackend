
namespace Backend.Common.Configs;

public record JWtSettings()
{
    public const string SectionTitle = "Jwt";
    public required string ValidIssuer { get; init; }
    public required string SigningKeys { get; init; }
    public required ICollection<string> ValidAudiences { get; init; }
}