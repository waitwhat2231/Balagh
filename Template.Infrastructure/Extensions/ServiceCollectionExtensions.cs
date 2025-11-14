using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Entities;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("TemplateDb");
        services.AddDbContext<TemplateDbContext>(options => options.UseSqlServer(connectionString));

        //this for identity and jwt when needed
        services.AddIdentityCore<User>()
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
    }
}
