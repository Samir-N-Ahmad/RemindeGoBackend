


using AutoMapper;
using Backend.Common.Contracts;
using Backend.DataAccess.Entity;

namespace Backend.Service.Mapping.TypeConverters;


public class ReminderTypeConverter : ITypeConverter<CreateReminderRequest, Reminder>
{
    public ReminderTypeConverter() { }
    public Reminder Convert(CreateReminderRequest source, Reminder destination, ResolutionContext context)
    {
        return Reminder.New(profileId: Guid.Parse(source.profileId), title: source.Title, description: source.Description, date: DateTime.Now);
    }
}