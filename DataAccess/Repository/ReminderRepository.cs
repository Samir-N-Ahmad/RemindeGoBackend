

using System.Data.Common;
using ReminderApp.DataAccess.Entity;

namespace ReminderApp.DataAccess.Repository;

public class ReminderReository(DatabaseContext databaseContext) : IReminderRepository
{

    private readonly DatabaseContext _databaseContext = databaseContext;
    public async Task<Reminder> AddReminder(Reminder reminder)
    {
        try
        {
            await _databaseContext.Database.BeginTransactionAsync();
            var reminder = await _databaseContext.AddAsync(reminder);
            await _databaseContext.FindAsync(obj => obj.)
            await _databaseContext.Database.CommitTransactionAsync();

        }
        catch (DbException e)
        {

            await _databaseContext.Database.RollbackTransactionAsync();
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