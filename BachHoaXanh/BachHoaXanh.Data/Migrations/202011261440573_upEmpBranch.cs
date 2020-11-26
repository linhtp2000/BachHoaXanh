namespace BachHoaXanh.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upEmpBranch : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Branches", name: "Employee_Id", newName: "Manage_Id");
            RenameIndex(table: "dbo.Branches", name: "IX_Employee_Id", newName: "IX_Manage_Id");
            DropColumn("dbo.Branches", "ManagerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Branches", "ManagerId", c => c.String(maxLength: 255));
            RenameIndex(table: "dbo.Branches", name: "IX_Manage_Id", newName: "IX_Employee_Id");
            RenameColumn(table: "dbo.Branches", name: "Manage_Id", newName: "Employee_Id");
        }
    }
}
