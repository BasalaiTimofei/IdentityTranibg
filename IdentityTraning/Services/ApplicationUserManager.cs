using System;
using IdentityTraning.Context;
using IdentityTraning.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;

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
                new ApplicationUserManager(new UserStore<ApplicationUser>(applicationContext))
                {
                    EmailService = new EmailService(),
                    PasswordValidator = new PasswordValidator
                    {
                        RequiredLength = 6,
                        RequireNonLetterOrDigit = true,
                        RequireDigit = false,
                        RequireLowercase = true,
                        RequireUppercase = true
                    },

                };


            IDataProtectionProvider dataProtectionProvider = identityFactoryOptions.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                applicationUserManager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"))
                    {
                        TokenLifespan = TimeSpan.FromHours(6)
                    };
            }




            return applicationUserManager;
        }
    }
}