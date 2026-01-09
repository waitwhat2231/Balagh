using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using QuestPDF.Infrastructure;
using Template.Application.Events;
using Template.Application.PDFFilesHelper;
using Template.Application.Users;


namespace Template.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplication(this IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtensions).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

        services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<IPdfExportService, PdfExportService>();
        QuestPDF.Settings.License = LicenseType.Community;

        services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddMemoryCache();

        services.AddAutoMapper(applicationAssembly);

    }
}