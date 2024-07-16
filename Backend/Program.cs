


using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Backend.Common;
using Backend.Common.Configs;
using Backend.DataAccess;
using Backend.DataAccess.Entity;
using Backend.DataAccess.Repository;
using Backend.Service;
using Backend.Service.Mapping;


namespace RemindeGo;
internal class Program
{
    static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder();
        var dbSettings = builder.Configuration.GetSection(DatabaseConfigs.SectionTitle);
        builder.Services.AddAutoMapper(typeof(ReminderMappingProfile));
        builder.Services.Configure<DatabaseConfigs>(dbSettings);


        builder.Services.AddDbContext<DatabaseContext>();


        builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(
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
        .AddRoles<IdentityRole<Guid>>()
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