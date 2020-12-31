namespace BachHoaXanh.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelupp1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "BranchId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "BranchId");
        }
    }
}
