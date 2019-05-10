using System.Collections.Generic;
using IdentityTraning.Context;
using IdentityTraning.Models.DbModel;
using IdentityTraning.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityTraning.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<IdentityTraning.Context.ApplicationContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override async void Seed(ApplicationContext applicationContext)
        {
            UserManager<ApplicationUser> userManager =
                new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new IdentityContext()));

            RoleManager<IdentityRole> roleManager =
                new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new IdentityContext()));

            UnitOfWork unitOfWork = new UnitOfWork(ApplicationContext.Create());


            //for (int i = 0; i < 200; i++)
            //{
            //    var appUser = new ApplicationUser
            //    {
            //        UserName = $"mail{i}@mail.com",
            //        Email = $"mail{i}@mail.com",
            //        EmailConfirmed = true,
            //        PhoneNumber = null
            //    };
            //    userManager.Create(appUser, $"Witcher{i}");
            //    var identityUser = userManager.FindByName(appUser.UserName);
            //    var user = new User
            //    {
            //        Id = identityUser.Id,
            //        UserName = appUser.UserName,
            //        FirstName = $"User{i}",
            //        LastName = $"User{i}",
            //        SecondName = $"User{i}",
            //        Location = "Belarus Minsk",
            //        Gendar = Gendar.Man,
            //    };
            //    unitOfWork.UserRepository.Create(user);
            //}
            //if (!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //    roleManager.Create(new IdentityRole { Name = "Worker" });
            //    roleManager.Create(new IdentityRole { Name = "Customer" });
            //    roleManager.Create(new IdentityRole { Name = "Director" });
            //}
            //for (int i = 1; i < 160; i++)
            //{
            //    var user = userManager.FindByName($"mail{i}@mail.com");
            //    userManager.AddToRoles(user.Id, "User", "Customer");
            //}
            //for (int i = 150; i < 200; i++)
            //{
            //    var user = userManager.FindByName($"mail{i}@mail.com");
            //    userManager.AddToRoles(user.Id, "User", "Worker");
            //}


            //for (int i = 0; i < 10; i++)
            //{
            //    var shop = new Shop
            //    {
            //        Adress = $"Adress{i}",
            //        ImgLink = @"https://image.flaticon.com/icons/png/512/69/69524.png",
            //        Information = $"Some information for shop{i}",
            //    };
            //    unitOfWork.ShopRepository.Create(shop);
            //}
            //await unitOfWork.Save();


            //for (int i = 0; i < 10; i++)
            //{
            //    var pisition = new Position
            //    {
            //        Name = $"Some PositionName{i}",
            //        Duties = $"Some Duties {i}",
            //    };
            //    unitOfWork.PositionRepository.Create(pisition);
            //}
            //await unitOfWork.Save();


            //for (int i = 0; i < 300; i++)
            //{
            //    var product = new Product
            //    {
            //        Name = $"Some ProductName{i}",
            //        Information = $"Some information for product {i}",
            //        ImgLink = @"https://www.freeiconspng.com/uploads/production-icon-20.png",
            //    };
            //    unitOfWork.ProductRepository.Create(product);
            //}
            //await unitOfWork.Save();


            //for (int i = 0; i < 300; i++)
            //{
            //    var shopProduct = new ShopProduct
            //    {
            //        Price = new Random().Next(10, 200),
            //        Count = new Random().Next(2, 50),
            //        Product = unitOfWork.ProductRepository.GetAll().Result[i],
            //        Shop = unitOfWork.ShopRepository.GetAll().Result[new Random().Next(0, 10)]
            //    };
            //    unitOfWork.ShopProductRepository.Create(shopProduct);
            //}
            //await unitOfWork.Save();


            //ApplicationUser applicationUser = new ApplicationUser
            //{
            //    UserName = "003ovavos@gmail.com",
            //    Email = "003ovavos@gmail.com",
            //    EmailConfirmed = true,
            //    PhoneNumber = "375295353868",
            //};
            //userManager.Create(applicationUser, "Witcher3");
            //var adminUser = userManager.FindByName("003ovavos@gmail.com");
            //var adUser = new User
            //{
            //    Id = adminUser.Id,
            //    UserName = adminUser.UserName,
            //    FirstName = "???????",
            //    LastName = "???????",
            //    SecondName = "???????????",
            //    Location = "Belarus Minsk",
            //    Gendar = Gendar.Man
            //};
            //unitOfWork.UserRepository.Create(adUser);
            //userManager.AddToRoles(adminUser.Id, "Admin", "User", "Worker", "Customer", "Director");


            ////TODO !!!!!
            for (int i = 0; i < 160; i++)
            {
                var customer = new Customer
                {
                    User = unitOfWork.UserRepository.GetByName($"mail{i}@mail.com").Result,
                };
                unitOfWork.CustomerRepository.Create(customer);
            }
            await unitOfWork.Save();


            //    for (int i = 150; i < 200; i++)
            //    {
            //        var worker = new Worker
            //        {
            //            Information = $"Some information for worker{i}",
            //            ImgLink = @"https://png.pngtree.com/svg/20160822/7fd62f769e.png",
            //            Shop = unitOfWork.ShopRepository.GetAll().Result[new Random().Next(0, 10)],
            //            User = unitOfWork.UserRepository.GetByName($"mail{i}@mail.com").Result,
            //            Position = unitOfWork.PositionRepository.GetAll().Result[new Random().Next(0, 10)]
            //        };
            //        unitOfWork.WorkerRepository.Create(worker);
            //    }
            //    await unitOfWork.Save();


            //    for (int i = 0; i < 200; i++)
            //    {
            //        var check = new Check
            //        {
            //            PurchaseDate = DateTime.Now,
            //            Shop = unitOfWork.ShopRepository.GetAll().Result[new Random().Next(0, 10)],
            //            Customer = unitOfWork.CustomerRepository.GetAll().Result[new Random().Next(0, 160)],
            //            Products = new List<Product> { unitOfWork.ProductRepository.GetAll().Result[i] }
            //        };
            //        unitOfWork.CheckRepository.Create(check);
            //    }
            //    await unitOfWork.Save();
        }

    }
}