using IdentityTraning.Context;
using IdentityTraning.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace IdentityTraning.Services
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> userStore) 
            : base(userStore)
        {
        }

        public static ApplicationUserManager Create(
            IdentityFactoryOptions<ApplicationUserManager> identityFactoryOptions, IOwinContext owinContext)
        {
            ApplicationContext applicationContext = owinContext.Get<ApplicationContext>();
            ApplicationUserManager applicationUserManager =
                new ApplicationUserManager(new UserStore<ApplicationUser>(applicationContext));

            return applicationUserManager;
        }
    }
}