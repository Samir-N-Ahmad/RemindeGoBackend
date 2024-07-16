

using System.Data.Common;
using Backend.DataAccess.Entity;

namespace Backend.DataAccess.Repository;

public class ReminderReository(DatabaseContext databaseContext) : IReminderRepository
{

    private readonly DatabaseContext _databaseContext = databaseContext;
    public async Task<Reminder> AddReminder(Reminder reminder)
    {
        try
        {
            var addedReminder = await _databaseContext.Reminders.AddAsync(reminder);
            _databaseContext.SaveChanges();

            return reminder;

        }
        catch (DbException)
        {
            throw new Exception("Database error");
        }
        catch (Exception)
        {
            throw new Exception("Unknown error");

        }


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