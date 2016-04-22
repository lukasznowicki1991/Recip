namespace Przepisy.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init9 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ReceipModels", newName: "RecipModels");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.RecipModels", newName: "ReceipModels");
        }
    }
}
