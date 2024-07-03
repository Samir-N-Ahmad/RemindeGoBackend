


using ReminderApp.DataAccess;

internal class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder();
        {

            builder.Services.AddDbContext<DatabaseContext>();
            builder.Services.AddControllers();

        }



        var app = builder.Build();
        {
            app.UsePathBase("/RemindeGo");
            app.MapControllers();
            app.UseAuthentication();
            app.UseAuthorization();
        }

        app.Run();

    }
}