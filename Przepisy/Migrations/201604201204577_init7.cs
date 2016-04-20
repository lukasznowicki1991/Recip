namespace Przepisy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceipModels", "ShortDescription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReceipModels", "ShortDescription");
        }
    }
}
