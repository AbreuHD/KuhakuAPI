using Core.Application.Enum;
using Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class DefaultUser
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser user = new ApplicationUser
            {
                Name = "Jefferson",
                LastName = "Abreu",
                UserName = "AbreuHDm",
                Email = "abreuhd08@gmail.com",
                PhoneNumber = "809-491-0570",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true

            };
            if (userManager.Users.All(u => u.Id != user.Id))
            {
                var userEmail = await userManager.FindByEmailAsync(user.Email);
                if (userEmail == null)
                {
                    await userManager.CreateAsync(user, "123Pa$$word!");
                    await userManager.AddToRoleAsync(user, Roles.User.ToString());
                }

            }
        }
    }
}
