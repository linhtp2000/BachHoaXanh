namespace BachHoaXanh.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelup : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bills", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.User_Authorize", "Authorize_Id", "dbo.Authorizes");
            DropIndex("dbo.Bills", new[] { "EmployeeId" });
            DropIndex("dbo.User_Authorize", new[] { "Authorize_Id" });
            RenameColumn(table: "dbo.Branches", name: "Manage_Id", newName: "Employee_Id");
            RenameColumn(table: "dbo.User_Authorize", name: "Authorize_Id", newName: "AuthorizeId");
            RenameIndex(table: "dbo.Branches", name: "IX_Manage_Id", newName: "IX_Employee_Id");
            DropPrimaryKey("dbo.User_Authorize");
            AddColumn("dbo.Branches", "ManagerId", c => c.String(maxLength: 255));
            AddColumn("dbo.Carts", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Carts", "Image", c => c.String());
            AddColumn("dbo.Bills", "CustomerName", c => c.String(maxLength: 255));
            AddColumn("dbo.Bills", "City", c => c.String());
            AddColumn("dbo.Bills", "Address", c => c.String());
            AddColumn("dbo.Bills", "ServiceCharge", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Bills", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DetailsOfBills", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DetailsOfBills", "Total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Products", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Products", "Image1", c => c.String());
            AlterColumn("dbo.Products", "Image2", c => c.String());
            AlterColumn("dbo.Products", "Image3", c => c.String());
            AlterColumn("dbo.Carts", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.User_Authorize", "AuthorizeId", c => c.String(nullable: false, maxLength: 255));
            AddPrimaryKey("dbo.User_Authorize", new[] { "UserId", "AuthorizeId" });
            CreateIndex("dbo.User_Authorize", "AuthorizeId");
            AddForeignKey("dbo.User_Authorize", "AuthorizeId", "dbo.Authorizes", "Id", cascadeDelete: true);
            DropColumn("dbo.Bills", "EmployeeId");
            DropColumn("dbo.Bills", "Price");
            DropColumn("dbo.User_Authorize", "AuthId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User_Authorize", "AuthId", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Bills", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.Bills", "EmployeeId", c => c.String(maxLength: 255));
            DropForeignKey("dbo.User_Authorize", "AuthorizeId", "dbo.Authorizes");
            DropIndex("dbo.User_Authorize", new[] { "AuthorizeId" });
            DropPrimaryKey("dbo.User_Authorize");
            AlterColumn("dbo.User_Authorize", "AuthorizeId", c => c.String(maxLength: 255));
            AlterColumn("dbo.Carts", "Price", c => c.Double(nullable: false));
            AlterColumn("dbo.Products", "Image3", c => c.String(maxLength: 255));
            AlterColumn("dbo.Products", "Image2", c => c.String(maxLength: 255));
            AlterColumn("dbo.Products", "Image1", c => c.String(maxLength: 255));
            AlterColumn("dbo.Products", "Price", c => c.Double(nullable: false));
            DropColumn("dbo.DetailsOfBills", "Total");
            DropColumn("dbo.DetailsOfBills", "Price");
            DropColumn("dbo.Bills", "Total");
            DropColumn("dbo.Bills", "ServiceCharge");
            DropColumn("dbo.Bills", "Address");
            DropColumn("dbo.Bills", "City");
            DropColumn("dbo.Bills", "CustomerName");
            DropColumn("dbo.Carts", "Image");
            DropColumn("dbo.Carts", "Total");
            DropColumn("dbo.Branches", "ManagerId");
            AddPrimaryKey("dbo.User_Authorize", new[] { "UserId", "AuthId" });
            RenameIndex(table: "dbo.Branches", name: "IX_Employee_Id", newName: "IX_Manage_Id");
            RenameColumn(table: "dbo.User_Authorize", name: "AuthorizeId", newName: "Authorize_Id");
            RenameColumn(table: "dbo.Branches", name: "Employee_Id", newName: "Manage_Id");
            CreateIndex("dbo.User_Authorize", "Authorize_Id");
            CreateIndex("dbo.Bills", "EmployeeId");
            AddForeignKey("dbo.User_Authorize", "Authorize_Id", "dbo.Authorizes", "Id");
            AddForeignKey("dbo.Bills", "EmployeeId", "dbo.Employees", "Id");
        }
    }
}
