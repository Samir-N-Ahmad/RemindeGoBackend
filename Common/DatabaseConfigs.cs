


using System.Security.Policy;

namespace ReminderApp.Common;
public record DatabaseConfigs
{
    public const string SectionTitle = "DatabaseSettings";
    public string? DatabaseName { get; set; }
    public string? ConnectionString { get; set; }

}