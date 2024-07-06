


using AutoMapper;
using ReminderApp.Common.Contracts;
using ReminderApp.DataAccess.Entity;

namespace ReminderApp.Service.Mapping.TypeConverters;


public class ReminderTypeConverter : ITypeConverter<CreateReminderRequest, Reminder>
{
    public Reminder Convert(CreateReminderRequest source, Reminder destination, ResolutionContext context)
    {
        var location = new Location() { Lang = source.Lang.ToString(), Lat = source.Lat.ToString() };
        return Reminder.New(title: source.Title, location: location, description: source.Description, date: DateTime.Now);
    }
}