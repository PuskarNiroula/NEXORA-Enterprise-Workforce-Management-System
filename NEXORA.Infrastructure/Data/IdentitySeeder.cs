using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NEXORA.Infrastructure.Models;

namespace NEXORA.Infrastructure.Data;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();

        var context = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        await context.Database.MigrateAsync();

        var userManager = scope.ServiceProvider
            .GetRequiredService<UserManager<UserDbModel>>();

        var roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        const string roleName = "SuperAdmin";
        const string username = "SuperAdmin";
        const string password = "Super@Admin.123";

        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(
                new IdentityRole<Guid>(roleName)
            );
        }

        var user = await userManager.FindByNameAsync(username);

        if (user == null)
        {
            user = new UserDbModel
            {
                UserName = username,
                FullName = "Super Administrator",
                Email = "superadmin@nexora.local",
                EmailConfirmed = true,
                IsActive = true,
                MustChangePassword = true,
                CreatedAt = DateTimeOffset.UtcNow
            };

            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new Exception(
                    string.Join(", ", result.Errors.Select(x => x.Description))
                );
            }
        }

        if (!await userManager.IsInRoleAsync(user, roleName))
        {
            await userManager.AddToRoleAsync(user, roleName);
        }
    }
}