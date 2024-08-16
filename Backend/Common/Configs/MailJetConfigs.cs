
namespace Backend.Common.Configs;


public class MailJetConfigs
{
    public const string SectionTitle = "RemindeGo:MailJet";
    public required string ApiKey { get; set; }
    public required string ApiSecret { get; set; }
}