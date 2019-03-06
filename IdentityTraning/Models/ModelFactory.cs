using System.Net.Http;
using System.Web.Http.Routing;
using IdentityTraning.Services;

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
    }
}