using Microsoft.AspNetCore.Identity;

namespace HotelManagementSystem.Api.Roles;

public static class DefaultRoles
{
    public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));
            await roleManager.CreateAsync(new IdentityRole(AppRoles.Receptionist));
            await roleManager.CreateAsync(new IdentityRole(AppRoles.Customer));
        }
    }
}
