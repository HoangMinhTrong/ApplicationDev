using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ApplicationDev.Data.Initializer
{
    public static class RoleInitializer
    {
        public static async Task SeedSuperAdminAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Default User
            var defaultUser = new ApplicationUser 
            {
                UserName = "superadmin", 
                Email = "superadmin@gmail.com",
                FirstName = "Trong",
                LastName = "Hoang",
                EmailConfirmed = true, 
                PhoneNumberConfirmed = true 
            };
            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser.Email);
                if(user==null)
                {
                    await userManager.CreateAsync(defaultUser, "Password@123");
                    await userManager.AddToRoleAsync(defaultUser, Enum.Roles.Customer.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enum.Roles.StoreOwner.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enum.Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Enum.Roles.SuperAdmin.ToString());
                }
               
            }
        }
        public static async Task SeedRolesAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.StoreOwner.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Enum.Roles.Customer.ToString()));
        }
    }
}