

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

                    var admin = new AppUser
                    {
                        FirstName = "Admin",
                        LastName = "Admin",
                        Email = "Admin@ReminderApp.ai",
                        Role = (int)UserRole.Admin,
                        PasswordHash = ""
                    };
                    // add an admin user
                    await dbContext.AppUsers.AddAsync(admin);

                    // create profile for the admin user
                    await dbContext.UserProfile.AddAsync(UserProfile.New(admin.Id, null, null, null));

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