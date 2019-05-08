using System.Data.Entity.ModelConfiguration;
using IdentityTraning.Models.DbModel;

namespace IdentityTraning.Context
{
    public class ApplicationUserConfiguration : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfiguration()
        {
            HasKey(h => h.Id);

            HasOptional(h => h.Customer)
                .WithRequired(w => w.User);
            HasOptional(w => w.Worker)
                .WithRequired(w => w.User);

            Property(p => p.FirstName).IsRequired().HasMaxLength(50);
            Property(p => p.LastName).IsRequired().HasMaxLength(50);
            Property(p => p.SecondName).IsRequired().HasMaxLength(50);
            Property(p => p.Location).IsRequired().HasMaxLength(100);
            Property(p => p.Gendar).IsRequired();
            Property(p => p.JoinDate).IsRequired();
        }
    }

    public class CheckConfiguration : EntityTypeConfiguration<Check>
    {
        public CheckConfiguration()
        {
            HasKey(h => h.Id);

            HasMany(h => h.Products)
                .WithMany(w => w.Checks);
            HasRequired(h => h.Customer)
                .WithMany(w => w.Checks);
            HasRequired(h => h.Shop)
                .WithMany(w => w.Checks);

            Property(p => p.PurchaseDate).IsRequired();
        }
    }

    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            HasKey(h => h.Id);

            HasRequired(h => h.User)
                .WithOptional(w => w.Customer);
            HasMany(h => h.Checks)
                .WithRequired(w => w.Customer);
        }
    }

    public class PositionConfiguration : EntityTypeConfiguration<Position>
    {
        public PositionConfiguration()
        {
            HasKey(h => h.Id);

            HasMany(h => h.Workers)
                .WithRequired(w => w.Position);
            HasMany(h => h.Shops)
                .WithMany(w => w.Positions);

            Property(p => p.Name).IsRequired().HasMaxLength(50);
            Property(p => p.Duties).IsRequired().HasMaxLength(500);
        }
    }

    public class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            HasKey(h => h.Id);

            HasMany(h => h.Checks)
                .WithMany(w => w.Products);
            HasMany(h => h.Shops)
                .WithMany(w => w.Products);

            Property(p => p.Name).IsRequired().HasMaxLength(50);
            Property(p => p.Cost).IsRequired();
            Property(p => p.Information).IsRequired().HasMaxLength(500);
            Property(p => p.ImgLink).IsRequired();
        }
    }

    public class ShopConfiguration : EntityTypeConfiguration<Shop>
    {
        public ShopConfiguration()
        {
            HasKey(h => h.Id);

            HasMany(h => h.Checks)
                .WithRequired(w => w.Shop);
            HasMany(h => h.Positions)
                .WithMany(w => w.Shops);
            HasMany(h => h.Products)
                .WithMany(w => w.Shops);
            HasMany(h => h.Workers)
                .WithRequired(w => w.Shop);

            Property(p => p.Adress).IsRequired().HasMaxLength(100);
            Property(p => p.Information).IsRequired().HasMaxLength(500);
            Property(p => p.ImgLink).IsRequired();
        }
    }

    public class WorkerConfiguration : EntityTypeConfiguration<Worker>
    {
        public WorkerConfiguration()
        {
            HasKey(h => h.Id);

            HasRequired(h => h.Position)
                .WithMany(w => w.Workers);
            HasRequired(h => h.User)
                .WithOptional(w => w.Worker);
            HasRequired(h => h.Shop)
                .WithMany(s => s.Workers);

            Property(p => p.ImgLink).IsRequired();
            Property(p => p.Information).IsRequired().HasMaxLength(500);
        }
    }
}