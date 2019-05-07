using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Web.Http.Routing;
using IdentityTraning.Models.DbModel;
using IdentityTraning.Models.ViewModel;
using IdentityTraning.Services;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityTraning.Models
{
    public class ModelFactory
    {
        private readonly UrlHelper _urlHelper;
        private readonly ApplicationUserManager _applicationUserManager;

        public ModelFactory(HttpRequestMessage httpRequestMessage, 
            ApplicationUserManager applicationUserManager)
        {
            _urlHelper = new UrlHelper(httpRequestMessage);
            _applicationUserManager = applicationUserManager;
        }

        public UserReturnModel Create(ApplicationUser applicationUser)
        {
            return new UserReturnModel
            {
                Url = _urlHelper.Link("GetUserById", new {id = applicationUser.Id}),
                Id = applicationUser.Id,
                UserName = applicationUser.UserName,
                FullName = $"{applicationUser.LastName} {applicationUser.FirstName} {applicationUser.SecondName}",
                Email = applicationUser.Email,
                EmailConfirmed = applicationUser.EmailConfirmed,
                Gendar = applicationUser.Gendar.ToString(),
                Location = applicationUser.Location,
                JoinDate = applicationUser.JoinDate,
                PhoneNumber = applicationUser.PhoneNumber,
                Roles = _applicationUserManager.GetRolesAsync(applicationUser.Id).Result,
                Claims = _applicationUserManager.GetClaimsAsync(applicationUser.Id).Result
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

        public CheckReturnModel Create(Check check)
        {
            var products = new List<string[]>();
            foreach (var checkProduct in check.Products)
                products.Add(new []
                {
                    checkProduct.Id,
                    checkProduct.Name,
                    checkProduct.Cost.ToString(CultureInfo.InvariantCulture)
                });

            return new CheckReturnModel
            {
                Url = _urlHelper.Link("GetCheckById", new {id = check.Id}),
                Id = check.Id,
                Products = products,
                Customer = ShortUserModel.Create(check.Customer.User),
                Adress = check.Shop.Adress,
                PurchaseDate = check.PurchaseDate
            };
        }

        public CustomerReturnModel Create(Customer customer)
        {
            var checks = new List<string[]>();
            foreach (var customerCheck in customer.Checks)
                checks.Add(new[] 
                {
                    customerCheck.Id,
                    customerCheck.PurchaseDate.ToString(CultureInfo.InvariantCulture)
                });

            return new CustomerReturnModel
            {
                Url = _urlHelper.Link("GetCustomerById", new {id = customer.Id}),
                Id = customer.Id,
                User = ShortUserModel.Create(customer.User),
                Check = checks
            };
        }

        public PositionReturnModel Create(Position position)
        {
            var workers = new List<ShortUserModel>();
            foreach (var positionWorker in position.Workers)
               workers.Add(ShortUserModel.Create(positionWorker.User));

            return new PositionReturnModel
            {
                Url = _urlHelper.Link("GetPositionById", new {id = position.Id}),
                Id = position.Id,
                PositionName = position.Name,
                Duties = position.Duties,
                Workers = workers
            };
        }

        public ProductReturnModel Create(Product product)
        {
            return new ProductReturnModel
            {
                Url = _urlHelper.Link("GetProductById", new {id = product.Id}),
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost.ToString(CultureInfo.InvariantCulture),
                ImgLink = product.ImgLink,
                Information = product.Information
            };
        }

        public ShopReturnModel Create(Shop shop)
        {
            return new ShopReturnModel
            {
                Url = _urlHelper.Link("GetShopById", new {id = shop.Id}),
                Id = shop.Id,
                Adress = shop.Adress,
                Information = shop.Information,
                ImgLink = shop.ImgLink
            };
        }

        public WorkerReturnModel Create(Worker worker)
        {
            return new WorkerReturnModel
            {
                Url = _urlHelper.Link("GetWorkerById", new {id = worker.Id}),
                Id = worker.Id,
                ImgLink = worker.ImgLink,
                Information = worker.Information,
                Position = worker.Position.Name,
                User = ShortUserModel.Create(worker.User)
            };
        }
    }
}