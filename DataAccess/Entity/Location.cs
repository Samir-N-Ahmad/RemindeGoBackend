



using System.ComponentModel.DataAnnotations;

namespace ReminderApp.DataAccess.Entity;

public class Location
{

    private Location(Guid reminderId, string lat, string lang)
    {
        Id = Guid.NewGuid();
        ReminderId = reminderId;
        Lang = lang;
        Lat = lat;

    }

    public static Location New(Guid reminderId, string lat, string lang)
    {
        return new(reminderId, lat, lang);

    }
    [Required]
    public Guid Id { get; init; }
    [Required]
    public string Lat { get; init; }
    [Required]
    public string Lang { get; init; }


    public Reminder Reminder { get; set; }
    [Required]
    public Guid ReminderId { get; set; }

}