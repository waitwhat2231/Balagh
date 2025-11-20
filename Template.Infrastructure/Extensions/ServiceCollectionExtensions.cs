using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Entities;
using Template.Domain.Repositories;
using Template.Domain.Services;
using Template.Infrastructure.Persistence;
using Template.Infrastructure.Repositories;
using Template.Infrastructure.Seeders;
using Template.Infrastructure.Services;

namespace Template.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("BalaghDb");
        services.AddDbContext<TemplateDbContext>(options => options.UseSqlServer(connectionString));

        //this for identity and jwt when needed
        services.AddIdentityCore<User>(options =>
        {
            options.User.RequireUniqueEmail = true;
        })
              .AddRoles<IdentityRole>()
              .AddTokenProvider<DataProtectorTokenProvider<User>>("TemplateTokenProvidor")
              .AddEntityFrameworkStores<TemplateDbContext>()
              .AddDefaultTokenProviders();
        services.Configure<IdentityOptions>(options =>
        {
            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // lockout duration
            options.Lockout.MaxFailedAccessAttempts = 5; // number of failed attempts
            options.Lockout.AllowedForNewUsers = true;
        });
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        // we use typeof because the interface and the class are generic
        // and without it we would have to specify the type(IGenericRepository<Kit>, GenericType<Kit>)

        services.AddScoped<ITokenRepository, TokenRepository>();
        services.AddScoped<IDeviceRepository, DeviceRepository>();

        services.AddMemoryCache();
        services.AddScoped<ComplaintRepository>();
        services.AddScoped<IComplaintRepository>(provider =>
        {
            var complaintRepository = provider.GetService<ComplaintRepository>()!;

            return new CachedComplaintRepository(
                complaintRepository,
                provider.GetService<IMemoryCache>()!);
        });


        services.AddScoped<IFileService, FileService>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IOTPRepository, OTPRepository>();
        services.AddScoped<IHistoryRepository, HistoryRepository>();
        services.AddScoped<INotesRepository, NotesRepository>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IGovermentalEntitiesRepository, GovermentalEntitiesRepository>();
        services.AddScoped<IRolesSeeder, RolesSeeder>();


        // firebase
        var firebaseKeyPath = Path.Combine(Directory.GetCurrentDirectory(), configuration["Firebase:ServiceAccountFilePath"]);

        if (FirebaseApp.DefaultInstance == null)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(firebaseKeyPath)
            });
        }
    }
}
