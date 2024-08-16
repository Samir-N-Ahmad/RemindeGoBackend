

using System.Data.Common;
using Microsoft.EntityFrameworkCore;

using Backend.DataAccess.Entity;
using Backend.DataAccess.types;

namespace Backend.DataAccess;
public static class DatabaseSerializer
{

    /// <summary>
    /// Seeds the database with initial data, including an admin user.
    /// </summary>
    /// <param name="serviceProvider">The service provider to use for database operations.</param>
    /// <param name="IsDevelopment">A flag indicating whether the database should be deleted before seeding.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
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
                        Email = "Admin@Backend.ai",
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