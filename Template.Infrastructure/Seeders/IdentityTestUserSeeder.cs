using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Template.Domain.Entities;
namespace Template.Infrastructure.Seeders
{


    public static class IdentityTestUserSeeder
    {
        public static async Task SeedUsersAsync(
            IServiceProvider services,
            int userCount = 100)
        {
            using var scope = services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            for (int i = 1; i <= userCount; i++)
            {
                var email = $"loadtest{i}@example.com";

                if (await userManager.FindByEmailAsync(email) != null)
                    continue;

                var user = new User
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    GovernmentalEntityId = null,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(user, "Test@12345");
                var roleResult = await userManager.AddToRoleAsync(user, "User");
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ",
                        result.Errors.Select(e => e.Description)));
                }
            }
        }
    }

}
