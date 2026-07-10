using Microsoft.AspNetCore.Identity;
using EcoMeal.Constants;
using EcoMeal.Entities;

namespace EcoMeal.Database;

public static class DbSeeder
{
    public static async Task SeedAsync(IServiceProvider services, IConfiguration config)
    {
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var role in AppRoles.AllRoles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        //var email = config.GetValue<string>("AdminUser:Email");
        //var pass = config.GetValue<string>("AdminUser:Password");
        var email = config["SeedAdmin:Email"];
        var pass = config["SeedAdmin:Password"];

        var existingAdmin = await userManager.FindByEmailAsync(email);

        if (existingAdmin == null)
        {
            var admin = new ApplicationUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                FullName = "Administrator" // alternatively, "admin user" :p
            };
            var result = await userManager.CreateAsync(admin, pass);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(admin, AppRoles.Admin);
            }

        }
    }
}