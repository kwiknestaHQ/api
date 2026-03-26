using Asp.Versioning;
using Hangfire;
using Hangfire.Console;
using Hangfire.PostgreSql;
using Hangfire.RecurringJobExtensions;
using KwikNesta.Mediator.Cores.Abstractions;
using KwikNesta.Mediator.Cores.Extensions;
using KwikNesta.Mediator.Cores.Implementations.Pipelines;
using KwikNesta.Shared.Constants;
using KwikNesta.Shared.Contracts;
using KwikNesta.Shared.Implementations;
using KwikNesta.Shared.Models.Settings;
using KwikNestaIdentity.Application;
using KwikNestaIdentity.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using KwikNestaInfra.Infrastructure;
using System.Text;
using KwikNestaInfra.Application;
using KwikNesta.Mediator.Hangfire.Extensions;
using KwikNestaProperty.Infrastructure;

namespace KwikNestaGateway.API.Extensions
{
    public static class AppServiceDIExtension
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureMediator()
                .AddEndpointsApiExplorer()
                .ConfigureController()
                .AddHttpClient()
                .ConfigureJwt(configuration)
                .ConfigureSwagger()
                .ConfigureVersioning()
                .ConfigureSettings(configuration)
                .ConfigureCors()
                .ConfigureDbContexts(configuration)
                .ConfigureHangfire(configuration)
                .AddOtherServices()
                .AddControllers();
        }

        private static IServiceCollection ConfigureJwt(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("Jwt");
            var settings = section.Get<JwtSettings>();
            if (settings != null)
            {
                services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                }).AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = settings.Issuer,

                        ValidateAudience = true,
                        ValidAudience = settings.Audience,

                        ValidateLifetime = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key))
                    };
                });

                services.AddAuthorization();
            }

            return services;
        }

        private static IServiceCollection ConfigureMediator(this  IServiceCollection services)
        {
            return services
                .ConfigureKNMediators(
                    typeof(IdentityAppAssemblyMarker).Assembly, 
                    typeof(InfraAppAssemblyMarker).Assembly)
                .AddTransient(typeof(IKNPipelineBehavior<,>), typeof(LoggingBehavior<,>))
                .AddTransient(typeof(IKNNotificationBehavior<>), typeof(NotificationLoggingBehavior<>))
                .ConfigureKNBackgroundMediators();
        }

        private static IServiceCollection ConfigureController(this IServiceCollection services)
        {
            services.AddControllers();
            return services;
        }

        private static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "KwikNesta Gateway API",
                    Version = "v1",
                    Description = "KwikNesta Gateway API 1.0",
                    Contact = new OpenApiContact
                    {
                        Name = "KwikNesta",
                        Email = "info@kwiknesta.com",
                        Url = new Uri("https://kwik-nesta.com")
                    }
                });
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "KwikNesta Gateway API",
                    Version = "v2",
                    Description = "KwikNesta Gateway API v2.0",
                    Contact = new OpenApiContact
                    {
                        Name = "KwikNesta",
                        Email = "info@kwiknesta.com",
                        Url = new Uri("https://kwik-nesta.com")
                    }
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter: Bearer {JWT Token}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        private static IServiceCollection ConfigureVersioning(this IServiceCollection services)
        {
            var versioningBuilder = services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new HeaderApiVersionReader("api-version"),
                    new HeaderApiVersionReader("X-Version"),
                    new UrlSegmentApiVersionReader());
            });

            versioningBuilder.AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            return services;
        }

        private static IServiceCollection ConfigureSettings(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .Configure<KNApplicationSettings>(configuration);            
        }

        private static IServiceCollection ConfigureCors(this IServiceCollection services)
        {
            return services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }

        private static IServiceCollection ConfigureDbContexts(this IServiceCollection services,
                                                            IConfiguration configuration)
        {
            services.ConfigureIdentityServiceDbContexts(configuration)
                .ConfigureInfraServices(configuration)
                .ConfigurePropertyServices(configuration);
            return services;
        }

        private static IServiceCollection ConfigureHangfire(this IServiceCollection services,
                                                           IConfiguration configuration)
        {
            services.AddHangfire(config =>
            {
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UsePostgreSqlStorage(opt =>
                    {
                        opt.UseNpgsqlConnection(configuration.GetConnectionString("DefaultConnection"));
                    }, new PostgreSqlStorageOptions
                    {
                        SchemaName = "my-gov-pay-hangfire",
                        PrepareSchemaIfNecessary = true
                    })
                    .UseRecurringJob(typeof(IRecurringJobsService))
                    .UseConsole()
                    .UseFilter(new AutomaticRetryAttribute()
                    {
                        Attempts = 5,
                        DelayInSecondsByAttemptFunc = _ => 60
                    });
            }).AddHangfireServer(opt =>
            {
                opt.ServerName = "KwikNesta API";
                opt.Queues = new[] { HangfireQueues.Recurring, HangfireQueues.Default };
                opt.SchedulePollingInterval = TimeSpan.FromSeconds(30);
                opt.WorkerCount = 5;
            });

            return services;
        }

        private static IServiceCollection AddOtherServices(this IServiceCollection services)
        {
            return services.AddScoped<INotificationService, NotificationService>()
                .AddScoped<IIdentityRepositoryManager, IdentityRepositoryManager>()
                .AddScoped<IUploadService, UploadService>();
        }
    }
}