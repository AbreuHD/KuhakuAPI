using K_haku.Core.Application.Enum;
using K_haku.Infrastructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K_haku.Infrastructure.Identity.Seeds
{
    public static class DefaultOwner
    {
        public static async Task Seed(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var user = new ApplicationUser 
            {
                Name="Jefferson",
                LastName="Abreu",
                UserName="AbreuHD",
                Email="AbrueMartinezJefferson@gmail.com",
                PhoneNumber="809-491-0570",
                EmailConfirmed=true
            };
            if(userManager.Users.All(u=>u.Id != user.Id))
            {
                var userEmail = await userManager.FindByEmailAsync(user.Email);
                if(userEmail == null)
                {
                    await userManager.CreateAsync(user, "123Pa$$word!");
                    await userManager.AddToRoleAsync(user, Roles.Owner.ToString());
                }
            }
        }
    }
}
