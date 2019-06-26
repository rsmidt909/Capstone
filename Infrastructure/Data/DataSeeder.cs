using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yawn.Data;

namespace Infrastructure.Data
{
    public class DataSeeder
    {
        public static void Initialize(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();

            String adminId1 = "";
            String adminId2 = "";

            string role1 = "Admin";
            string desc1 = "This is the Administrator role";
            string role2 = "Member";
            string desc2 = "This is the Member role";

            string password = "Password1!";

            if (roleManager.FindByNameAsync(role1) == null)
            {
                roleManager.CreateAsync(new ApplicationRole(role1, desc1, DateTime.Now));
            }
            if (roleManager.FindByNameAsync(role2) == null)
            {
                roleManager.CreateAsync(new ApplicationRole(role2, desc2, DateTime.Now));
            }

            if (userManager.FindByNameAsync("test@test.com") == null)
            {
                var user = new ApplicationUser
                {
                    FirstName = "Wesley",
                    LastName = "Yawn",
                    StreetAddress = "1808 Eclipse Cv",
                    City = "Austin",
                    State = "Tx",
                    ZipCode = 78723,
                    Phone = 2542542544
                };
                var result = userManager.CreateAsync(user);                              
                userManager.AddPasswordAsync(user, password);
                userManager.AddToRoleAsync(user, role1);               
                adminId1 = user.Id;

            }



        }
    }
}
