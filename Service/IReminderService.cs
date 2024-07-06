

using ReminderApp.Common.Contracts;
using ReminderApp.DataAccess.Entity;
using ErrorOr;
using ReminderApp.Service.Contracts;
namespace ReminderApp.Service;

public interface IReminderService
{
    public Task<ErrorOr<CreateReminderResult>> CreateReminder(CreateReminderRequest request);
    public Task<ErrorOr<bool>> DeleteReminder();
    public Task<ErrorOr<bool>> ChangeReminderLocation();
    public Task<ErrorOr<bool>> DiableReminder();
    public Task<ErrorOr<bool>> EnableReminder();
}