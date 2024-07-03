

using ReminderApp.DataAccess.Entity;

namespace ReminderApp.DataAccess.Repository;

public class ReminderReository : IReminderRepository
{
    public async Task AddReminder(Reminder reminder)
    {
        await Task.Delay(200);
    }

    public async Task Remove(int id)
    {
        await Task.Delay(200);
    }

    public async Task Update(Reminder reminder)
    {
        await Task.Delay(200);
    }
}