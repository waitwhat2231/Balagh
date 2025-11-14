using Microsoft.AspNetCore.Identity;
using Template.Infrastructure.Persistence;

namespace Template.Infrastructure.Seeders;

public class RolesSeeder(TemplateDbContext dbContext) : IRolesSeeder
{
    public async Task Seed()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Roles.Any())
            {
                var roles = GetRoles();
                dbContext.Roles.AddRange(roles);
                await dbContext.SaveChangesAsync();
            }
        }
    }
    public IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles = [
            new ()
                {
                    Name = "User",
                    NormalizedName = "USER"
                },
                new ()
                {
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR"
                },
                new ()
                {
                    Name = "Employee",
                    NormalizedName = "Employee"
                },
                ];
        return roles;
    }
}
