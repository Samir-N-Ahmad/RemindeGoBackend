

namespace ReminderApp.Service;

public interface IReminderService
{
    public Task CreateReminder();
    public Task DeleteReminder();
    public Task ChangeReminderDate();
    public Task ChangeReminderLocation();
    public Task DiableReminder();
    public Task EnableReminder();
}