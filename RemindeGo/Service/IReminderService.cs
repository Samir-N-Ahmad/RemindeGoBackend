

using RemindeGo.Common.Contracts;
using RemindeGo.DataAccess.Entity;
using ErrorOr;
using RemindeGo.Service.Contracts;
namespace RemindeGo.Service;

public interface IReminderService
{
    public Task<ErrorOr<CreateReminderResult>> CreateReminder(CreateReminderRequest request);
    public Task<ErrorOr<bool>> DeleteReminder();
    public Task<ErrorOr<bool>> ChangeReminderLocation();
    public Task<ErrorOr<bool>> DiableReminder();
    public Task<ErrorOr<bool>> EnableReminder();
}