namespace Przepisy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceipModels", "ImageSize", c => c.Int(nullable: false));
            AddColumn("dbo.ReceipModels", "FileName", c => c.String());
            AddColumn("dbo.ReceipModels", "ImageData", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReceipModels", "ImageData");
            DropColumn("dbo.ReceipModels", "FileName");
            DropColumn("dbo.ReceipModels", "ImageSize");
        }
    }
}
