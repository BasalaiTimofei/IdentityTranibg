using IdentityTraning.Models.DbModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityTraning.Context
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext()
            : base("name=IdentityContext", throwIfV1Schema:false)
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }

        //protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        //{
        //    dbModelBuilder.Configurations.Add(new ApplicationUserConfiguration());

        //    dbModelBuilder.Entity<IdentityUserRole>().HasKey(h => new {h.UserId, h.RoleId});
        //    dbModelBuilder.Entity<IdentityUserLogin>().HasKey(h => new {h.UserId});
        //}
    }
}