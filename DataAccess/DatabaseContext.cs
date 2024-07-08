

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReminderApp.Common;
using ReminderApp.DataAccess.Entity;

namespace ReminderApp.DataAccess;

public class DatabaseContext(IOptions<DatabaseConfigs> dbSettings) : DbContext
{

    private readonly IOptions<DatabaseConfigs> _dbSettings = dbSettings;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {


        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseMySql(_dbSettings.Value.ConnectionString, MariaDbServerVersion.AutoDetect(_dbSettings.Value.ConnectionString));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Reminder>().HasOne<Location>();

    }

    public DbSet<Reminder> Reminders { get; set; }
    public DbSet<Location> Locations { get; set; }
}
