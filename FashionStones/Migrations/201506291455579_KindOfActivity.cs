namespace FashionStones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class KindOfActivity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "KindOfActivity", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "KindOfActivity");
        }
    }
}
