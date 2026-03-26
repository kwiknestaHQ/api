using KwikNestaIdentity.Domain.Entities;
using KwikNestaIdentity.Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KwikNestaIdentity.Infrastructure
{
    public static class IdentityInfraDIExtensions
    {
        public static IServiceCollection ConfigureIdentityServiceDbContexts(this IServiceCollection services,
                                                            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("Connection string not set.");

            services.AddDbContext<IdentityServiceDbContext>(options =>
                options.UseNpgsql(connectionString));

            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityServiceDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager();

            return services;
        }

        public static WebApplication RunIdentityServiceMigrations(this WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                using var scope = app.Services.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<IdentityServiceDbContext>();
                db.Database.Migrate();
            }

            return app;
        }

        public static async Task<IHost> SeedIdentityData(this IHost host)
        {
            await host.SeedIdentityDataAsync();
            return host;
        }
    }
}