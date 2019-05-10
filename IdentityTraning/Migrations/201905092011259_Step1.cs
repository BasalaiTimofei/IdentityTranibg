namespace IdentityTraning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Step1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Checks",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PurchaseDate = c.DateTime(nullable: false),
                        Shop_Id = c.String(nullable: false, maxLength: 128),
                        Customer_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shops", t => t.Shop_Id, cascadeDelete: true)
                .ForeignKey("dbo.Customers", t => t.Customer_Id, cascadeDelete: true)
                .Index(t => t.Shop_Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        SecondName = c.String(nullable: false, maxLength: 50),
                        Gendar = c.Int(nullable: false),
                        Location = c.String(nullable: false, maxLength: 100),
                        JoinDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Workers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Information = c.String(nullable: false, maxLength: 500),
                        ImgLink = c.String(nullable: false),
                        Position_Id = c.String(nullable: false, maxLength: 128),
                        Shop_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Positions", t => t.Position_Id, cascadeDelete: true)
                .ForeignKey("dbo.Shops", t => t.Shop_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Position_Id)
                .Index(t => t.Shop_Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                        Duties = c.String(nullable: false, maxLength: 500),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Adress = c.String(nullable: false, maxLength: 100),
                        Information = c.String(nullable: false, maxLength: 500),
                        ImgLink = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShopProducts",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Count = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Product_Id = c.String(nullable: false, maxLength: 128),
                        Shop_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .ForeignKey("dbo.Shops", t => t.Shop_Id, cascadeDelete: true)
                .Index(t => t.Product_Id)
                .Index(t => t.Shop_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 50),
                        Information = c.String(nullable: false, maxLength: 500),
                        ImgLink = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CheckProducts",
                c => new
                    {
                        Check_Id = c.String(nullable: false, maxLength: 128),
                        Product_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Check_Id, t.Product_Id })
                .ForeignKey("dbo.Checks", t => t.Check_Id, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.Product_Id, cascadeDelete: true)
                .Index(t => t.Check_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CheckProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.CheckProducts", "Check_Id", "dbo.Checks");
            DropForeignKey("dbo.Checks", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Customers", "Id", "dbo.Users");
            DropForeignKey("dbo.Workers", "Id", "dbo.Users");
            DropForeignKey("dbo.Workers", "Shop_Id", "dbo.Shops");
            DropForeignKey("dbo.ShopProducts", "Shop_Id", "dbo.Shops");
            DropForeignKey("dbo.ShopProducts", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Checks", "Shop_Id", "dbo.Shops");
            DropForeignKey("dbo.Workers", "Position_Id", "dbo.Positions");
            DropIndex("dbo.CheckProducts", new[] { "Product_Id" });
            DropIndex("dbo.CheckProducts", new[] { "Check_Id" });
            DropIndex("dbo.ShopProducts", new[] { "Shop_Id" });
            DropIndex("dbo.ShopProducts", new[] { "Product_Id" });
            DropIndex("dbo.Workers", new[] { "Shop_Id" });
            DropIndex("dbo.Workers", new[] { "Position_Id" });
            DropIndex("dbo.Workers", new[] { "Id" });
            DropIndex("dbo.Customers", new[] { "Id" });
            DropIndex("dbo.Checks", new[] { "Customer_Id" });
            DropIndex("dbo.Checks", new[] { "Shop_Id" });
            DropTable("dbo.CheckProducts");
            DropTable("dbo.Products");
            DropTable("dbo.ShopProducts");
            DropTable("dbo.Shops");
            DropTable("dbo.Positions");
            DropTable("dbo.Workers");
            DropTable("dbo.Users");
            DropTable("dbo.Customers");
            DropTable("dbo.Checks");
        }
    }
}
