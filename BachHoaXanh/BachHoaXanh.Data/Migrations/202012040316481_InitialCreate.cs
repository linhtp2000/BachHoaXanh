namespace BachHoaXanh.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255),
                        Name = c.String(maxLength: 255),
                        Address = c.String(),
                        ManagerId = c.String(maxLength: 255),
                        AreaId = c.String(maxLength: 255),
                        Employee_Id = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.AreaId)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .Index(t => t.AreaId)
                .Index(t => t.Employee_Id);
            
            CreateTable(
                "dbo.Invoices",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255),
                        Datetime = c.DateTime(nullable: false),
                        ProviderId = c.String(maxLength: 255),
                        BranhId = c.String(maxLength: 255),
                        Price = c.Double(nullable: false),
                        Branch_Id = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Branches", t => t.Branch_Id)
                .ForeignKey("dbo.Providers", t => t.ProviderId)
                .Index(t => t.ProviderId)
                .Index(t => t.Branch_Id);
            
            CreateTable(
                "dbo.DetailsOfInvoices",
                c => new
                    {
                        ProductId = c.String(nullable: false, maxLength: 255),
                        InvoiceId = c.String(nullable: false, maxLength: 255),
                        ProductName = c.String(),
                        Price = c.Double(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.InvoiceId })
                .ForeignKey("dbo.Invoices", t => t.InvoiceId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.InvoiceId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255),
                        Name = c.String(maxLength: 255),
                        Price = c.Double(nullable: false),
                        Discount = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Details = c.String(maxLength: 255),
                        Image1 = c.String(maxLength: 255),
                        Image2 = c.String(maxLength: 255),
                        Image3 = c.String(maxLength: 255),
                        ClassifyId = c.String(maxLength: 255),
                        BranchId = c.String(maxLength: 255),
                        ProviderId = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classifies", t => t.ClassifyId)
                .ForeignKey("dbo.Providers", t => t.ProviderId)
                .ForeignKey("dbo.Branches", t => t.BranchId)
                .Index(t => t.ClassifyId)
                .Index(t => t.BranchId)
                .Index(t => t.ProviderId);
            
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        ProductId = c.String(nullable: false, maxLength: 255),
                        CustomerId = c.String(nullable: false, maxLength: 255),
                        ProductName = c.String(maxLength: 255),
                        Price = c.Double(nullable: false),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.CustomerId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255),
                        Name = c.String(maxLength: 255),
                        Password = c.String(maxLength: 255),
                        PhoneNumber = c.String(),
                        Address = c.String(maxLength: 255),
                        Points = c.Int(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Bills",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255),
                        Datetime = c.DateTime(nullable: false),
                        CustomerId = c.String(maxLength: 255),
                        EmployeeId = c.String(maxLength: 255),
                        Price = c.Double(nullable: false),
                        Points = c.Int(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.CustomerId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.DetailsOfBills",
                c => new
                    {
                        ProductId = c.String(nullable: false, maxLength: 255),
                        BillId = c.String(nullable: false, maxLength: 255),
                        ProductName = c.String(maxLength: 255),
                        Image = c.String(),
                        Amount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.BillId })
                .ForeignKey("dbo.Bills", t => t.BillId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.BillId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255),
                        Name = c.String(maxLength: 255),
                        Password = c.String(maxLength: 255),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        ManagerId = c.String(maxLength: 255),
                        AdminId = c.String(maxLength: 255),
                        UserId = c.String(maxLength: 255),
                        Salary = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User_Authorize",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 255),
                        AuthorizeId = c.String(nullable: false, maxLength: 255),
                        Note = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => new { t.UserId, t.AuthorizeId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Authorizes", t => t.AuthorizeId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.AuthorizeId);
            
            CreateTable(
                "dbo.WorkAts",
                c => new
                    {
                        EmployeeId = c.String(nullable: false, maxLength: 255),
                        BranchId = c.String(nullable: false, maxLength: 255),
                        Position = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.BranchId })
                .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
                .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.BranchId);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        ProductId = c.String(nullable: false, maxLength: 255),
                        CustomerId = c.String(nullable: false, maxLength: 255),
                        Rate = c.Int(nullable: false),
                        Comments = c.String(nullable: false, maxLength: 255),
                        Points = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.CustomerId })
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Classifies",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255),
                        Name = c.String(maxLength: 255),
                        CategoryId = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Providers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255),
                        Name = c.String(maxLength: 255),
                        Address = c.String(maxLength: 255),
                        PhoneNumber = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Authorizes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 255),
                        Name = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.User_Authorize", "AuthorizeId", "dbo.Authorizes");
            DropForeignKey("dbo.Products", "BranchId", "dbo.Branches");
            DropForeignKey("dbo.Products", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.Invoices", "ProviderId", "dbo.Providers");
            DropForeignKey("dbo.DetailsOfInvoices", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ClassifyId", "dbo.Classifies");
            DropForeignKey("dbo.Classifies", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Carts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Ratings", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Ratings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Carts", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.WorkAts", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.WorkAts", "BranchId", "dbo.Branches");
            DropForeignKey("dbo.User_Authorize", "UserId", "dbo.Users");
            DropForeignKey("dbo.Employees", "UserId", "dbo.Users");
            DropForeignKey("dbo.Branches", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Bills", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.DetailsOfBills", "ProductId", "dbo.Products");
            DropForeignKey("dbo.DetailsOfBills", "BillId", "dbo.Bills");
            DropForeignKey("dbo.Bills", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.DetailsOfInvoices", "InvoiceId", "dbo.Invoices");
            DropForeignKey("dbo.Invoices", "Branch_Id", "dbo.Branches");
            DropForeignKey("dbo.Branches", "AreaId", "dbo.Areas");
            DropIndex("dbo.Classifies", new[] { "CategoryId" });
            DropIndex("dbo.Ratings", new[] { "CustomerId" });
            DropIndex("dbo.Ratings", new[] { "ProductId" });
            DropIndex("dbo.WorkAts", new[] { "BranchId" });
            DropIndex("dbo.WorkAts", new[] { "EmployeeId" });
            DropIndex("dbo.User_Authorize", new[] { "AuthorizeId" });
            DropIndex("dbo.User_Authorize", new[] { "UserId" });
            DropIndex("dbo.Employees", new[] { "UserId" });
            DropIndex("dbo.DetailsOfBills", new[] { "BillId" });
            DropIndex("dbo.DetailsOfBills", new[] { "ProductId" });
            DropIndex("dbo.Bills", new[] { "EmployeeId" });
            DropIndex("dbo.Bills", new[] { "CustomerId" });
            DropIndex("dbo.Carts", new[] { "CustomerId" });
            DropIndex("dbo.Carts", new[] { "ProductId" });
            DropIndex("dbo.Products", new[] { "ProviderId" });
            DropIndex("dbo.Products", new[] { "BranchId" });
            DropIndex("dbo.Products", new[] { "ClassifyId" });
            DropIndex("dbo.DetailsOfInvoices", new[] { "InvoiceId" });
            DropIndex("dbo.DetailsOfInvoices", new[] { "ProductId" });
            DropIndex("dbo.Invoices", new[] { "Branch_Id" });
            DropIndex("dbo.Invoices", new[] { "ProviderId" });
            DropIndex("dbo.Branches", new[] { "Employee_Id" });
            DropIndex("dbo.Branches", new[] { "AreaId" });
            DropTable("dbo.Authorizes");
            DropTable("dbo.Providers");
            DropTable("dbo.Categories");
            DropTable("dbo.Classifies");
            DropTable("dbo.Ratings");
            DropTable("dbo.WorkAts");
            DropTable("dbo.User_Authorize");
            DropTable("dbo.Users");
            DropTable("dbo.Employees");
            DropTable("dbo.DetailsOfBills");
            DropTable("dbo.Bills");
            DropTable("dbo.Customers");
            DropTable("dbo.Carts");
            DropTable("dbo.Products");
            DropTable("dbo.DetailsOfInvoices");
            DropTable("dbo.Invoices");
            DropTable("dbo.Branches");
            DropTable("dbo.Areas");
        }
    }
}
