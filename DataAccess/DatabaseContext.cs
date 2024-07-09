

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ReminderApp.Common.Configs;
using ReminderApp.DataAccess.Entity;

namespace ReminderApp.DataAccess;

public class DatabaseContext(DbContextOptions<DatabaseContext> options, IOptions<DatabaseConfigs> dbSettings) : IdentityDbContext(options)
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
    public DbSet<AppUser> AppUsers { get; set; }
}
