using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Template.API.AOP;
using TripPlanner.API.Middlewares;

namespace Template.API.Extensions
{
    public static class WebApplicationBuilderExtension
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            builder.Services.AddHttpContextAccessor();


            builder.Services.AddSingleton(new ActivitySource("Balagh.App"));
            builder.Services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(TracingBehavior<,>));

            builder.Services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("api-version")
                    );
            }).AddMvc()
            .AddApiExplorer(options =>
            {
                // the default is ToString(), but we want "'v'major[.minor][-status]"
                options.GroupNameFormat = "'v'VVV";

                // if we have both parts, decided how to format the group
                // from the example: "Sales - v1"
                options.FormatGroupName = (group, version) => $"{group} - {version}";
            });
            builder.Services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(LoggingBehavior<,>)
                );

            builder.Services.AddScoped<ExceptionHandlerMiddleware>();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                    ValidAudience = builder.Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
                };
            }
            );

            builder.Services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });
            ;
            builder.Services.AddSwaggerGen(c =>
            {
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearerAuth" }
                        },
                        []
                    }
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            //builder.Services.AddIdentityApiEndpoints<User>();
        }
    }
}
