using LR4.DbConnection;
using LR4.Entities;
using LR4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LR4;

public static class DbContextInitializer
{
    public async static Task<IServiceCollection> SeedDatabase(
       this IServiceCollection services,
       RoleManager<ApplicationRole> roleManager,
       UserManager<User> userManager,
       DataContext context
   )
    {
        if (!roleManager.Roles.AnyAsync(t => t.Name == Roles.Admin).GetAwaiter().GetResult())
            roleManager.CreateAsync(new ApplicationRole(Roles.Admin)).GetAwaiter().GetResult();

        if (!roleManager.Roles.AnyAsync(t => t.Name == Roles.User).GetAwaiter().GetResult())
            roleManager.CreateAsync(new ApplicationRole(Roles.User)).GetAwaiter().GetResult();

        var admins = await userManager.GetUsersInRoleAsync(Roles.Admin);

        if (admins.Count < 1)
        {
            var admin = new User
            {
                UserName = "admin",
                FirstName = "admin",
                LastName = "admin",
                PhoneNumber = "+380999999999",
                Email = "admin@gmail.com",
                PhoneNumberConfirmed = true
            };

            await userManager.CreateAsync(admin, "admin");
            await userManager.AddToRoleAsync(admin, Roles.Admin);
        }

        return services;
    }
}

