using System;
using System.Collections.Generic;
using IdentityTraning.Context;
using System.Data.Entity.Migrations;
using System.Linq;
using IdentityTraning.Models.DbModel;
using IdentityTraning.Repositories;
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

            UnitOfWork unitOfWork = new UnitOfWork(new ApplicationContext());

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


            for (int i = 0; i < 200; i++)
            {
                var appUser = new ApplicationUser
                {
                    UserName = $"mail{i}@mail.com",
                    Email = $"mail{i}@mail.com",
                    EmailConfirmed = true,
                    FirstName = $"User{i}",
                    LastName = $"User{i}",
                    SecondName = $"User{i}",
                    Location = "Belarus Minsk",
                    Gendar = Gendar.Man,
                    PhoneNumber = null,
                    JoinDate = DateTime.Now
                };
                userManager.Create(appUser, $"Witcher{i}");
            }

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole {Name = "Admin"});
                roleManager.Create(new IdentityRole {Name = "User"});
                roleManager.Create(new IdentityRole {Name = "Worker"});
                roleManager.Create(new IdentityRole {Name = "Customer"});
                roleManager.Create(new IdentityRole {Name = "Director"});
            }

            var adminUser = userManager.FindByName("003ovavos@gmail.com");
            userManager.AddToRoles(adminUser.Id, "Admin", "User", "Worker", "Customer", "Director");

            for (int i = 0; i < 160; i++)
            {
                var user = userManager.FindByName($"mail{i}@mail.com");
                userManager.AddToRoles(user.Id, "User", "Customer");
            }
            for (int i = 150; i < 200; i++)
            {
                var user = userManager.FindByName($"mail{i}@mail.com");
                userManager.AddToRoles(user.Id, "User", "Worker");
            }


            for (int i = 0; i < 10; i++)
            {
                var shop = new Shop
                {
                    Id = new Guid().ToString(),
                    Adress = $"Adress{i}",
                    ImgLink = @"https://image.flaticon.com/icons/png/512/69/69524.png",
                    Information = $"Some information for shop{i}",
                    Checks = new List<Check>(),
                    Positions = new List<Position>(),
                    ShopProducts = new List<ShopProduct>(),
                    Workers = new List<Worker>()
                };
                unitOfWork.ShopRepository.Create(shop);
            }

            for (int i = 0; i < 160; i++)
            {
                var customer = new Customer
                {
                    Id = new Guid().ToString(),
                    User = userManager.FindByName($"mail{i}@mail.com"),
                    Checks = new List<Check>()
                };
                unitOfWork.CustomerRepository.Create(customer);
            }

            for (int i = 0; i < 10; i++)
            {
                var pisition = new Position
                {
                    Id = new Guid().ToString(),
                    Name = $"Some PositionName{i}",
                    Duties = $"Some Duties {i}",
                    Workers = new List<Worker>(),
                    Shops = new List<Shop>()
                };
                unitOfWork.PositionRepository.Create(pisition);
            }

            for (int i = 0; i < 300; i++)
            {
                var product = new Product
                {
                    Id = new Guid().ToString(),
                    Name = $"Some ProductName{i}",
                    Information = $"Some information for product {i}",
                    ImgLink = @"https://www.freeiconspng.com/uploads/production-icon-20.png",
                    Checks = new List<Check>(),
                    ShopProducts = new List<ShopProduct>()
                };
                unitOfWork.ProductRepository.Create(product);
            }

            for (int i = 150; i < 200; i++)
            {
                var worker = new Worker
                {
                    Id = new Guid().ToString(),
                    Information = $"Some information for worker{i}",
                    ImgLink = @"https://png.pngtree.com/svg/20160822/7fd62f769e.png",
                    Shop = unitOfWork.ShopRepository.GetAll().Result[new Random().Next(0, 10)],
                    User = userManager.FindByName($"mail{i}@mail.com"),
                    Position = unitOfWork.PositionRepository.GetAll().Result[new Random().Next(0, 10)]
                };
                unitOfWork.WorkerRepository.Create(worker);
            }

            for (int i = 0; i < 200; i++)
            {
                var check = new Check
                {
                    Id = new Guid().ToString(),
                    PurchaseDate = DateTime.Now,
                    Shop = unitOfWork.ShopRepository.GetAll().Result[new Random().Next(0, 10)],
                    Customer = unitOfWork.CustomerRepository.GetAll().Result[new Random().Next(0, 160)],
                    Products = new List<Product> {unitOfWork.ProductRepository.GetAll().Result[i]}
                };
                unitOfWork.CheckRepository.Create(check);
            }

            for (int i = 0; i < 300; i++)
            {
                var shopProduct = new ShopProduct
                {
                    Id = new Guid().ToString(),
                    Price = new Random().Next(10, 200),
                    Count = new Random().Next(2, 50),
                    Product = unitOfWork.ProductRepository.GetAll().Result[i],
                    Shop = unitOfWork.ShopRepository.GetAll().Result[new Random().Next(0, 10)]
                };
                unitOfWork.ShopProductRepository.Create(shopProduct);
            }
        }
    }
}
