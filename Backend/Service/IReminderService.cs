

using Backend.Common.Contracts;
using Backend.DataAccess.Entity;
using ErrorOr;
using Backend.Service.Contracts;
namespace Backend.Service;

public interface IReminderService
{
    public Task<ErrorOr<CreateReminderResult>> CreateReminder(CreateReminderRequest request);
    public Task<ErrorOr<bool>> DeleteReminder();
    public Task<ErrorOr<bool>> ChangeReminderLocation();
    public Task<ErrorOr<bool>> DiableReminder();
    public Task<ErrorOr<bool>> EnableReminder();
}