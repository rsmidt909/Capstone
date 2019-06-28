using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Yawn.Data;

namespace Infrastructure.Data
{
    public class DataSeeder
    {
        public static async Task Initialize(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            context.Database.EnsureCreated();
            
            string adminId1 = "";
            //string adminId2 = "";

            string role1 = "Admin";
            string desc1 = "This is the Administrator role";
            //string role2 = "Member";
            //string desc2 = "This is the Member role";

            string password = "Password1!";

            if (await roleManager.FindByNameAsync(role1) == null)           
            {
                await roleManager.CreateAsync(new ApplicationRole(role1, desc1, DateTime.Now));
            }
            //if (await roleManager.FindByNameAsync(role2) == null)
            //{
            //    await roleManager.CreateAsync(new ApplicationRole(role2, desc2, DateTime.Now));
            //}

            if (await userManager.FindByNameAsync("wes@test.com") == null)            
            {
                
                var user = new ApplicationUser
                {
                    Email = "wes@test.com",
                    UserName = "wes@test.com",
                    RoleString = "Admin"
                };
                
                var result = await userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    await userManager.AddPasswordAsync(user, password);
                    await userManager.AddToRoleAsync(user, role1);
                }
                //context.Admins.Add(user);
                //context.SaveChanges();

                adminId1 = user.Id;


            }

            var admin = new Admin
            {
                FirstName = "Wesley",
                LastName = "Yawn",
                Email = "wes@test.com",
                ApplicationId = context.Users.FindFirstValue(ClaimTypes.NameIdentifier)
        };
            context.Admins.Add(admin);

            context.Customers.Add(
                new Customer { FirstName = "Sargeras", LastName = "Demon",StreetAddress="123 ApplePie St",City ="Austin", State = "Tx", ZipCode=78723, PhoneNumber=1234567890, Filters="sizes 20x20 7",NumberOfSystems = 4 });
            context.Customers.Add(
                new Customer { FirstName = "Jaina", LastName = "Proudmoore", StreetAddress = "123 PeachPie St", City = "Austin", State = "Tx", ZipCode = 78723, PhoneNumber = 0987654321, Filters = "sizes 20x20 7", NumberOfSystems = 3 });
            context.Customers.Add(
                new Customer { FirstName = "C'Thune", LastName = "SeaMonster", StreetAddress = "123 OrangePie St", City = "Austin", State = "Tx", ZipCode = 78723, PhoneNumber = 1231234567, Filters = "sizes 30x30 7", NumberOfSystems = 1 });
            context.Customers.Add(
                new Customer { FirstName = "Maiev", LastName = "Shadowsong", StreetAddress = "123 CherryPie St", City = "Austin", State = "Tx", ZipCode = 78723, PhoneNumber = 4564567890, Filters = "sizes 36x36 7", NumberOfSystems = 1 });
            context.Customers.Add(
                new Customer { FirstName = "Kael'thas", LastName = "Sunstrider", StreetAddress = "123 ChocolatePie St", City = "Austin", State = "Tx", ZipCode = 78723, PhoneNumber = 0980988765, Filters = "sizes 6x20 7", NumberOfSystems = 8 });
            context.Customers.Add(
                new Customer { FirstName = "Lord", LastName = "Serpentis", StreetAddress = "123 CobblerPie St", City = "Austin", State = "Tx", ZipCode = 78723, PhoneNumber = 5675674587, Filters = "sizes 18x20 7", NumberOfSystems = 5 });
            context.Customers.Add(
                new Customer { FirstName = "Illidan", LastName = "Stormrage", StreetAddress = "123 CreamPielol St", City = "Austin", State = "Tx", ZipCode = 78723, PhoneNumber = 1235437645, Filters = "sizes 23x23 7", NumberOfSystems = 3 });
            context.Customers.Add(
                new Customer { FirstName = "Grom", LastName = "Hellscream", StreetAddress = "123 BananaPie St", City = "Austin", State = "Tx", ZipCode = 78723, PhoneNumber = 3744856079, Filters = "sizes 25x25 7", NumberOfSystems = 2 });
            context.Customers.Add(
                new Customer { FirstName = "Mankirk", LastName = "ThirdLeg", StreetAddress = "123 Someothertypeofpie St", City = "Austin", State = "Tx", ZipCode = 78723, PhoneNumber = 7854756576, Filters = "sizes 20x20 7", NumberOfSystems = 9 });
            context.Customers.Add(
                new Customer { FirstName = "Lich", LastName = "King", StreetAddress = "123 AnotherPie St", City = "Austin", State = "Tx", ZipCode = 78723, PhoneNumber = 8765763645, Filters = "sizes 16x16 7", NumberOfSystems = 10 });
            context.SaveChanges();
        }
    }
}
