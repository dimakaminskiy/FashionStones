namespace FashionStones.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "ProductId", c => c.Int(nullable: false));
            DropColumn("dbo.OrderDetails", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderDetails", "Name", c => c.String());
            DropColumn("dbo.OrderDetails", "ProductId");
        }
    }
}
