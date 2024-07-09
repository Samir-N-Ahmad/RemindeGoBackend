

using System.Data.Common;
using Microsoft.EntityFrameworkCore;

using ReminderApp.DataAccess.Entity;
using ReminderApp.DataAccess.types;

namespace ReminderApp.DataAccess;
public static class DatabaseSerializer
{

    public static async Task SeedAsync(IServiceProvider serviceProvider, bool IsDevelopment)
    {

        using (var scope = serviceProvider.CreateScope())
        {
            try
            {


                var dbContext = scope.ServiceProvider.GetService<DatabaseContext>();
                if (dbContext is not null)
                {

                    if (IsDevelopment)
                    {
                        await dbContext.Database.EnsureDeletedAsync();
                    }
                    await dbContext.Database.EnsureCreatedAsync();
                    await dbContext.Database.OpenConnectionAsync();

                    await dbContext.Database.BeginTransactionAsync();
                    await dbContext.AppUsers.AddAsync(new AppUser()
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        Email = "Admin@ReminderApp.ai",
                        Role = (int)UserRole.Admin,
                        PasswordHash = ""
                    });

                    await dbContext.SaveChangesAsync();
                    await dbContext.Database.CommitTransactionAsync();

                }
            }
            catch (DbException e)
            {
                throw new Exception($"Could not connect to the Databse server! ${e.ToString()}");
            }

        }
    }

}