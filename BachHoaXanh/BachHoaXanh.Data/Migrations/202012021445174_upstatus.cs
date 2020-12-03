namespace BachHoaXanh.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upstatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bills", "Status", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bills", "Status");
        }
    }
}
