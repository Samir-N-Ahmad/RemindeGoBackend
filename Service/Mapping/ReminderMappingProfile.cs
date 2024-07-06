

using AutoMapper;
using ReminderApp.Common.Contracts;
using ReminderApp.DataAccess.Entity;
using ReminderApp.Service.Mapping.TypeConverters;

namespace ReminderApp.Service.Mapping;


public class ReminderMappingProfile : Profile
{
    public ReminderMappingProfile()
    {
        CreateMap<CreateReminderRequest, Reminder>().ConvertUsing<ReminderTypeConverter>();
        CreateMap<Reminder, CreateReminderResult>().ConvertUsing<CreateReminderResultTypeConverter>();
    }

}