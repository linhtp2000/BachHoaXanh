namespace BachHoaXanh.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemAnh : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Image1", c => c.String(maxLength: 255));
            AddColumn("dbo.Products", "Image2", c => c.String(maxLength: 255));
            AddColumn("dbo.Products", "Image3", c => c.String(maxLength: 255));
            DropColumn("dbo.Products", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "Image", c => c.String(maxLength: 255));
            DropColumn("dbo.Products", "Image3");
            DropColumn("dbo.Products", "Image2");
            DropColumn("dbo.Products", "Image1");
        }
    }
}
