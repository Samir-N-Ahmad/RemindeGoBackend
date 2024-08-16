

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Backend.Common.Configs;
using Backend.DataAccess.Entity;

namespace Backend.DataAccess;

public class DatabaseContext(DbContextOptions<DatabaseContext> options, IOptions<DatabaseConfigs> dbSettings) : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>(options)
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

        // Location => Rrminder : many -> one
        modelBuilder.Entity<Location>()
        .HasOne(l => l.Reminder)
        .WithMany(r => r.ReminderLocations)
        .HasForeignKey(l => l.ReminderId);

        // Appuser => UserProfile : one -> one
        modelBuilder.Entity<AppUser>()
        .HasOne(au => au.UserProfile)
        .WithOne(p => p.User)
        .HasForeignKey<UserProfile>(p => p.UserId);

        // Reminder => Profile : many -> one
        modelBuilder.Entity<Reminder>()
        .HasOne(r => r.ReminderUserProfile)
        .WithMany(p => p.Reminders)
        .HasForeignKey(r => r.ReminderUserProfileId);


    }

    public DbSet<Reminder> Reminders { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<UserProfile> UserProfile { get; set; }
}
