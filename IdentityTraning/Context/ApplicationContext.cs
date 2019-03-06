using IdentityTraning.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityTraning.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext()
            : base("name=ApplicationContext", throwIfV1Schema:false)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }
    }
}