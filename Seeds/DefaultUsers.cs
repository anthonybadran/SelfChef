using Microsoft.AspNetCore.Identity;
using SelfChef.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelfChef.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new ApplicationUser
            {
                UserName = "admin@selfchef.com",
                Email = "admin@selfchef.com",
                EmailConfirmed = true,
                FirstName = "admin",
                LastName = "admin"
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser, "Admin123*");
                    await userManager.AddToRoleAsync(defaultUser, "Admin");
                }
            }
        }
    }
}
