


using AutoMapper;
using ReminderApp.Common.Contracts;
using ReminderApp.DataAccess.Entity;

namespace ReminderApp.Service.Mapping.TypeConverters;


public class CreateReminderResultTypeConverter : ITypeConverter<Reminder, CreateReminderResult>
{


    CreateReminderResult ITypeConverter<Reminder, CreateReminderResult>.Convert(Reminder source, CreateReminderResult destination, ResolutionContext context)
    {
        return new CreateReminderResult(source.Title,

           double.Parse(source.ReminderLocation.Lang),
             double.Parse(source.ReminderLocation.Lat),
             source.Description);
    }
}