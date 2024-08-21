


using AutoMapper;
using RemindeGo.Common.Contracts;
using RemindeGo.DataAccess.Entity;

namespace RemindeGo.Service.Mapping.TypeConverters;


public class CreateReminderResultTypeConverter : ITypeConverter<Reminder, CreateReminderResult>
{

    public CreateReminderResultTypeConverter() { }
    CreateReminderResult ITypeConverter<Reminder, CreateReminderResult>.Convert(Reminder source, CreateReminderResult destination, ResolutionContext context)
    {


        double? lat =
         source.ReminderLocations.Count > 0 ? double.Parse(source.ReminderLocations.First().Lat) : null;
        double? lang =
       source.ReminderLocations.Count > 0 ? double.Parse(source.ReminderLocations.First().Lang) : null;

        return new CreateReminderResult(source.Title,

          lang,
             lat,
             source.Description);
    }
}