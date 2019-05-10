namespace IdentityTraning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Step2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "Id", "dbo.Users");
            DropIndex("dbo.Customers", new[] { "Id" });
            AddColumn("dbo.Customers", "CustomerId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Customers", "CustomerId");
            AddForeignKey("dbo.Customers", "CustomerId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "CustomerId", "dbo.Users");
            DropIndex("dbo.Customers", new[] { "CustomerId" });
            DropColumn("dbo.Customers", "CustomerId");
            CreateIndex("dbo.Customers", "Id");
            AddForeignKey("dbo.Customers", "Id", "dbo.Users", "Id");
        }
    }
}
