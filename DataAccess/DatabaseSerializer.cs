

using System.Data.Common;
using Microsoft.EntityFrameworkCore;

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

                }
            }
            catch (DbException e)
            {
                throw new Exception($"Could not connect to the Databse server! ${e.ToString()}");
            }

        }
    }

}