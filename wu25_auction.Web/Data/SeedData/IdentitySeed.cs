using Microsoft.AspNetCore.Identity;
using wu25_auction.Web.Data.Entities;

namespace wu25_auction.Web.Data.SeedData;

public static class IdentitySeed
{
    public static async Task SeedIdentityAsync(this IServiceProvider services)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        string[] roles = { "Admin", "Manager", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(role));
                if (!result.Succeeded)
                    throw new Exception($"Kunde inte skapa rollen {role}");
            }
        }

        var adminEmail = "admin@gg.se";
        var adminPassword = "Admin123!";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser is null)
        {
            adminUser = ApplicationUser.Create(
                email: adminEmail, "System", "Admin");

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (!result.Succeeded)
                throw new Exception("Kunde inte skapa admin-användaren");
        }

        await EnsureRole(userManager, adminUser, "User");
        await EnsureRole(userManager, adminUser, "Manager");
        await EnsureRole(userManager, adminUser, "Admin");
    }

    private static async Task EnsureRole(
        UserManager<ApplicationUser> userManager,
        ApplicationUser user,
        string role)
    {
        if (!await userManager.IsInRoleAsync(user, role))
            await userManager.AddToRoleAsync(user, role);
    }
}
