using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Web.Http.Routing;
using IdentityTraning.Models.DbModel;
using IdentityTraning.Models.ViewModel;
using IdentityTraning.Repositories;
using IdentityTraning.Services;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityTraning.Models
{
    public class ModelFactory
    {
        private readonly UrlHelper _urlHelper;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly UnitOfWork _unitOfWork;

        public ModelFactory(HttpRequestMessage httpRequestMessage, 
            ApplicationUserManager applicationUserManager, UnitOfWork unitOfWork)
        {
            _urlHelper = new UrlHelper(httpRequestMessage);
            _applicationUserManager = applicationUserManager;
            _unitOfWork = unitOfWork;
        }

        public UserReturnModel Create(ApplicationUser identityUser)
        {
            var user = _unitOfWork.UserRepository.GetById(identityUser.Id).Result;

            return new UserReturnModel
            {
                Url = _urlHelper.Link("GetUserById", new {id = identityUser.Id}),
                Id = identityUser.Id,
                UserName = identityUser.UserName,
                FullName = $"{user.LastName} {user.FirstName} {user.SecondName}",
                Email = identityUser.Email,
                EmailConfirmed = identityUser.EmailConfirmed,
                Gendar = user.Gendar.ToString(),
                Location = user.Location,
                JoinDate = user.JoinDate,
                PhoneNumber = identityUser.PhoneNumber,
                Roles = _applicationUserManager.GetRolesAsync(identityUser.Id).Result,
                Claims = _applicationUserManager.GetClaimsAsync(identityUser.Id).Result
            };
        }

        public RoleReturnModel Create(IdentityRole identityRole)
        {
            return new RoleReturnModel
            {
                Url = _urlHelper.Link("GetRoleById", new {id = identityRole.Id}),
                Id = identityRole.Id,
                Name = identityRole.Name
            };
        }

        //public CheckReturnModel Create(Check check)
        //{
        //    var products = new List<string[]>();
        //    decimal fullPrice = 0;
        //    foreach (var checkProduct in check.Products)
        //    {
        //        products.Add(new[]
        //        {
        //            checkProduct.Id,
        //            checkProduct.Name,
        //            _unitOfWork.ShopProductRepository.GetById(check.)
        //            //checkProduct.ShopProducts
        //            //    .Where(w => w.Shop.Adress == check.Shop.Adress
        //            //                && w.Product.Id == checkProduct.Id)
        //            //    .Select(s => s.Price).First().ToString(CultureInfo.InvariantCulture)
        //        });
        //        fullPrice += checkProduct.ShopProducts
        //            .Where(w => w.Shop.Adress == check.Shop.Adress
        //                        && w.Product.Id == checkProduct.Id)
        //            .Select(s => s.Price).First();
        //    }

        //    return new CheckReturnModel
        //    {
        //        Url = _urlHelper.Link("GetCheckById", new {id = check.Id}),
        //        Id = check.Id,
        //        Products = products,
        //        Customer = ShortUserModel.Create(check.Customer.User),
        //        Adress = check.Shop.Adress,
        //        FullPrice = fullPrice.ToString(CultureInfo.InvariantCulture),
        //        PurchaseDate = check.PurchaseDate
        //    };
        //}

        //public CustomerReturnModel Create(Customer customer)
        //{
        //    var checks = new List<string[]>();
        //    foreach (var customerCheck in customer.Checks)
        //        checks.Add(new[] 
        //        {
        //            customerCheck.Id,
        //            customerCheck.PurchaseDate.ToString(CultureInfo.InvariantCulture)
        //        });

        //    return new CustomerReturnModel
        //    {
        //        Url = _urlHelper.Link("GetCustomerById", new {id = customer.Id}),
        //        Id = customer.Id,
        //        User = ShortUserModel.Create(customer.User),
        //        Check = checks
        //    };
        //}

        //public PositionReturnModel Create(Position position)
        //{
        //    var workers = new List<ShortUserModel>();
        //    foreach (var positionWorker in position.Workers)
        //       workers.Add(ShortUserModel.Create(positionWorker.User.));

        //    return new PositionReturnModel
        //    {
        //        Url = _urlHelper.Link("GetPositionById", new {id = position.Id}),
        //        Id = position.Id,
        //        PositionName = position.Name,
        //        Duties = position.Duties,
        //        Workers = workers
        //    };
        //}

        public ProductReturnModel Create(Product product)
        {
            return new ProductReturnModel
            {
                Url = _urlHelper.Link("GetProductById", new {id = product.Id}),
                Id = product.Id,
                Name = product.Name,
                Price = $"{product.ShopProducts.Select(s => s.Price).Min()} - " +
                        $"{product.ShopProducts.Select(s => s.Price).Max()}",
                ImgLink = product.ImgLink,
                Information = product.Information
            };
        }

        public ShopReturnModel Create(Shop shop)
        {
            return new ShopReturnModel
            {
                Url = _urlHelper.Link("GetShopById", new {id = shop.Id}),
                Id = shop.Id.ToString(),
                Adress = shop.Adress,
                Information = shop.Information,
                ImgLink = shop.ImgLink
            };
        }

        //public WorkerReturnModel Create(Worker worker)
        //{
        //    return new WorkerReturnModel
        //    {
        //        Url = _urlHelper.Link("GetWorkerById", new {id = worker.Id}),
        //        Id = worker.Id,
        //        ImgLink = worker.ImgLink,
        //        Information = worker.Information,
        //        Position = worker.Position.Name,
        //        User = ShortUserModel.Create(worker.User)
        //    };
        //}

        //public ProductInShopReturnModel Create(ShopProduct productInShop)
        //{
        //    return new ProductInShopReturnModel
        //    {
        //        Url = _urlHelper.Link("GetProductInShop", new {id = productInShop.Id}),
        //        Id = productInShop.Id,
        //        Price = productInShop.Price.ToString(CultureInfo.InvariantCulture),
        //        Count = productInShop.Count,
        //        ImgLink = productInShop.Product.ImgLink,
        //        Information = productInShop.Product.Information,
        //        ProductName = productInShop.Product.Name,
        //        Shop = new[] {productInShop.Shop.Id, productInShop.Shop.Adress}
        //    };
        //}
    }
}