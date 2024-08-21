

using AutoMapper;
using RemindeGo.Common.Contracts;
using RemindeGo.DataAccess.Entity;
using RemindeGo.Service.Mapping.TypeConverters;

namespace RemindeGo.Service.Mapping;


public class ReminderMappingProfile : Profile
{
    public ReminderMappingProfile()
    {
        CreateMap<CreateReminderRequest, Reminder>().ConvertUsing<ReminderTypeConverter>();
        CreateMap<Reminder, CreateReminderResult>().ConvertUsing<CreateReminderResultTypeConverter>();
    }

}