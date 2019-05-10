namespace IdentityTraning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Step4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Checks", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Checks", new[] { "Customer_Id" });
            DropColumn("dbo.Customers", "Id");
            RenameColumn(table: "dbo.Customers", name: "CustomerId", newName: "Id");
            RenameIndex(table: "dbo.Customers", name: "IX_CustomerId", newName: "IX_Id");
            DropColumn("dbo.Checks", "Customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Checks", "Customer_Id", c => c.String(nullable: false, maxLength: 128));
            RenameIndex(table: "dbo.Customers", name: "IX_Id", newName: "IX_CustomerId");
            RenameColumn(table: "dbo.Customers", name: "Id", newName: "CustomerId");
            AddColumn("dbo.Customers", "Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Checks", "Customer_Id");
            AddForeignKey("dbo.Checks", "Customer_Id", "dbo.Customers", "Id", cascadeDelete: true);
        }
    }
}
