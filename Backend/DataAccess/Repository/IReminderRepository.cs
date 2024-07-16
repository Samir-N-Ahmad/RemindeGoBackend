using Backend.DataAccess.Entity;

namespace Backend.DataAccess.Repository;

public interface IReminderRepository
{
    public Task<Reminder> AddReminder(Reminder reminder);
    public Task Remove(int id);
    public Task Update(Reminder reminder);
}