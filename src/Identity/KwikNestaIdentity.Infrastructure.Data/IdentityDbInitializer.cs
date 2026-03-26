using KwikNesta.Shared.Extensions;
using KwikNesta.Shared.Models.Enumerations.Identity;
using KwikNesta.Shared.Models.Settings;
using KwikNestaIdentity.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace KwikNestaIdentity.Infrastructure.Data
{
    public static class IdentityDbInitializer
    {
        public static async Task SeedIdentityDataAsync(this IHost host)
        {
            await host.SeedSystemUser();
        }

        private static async Task SeedSystemUser(this IHost host)
        {
            var serviceProvider = host.Services.CreateScope().ServiceProvider;
            var config = serviceProvider.GetRequiredService<IOptions<KNApplicationSettings>>();
            var context = serviceProvider.GetRequiredService<UserManager<User>>();

            if (config.Value.AppAdmin != null)
            {
                var pass = config.Value.AppAdmin.Password;
                var email = config.Value.AppAdmin.Email;
                var phone = config.Value.AppAdmin.Phone;
                if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(pass) && !string.IsNullOrWhiteSpace(phone))
                {
                    var user = await context.FindByEmailAsync(email);
                    if (user == null)
                    {
                        user = new User
                        {
                            PhoneNumber = phone,
                            FirstName = "System",
                            LastName = "Admin",
                            Email = email,
                            EmailConfirmed = true,
                            Status = EUserStatus.Active,
                            Gender = EGender.Male,
                            UserName = email
                        };

                        var createResult = await context.CreateAsync(user, pass);
                        if (!createResult.Succeeded)
                        {
                            return;
                        }

                        var roleResult = await context.AddToRoleAsync(user, ESystemRoles.SuperAdmin.GetDescription());
                        if (!roleResult.Succeeded)
                        {
                            return;
                        }
                    }
                }
            }
        }

    }
}