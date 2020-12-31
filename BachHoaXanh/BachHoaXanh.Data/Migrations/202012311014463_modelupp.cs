namespace BachHoaXanh.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelupp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Branches", "Employee_Id", c => c.String(maxLength: 255));
            CreateIndex("dbo.Branches", "Employee_Id");
            AddForeignKey("dbo.Branches", "Employee_Id", "dbo.Employees", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Branches", "Employee_Id", "dbo.Employees");
            DropIndex("dbo.Branches", new[] { "Employee_Id" });
            DropColumn("dbo.Branches", "Employee_Id");
        }
    }
}
