using System.Data.Entity;
using IdentityTraning.Models;
using IdentityTraning.Models.DbModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace IdentityTraning.Context
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Check> Checks { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Position> Positions { get; set; }

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