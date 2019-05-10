using IdentityTraning.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace IdentityTraning.Services
{
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> store) 
            : base(store)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options,
            IOwinContext context)
        {
            var applicationRoleManager =
                new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<IdentityContext>()));

            return applicationRoleManager;
        }
    }
}