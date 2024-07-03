
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ReminderApp.DataAccess.types;

namespace ReminderApp.DataAccess.Entity;

public class Reminder
{

    [Required]
    public required string Id { get; init; }

    [Required]
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required DateTime Date { get; init; }

    [EnumDataType(typeof(ReminderStatus))]
    [DefaultValue(ReminderStatus.InActive)]
    public int Status { get; init; }

    [Required]
    public required Location ReminderLocation { get; init; }
}