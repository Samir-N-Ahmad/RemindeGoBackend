


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
using Mailjet.Client;
using Backend.Service.MailService;
using Backend.Common.Utilities;


internal class Program
{
    static async Task Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder();
        var dbSettings = builder.Configuration.GetSection(DatabaseConfigs.SectionTitle);
        builder.Services.AddAutoMapper(typeof(ReminderMappingProfile));
        builder.Services.Configure<DatabaseConfigs>(dbSettings);


        builder.Services.AddDbContext<DatabaseContext>();

        builder.Services.Configure<JWtSettings>(builder.Configuration.GetSection(JWtSettings.SectionTitle));

        // Mailjet
        var mailJetConfigSection = builder.Configuration.GetSection(MailJetConfigs.SectionTitle);
        builder.Services.AddHttpClient<IMailjetClient, MailjetClient>(client =>
        {
            //set BaseAddress, MediaType, UserAgent
            client.SetDefaultSettings();
            client.BaseAddress = new Uri("https://api.mailjet.com");
            client.UseBasicAuthentication(mailJetConfigSection.Get<MailJetConfigs>()?.ApiKey, mailJetConfigSection.Get<MailJetConfigs>()?.ApiSecret);
        });
        builder.Services.Configure<MailJetConfigs>(mailJetConfigSection);


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
                RequireConfirmedPhoneNumber = false,

            };

            options.User.RequireUniqueEmail = true;
            options.Lockout.MaxFailedAccessAttempts = 10;
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


                ValidIssuer = jwtSettings?.ValidIssuer,
                ValidAudiences = jwtSettings?.ValidAudiences,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings?.SigningKeys))

            };

        });

        builder.Services.AddAuthorizationBuilder();


        builder.Services.AddScoped<IReminderService, ReminderService>();
        builder.Services.AddScoped<IReminderRepository, ReminderReository>();
        builder.Services.AddTransient<ITokenGenerator, TokenGenerator>();
        builder.Services.AddTransient<IAuthService, AuthService>();
        builder.Services.AddTransient<IMailService, MailService>();

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