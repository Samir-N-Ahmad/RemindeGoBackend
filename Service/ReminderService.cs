


using AutoMapper;
using ErrorOr;
using ReminderApp.Common.Contracts;
using ReminderApp.DataAccess.Entity;
using ReminderApp.DataAccess.Repository;

namespace ReminderApp.Service;

public class ReminderService(IReminderRepository reminderRepository, IMapper mapper) : IReminderService
{
    private readonly IReminderRepository _reminderRepository = reminderRepository;
    private readonly IMapper _mapper = mapper;
    public Task<ErrorOr<bool>> ChangeReminderLocation()
    {
        throw new NotImplementedException();
    }

    public async Task<ErrorOr<CreateReminderResult>> CreateReminder(CreateReminderRequest request)
    {
        try
        {
            Reminder reminder = _mapper.Map<Reminder>(request);
            await _reminderRepository.AddReminder(reminder);
            return _mapper.Map<CreateReminderResult>(reminder);
        }
        catch (Exception e)
        {
            return Error.Failure(e.Message);
        }
    }

    public Task<ErrorOr<bool>> DeleteReminder()
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<bool>> DiableReminder()
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<bool>> EnableReminder()
    {
        throw new NotImplementedException();
    }
}