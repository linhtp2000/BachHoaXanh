namespace BachHoaXanh.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.Employees", "Employee_Id", "dbo.Employees");
            //DropForeignKey("dbo.Employees", "Administrator_Id", "dbo.Employees");
            //DropForeignKey("dbo.Employees", "Employee_Id1", "dbo.Employees");
            //DropForeignKey("dbo.Employees", "ManageBranch_Id", "dbo.Branches");
            //DropForeignKey("dbo.Employees", "ManagerId", "dbo.Employees");
            //DropForeignKey("dbo.Users", "Id", "dbo.Employees");
            //DropForeignKey("dbo.Employees", "WorkAtBranch_Id", "dbo.Branches");
            //DropForeignKey("dbo.Employees", "Branch_Id", "dbo.Branches");
            //DropForeignKey("dbo.User_Authorize", new[] { "User_Id", "User_Email" }, "dbo.Users");
            //DropForeignKey("dbo.Branches", "ManagerId", "dbo.Employees");
            DropIndex("dbo.Branches", new[] { "ManagerId" });
            DropIndex("dbo.Employees", new[] { "ManagerId" });
            DropIndex("dbo.Employees", new[] { "Employee_Id" });
            DropIndex("dbo.Employees", new[] { "Administrator_Id" });
            DropIndex("dbo.Employees", new[] { "Employee_Id1" });
            DropIndex("dbo.Employees", new[] { "ManageBranch_Id" });
            DropIndex("dbo.Employees", new[] { "WorkAtBranch_Id" });
            DropIndex("dbo.Employees", new[] { "Branch_Id" });
            DropIndex("dbo.Users", new[] { "Id" });
            //DropIndex("dbo.User_Authorize", new[] { "User_Id", "User_Email" });
            //DropColumn("dbo.User_Authorize", "UserId");
            //RenameColumn(table: "dbo.User_Authorize", name: "User_Id", newName: "UserId");
            //DropPrimaryKey("dbo.Users");
            //DropPrimaryKey("dbo.User_Authorize");
            //CreateTable(
            //    "dbo.WorkAts",
            //    c => new
            //        {
            //            EmployeeId = c.String(nullable: false, maxLength: 255),
            //            BranchId = c.String(nullable: false, maxLength: 255),
            //            Position = c.String(maxLength: 255),
            //        })
            //    .PrimaryKey(t => new { t.EmployeeId, t.BranchId })
            //    .ForeignKey("dbo.Branches", t => t.BranchId, cascadeDelete: true)
            //    .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
            //    .Index(t => t.EmployeeId)
            //    .Index(t => t.BranchId);

            //AddColumn("dbo.Branches", "Employee_Id", c => c.String(maxLength: 255));
            //AddColumn("dbo.Employees", "PhoneNumber", c => c.String());
            //AddColumn("dbo.Employees", "UserId", c => c.String(maxLength: 255));
            //AddColumn("dbo.Users", "Name", c => c.String());
            //AlterColumn("dbo.Customers", "PhoneNumber", c => c.String());
            //AlterColumn("dbo.User_Authorize", "UserId", c => c.String(nullable: false, maxLength: 255));
            //AddPrimaryKey("dbo.Users", "Id");
            //AddPrimaryKey("dbo.User_Authorize", new[] { "UserId", "AuthId" });
            //CreateIndex("dbo.Branches", "Employee_Id");
            //CreateIndex("dbo.Employees", "UserId");
            //CreateIndex("dbo.User_Authorize", "UserId");
            //AddForeignKey("dbo.Employees", "UserId", "dbo.Users", "Id");
            //AddForeignKey("dbo.User_Authorize", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            //AddForeignKey("dbo.Branches", "Employee_Id", "dbo.Employees", "Id");
            DropColumn("dbo.Employees", "BranchId");
            DropColumn("dbo.Employees", "Employee_Id");
            DropColumn("dbo.Employees", "Administrator_Id");
            DropColumn("dbo.Employees", "Employee_Id1");
            DropColumn("dbo.Employees", "ManageBranch_Id");
            DropColumn("dbo.Employees", "WorkAtBranch_Id");
            DropColumn("dbo.Employees", "Branch_Id");
            DropColumn("dbo.Users", "Email");
            DropColumn("dbo.Users", "PassWord");
            DropColumn("dbo.User_Authorize", "User_Email");
        }

        public override void Down()
        {
            AddColumn("dbo.User_Authorize", "User_Email", c => c.String(maxLength: 255));
            AddColumn("dbo.Users", "PassWord", c => c.String(maxLength: 255));
            AddColumn("dbo.Users", "Email", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Employees", "Branch_Id", c => c.String(maxLength: 255));
            AddColumn("dbo.Employees", "WorkAtBranch_Id", c => c.String(maxLength: 255));
            AddColumn("dbo.Employees", "ManageBranch_Id", c => c.String(maxLength: 255));
            AddColumn("dbo.Employees", "Employee_Id1", c => c.String(maxLength: 255));
            AddColumn("dbo.Employees", "Administrator_Id", c => c.String(maxLength: 255));
            AddColumn("dbo.Employees", "Employee_Id", c => c.String(maxLength: 255));
            AddColumn("dbo.Employees", "BranchId", c => c.String(maxLength: 255));
            DropForeignKey("dbo.Branches", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.User_Authorize", "UserId", "dbo.Users");
            DropForeignKey("dbo.WorkAts", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.WorkAts", "BranchId", "dbo.Branches");
            DropForeignKey("dbo.Employees", "UserId", "dbo.Users");
            DropIndex("dbo.WorkAts", new[] { "BranchId" });
            DropIndex("dbo.WorkAts", new[] { "EmployeeId" });
            DropIndex("dbo.User_Authorize", new[] { "UserId" });
            DropIndex("dbo.Employees", new[] { "UserId" });
            DropIndex("dbo.Branches", new[] { "Employee_Id" });
            DropPrimaryKey("dbo.User_Authorize");
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.User_Authorize", "UserId", c => c.String(maxLength: 255));
            AlterColumn("dbo.Customers", "PhoneNumber", c => c.String(maxLength: 255));
            DropColumn("dbo.Users", "Name");
            DropColumn("dbo.Employees", "UserId");
            DropColumn("dbo.Employees", "PhoneNumber");
            DropColumn("dbo.Branches", "Employee_Id");
            DropTable("dbo.WorkAts");
            AddPrimaryKey("dbo.User_Authorize", new[] { "UserId", "AuthId" });
            AddPrimaryKey("dbo.Users", new[] { "Id", "Email" });
            RenameColumn(table: "dbo.User_Authorize", name: "UserId", newName: "User_Id");
            AddColumn("dbo.User_Authorize", "UserId", c => c.String(nullable: false, maxLength: 255));
            CreateIndex("dbo.User_Authorize", new[] { "User_Id", "User_Email" });
            CreateIndex("dbo.Users", "Id");
            CreateIndex("dbo.Employees", "Branch_Id");
            CreateIndex("dbo.Employees", "WorkAtBranch_Id");
            CreateIndex("dbo.Employees", "ManageBranch_Id");
            CreateIndex("dbo.Employees", "Employee_Id1");
            CreateIndex("dbo.Employees", "Administrator_Id");
            CreateIndex("dbo.Employees", "Employee_Id");
            CreateIndex("dbo.Employees", "ManagerId");
            CreateIndex("dbo.Branches", "ManagerId");
            AddForeignKey("dbo.Branches", "ManagerId", "dbo.Employees", "Id");
            AddForeignKey("dbo.User_Authorize", new[] { "User_Id", "User_Email" }, "dbo.Users", new[] { "Id", "Email" });
            AddForeignKey("dbo.Employees", "Branch_Id", "dbo.Branches", "Id");
            AddForeignKey("dbo.Employees", "WorkAtBranch_Id", "dbo.Branches", "Id");
            AddForeignKey("dbo.Users", "Id", "dbo.Employees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Employees", "ManagerId", "dbo.Employees", "Id");
            AddForeignKey("dbo.Employees", "ManageBranch_Id", "dbo.Branches", "Id");
            AddForeignKey("dbo.Employees", "Employee_Id1", "dbo.Employees", "Id");
            AddForeignKey("dbo.Employees", "Administrator_Id", "dbo.Employees", "Id");
            AddForeignKey("dbo.Employees", "Employee_Id", "dbo.Employees", "Id");
        }
    }
}
