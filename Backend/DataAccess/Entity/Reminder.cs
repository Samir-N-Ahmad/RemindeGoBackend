
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Backend.DataAccess.types;

namespace Backend.DataAccess.Entity;

public sealed class Reminder
{

    private Reminder()
    {

    }


    public static Reminder New(string title, DateTime? date, string description, Guid profileId, int status = ((int)ReminderStatus.InActive))
    {
        return new Reminder()
        {

            Id = Guid.NewGuid(),
            Title = title,
            Date = date ?? DateTime.Now,
            Description = description,
            Status = status,
            ReminderUserProfileId = profileId
        };
    }


    [Required]
    public Guid Id { get; private set; }

    [Required]
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public DateTime Date { get; private set; }

    [EnumDataType(typeof(ReminderStatus))]
    [DefaultValue(ReminderStatus.InActive)]
    public int Status { get; private set; }

    [Required]
    public Guid ReminderUserProfileId { get; set; }

    // 
    public UserProfile ReminderUserProfile { get; set; }
    public ICollection<Location> ReminderLocations { get; set; }
}