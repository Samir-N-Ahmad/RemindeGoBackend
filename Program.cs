


using Microsoft.EntityFrameworkCore.Metadata;
using ReminderApp.Common;
using ReminderApp.DataAccess;
using ReminderApp.Service.Mapping;

internal class Program
{
    static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder();
        var dbSettings = builder.Configuration.GetSection(DatabaseConfigs.SectionTitle);
        builder.Services.Configure<DatabaseConfigs>(dbSettings);
        builder.Services.AddDbContext<DatabaseContext>();
        builder.Services.AddControllers();
        builder.Services.AddAutoMapper(conf => conf.AddProfile(typeof(ReminderMappingProfile)));


        var app = builder.Build();
        app.UsePathBase("/RemindeGo");
        app.MapControllers();
        app.UseAuthentication();
        app.UseAuthorization();
        await DatabaseSerializer.SeedAsync(app.Services, app.Environment.IsDevelopment());
        app.Run();
        return;

    }
}