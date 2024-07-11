


using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ReminderApp.Common;
using ReminderApp.Common.Configs;
using ReminderApp.DataAccess;
using ReminderApp.DataAccess.Entity;
using ReminderApp.DataAccess.Repository;
using ReminderApp.Service;
using ReminderApp.Service.Mapping;

internal class Program
{
    static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder();
        var dbSettings = builder.Configuration.GetSection(DatabaseConfigs.SectionTitle);
        builder.Services.AddAutoMapper(typeof(ReminderMappingProfile));
        builder.Services.Configure<DatabaseConfigs>(dbSettings);


        builder.Services.AddDbContext<DatabaseContext>();


        builder.Services.AddIdentity<AppUser, IdentityRole>(
            options =>
        {
            options.Password = new()
            {
                RequireDigit = true,
                RequiredLength = 8,
                RequireUppercase = true,
                RequireLowercase = true
            };

            options.SignIn = new SignInOptions()
            {
                RequireConfirmedEmail = false,
                RequireConfirmedAccount = false,
                RequireConfirmedPhoneNumber = false
            };
        })
        .AddEntityFrameworkStores<DatabaseContext>()
        .AddDefaultTokenProviders()
        .AddRoles<IdentityRole>()
        .AddSignInManager();

        var jwtSettings = builder.Configuration.GetSection(JWtSettings.SectionTitle);

        builder.Services.AddAuthentication(options =>
              {

                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
              })
              .AddJwtBearer(options =>
        {
            options.IncludeErrorDetails = true;
            var jwtSettings = builder.Configuration.GetSection(JWtSettings.SectionTitle).Get<JWtSettings>();
            options.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,


                ValidIssuer = jwtSettings.ValidIssuer,
                ValidAudiences = jwtSettings.ValidAudeinces,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SigningKeys))

            };
        });

        builder.Services.AddAuthorizationBuilder();


        builder.Services.AddScoped<IReminderService, ReminderService>();
        builder.Services.AddScoped<IReminderRepository, ReminderReository>();
        builder.Services.AddControllers();

        builder.Services.AddProblemDetails();
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