using System.Data.Entity;
using IdentityTraning.Models.DbModel;

namespace IdentityTraning.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Check> Checks { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<ShopProduct> ShopProducts { get; set; }

        public ApplicationContext()
            : base("name=ApplicationContext")
        {
            //Database.SetInitializer(new ApplicationDbInitializer());
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public static ApplicationContext Create()
        {
            return new ApplicationContext();
        }

        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Configurations.Add(new UserConfiguration());
            dbModelBuilder.Configurations.Add(new CheckConfiguration());
            dbModelBuilder.Configurations.Add(new CustomerConfiguration());
            dbModelBuilder.Configurations.Add(new PositionConfiguration());
            dbModelBuilder.Configurations.Add(new ProductConfiguration());
            dbModelBuilder.Configurations.Add(new ShopConfiguration());
            dbModelBuilder.Configurations.Add(new ShopProductConfiguration());
            dbModelBuilder.Configurations.Add(new WorkerConfiguration());
        }
    }

    //public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationContext>
    //{
    //    protected override void Seed(ApplicationContext applicationContext)
    //    {
    //        applicationContext.Database.ExecuteSqlCommand(
    //            "ALTER TABLE dbo.User ADD CONSTRAINT uc_Customer UNIQUE(CustomerId)");
    //    }
    //}
}