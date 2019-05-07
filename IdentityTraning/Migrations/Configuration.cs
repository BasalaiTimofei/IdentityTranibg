using System;
using IdentityTraning.Context;
using System.Data.Entity.Migrations;
using System.Linq;
using IdentityTraning.Models;
using IdentityTraning.Models.DbModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace IdentityTraning.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationContext applicationContext)
        {
            UserManager<ApplicationUser> userManager =
                new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationContext()));

            RoleManager<IdentityRole> roleManager =
                new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationContext()));

            ApplicationUser applicationUser = new ApplicationUser
            {
                UserName = "003ovavos@gmail.com",
                Email = "003ovavos@gmail.com",
                EmailConfirmed = true,
                FirstName = "Тимофей",
                LastName = "Басалай",
                SecondName = "Геннадьевич",
                Location = "Belarus Minsk",
                Gendar = Gendar.Man,
                PhoneNumber = "+375295353868",
                JoinDate = DateTime.Now
            };

            userManager.Create(applicationUser, "Witcher3");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole {Name = "Admin"});
                roleManager.Create(new IdentityRole {Name = "User"});
            }

            var adminUser = userManager.FindByName("003ovavos@gmail.com");

            userManager.AddToRole(adminUser.Id, "Admin");
        }
    }
}
