

using AutoMapper;
using Backend.Common.Contracts;
using Backend.DataAccess.Entity;
using Backend.Service.Mapping.TypeConverters;

namespace Backend.Service.Mapping;


public class ReminderMappingProfile : Profile
{
    public ReminderMappingProfile()
    {
        CreateMap<CreateReminderRequest, Reminder>().ConvertUsing<ReminderTypeConverter>();
        CreateMap<Reminder, CreateReminderResult>().ConvertUsing<CreateReminderResultTypeConverter>();
    }

}