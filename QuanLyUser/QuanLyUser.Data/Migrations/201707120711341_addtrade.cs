namespace QuanLyUser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtrade : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TradeInfomations",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Amount = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Users", "Amount", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "TradeInfoID", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "TradeInfoID");
            AddForeignKey("dbo.Users", "TradeInfoID", "dbo.TradeInfomations", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "TradeInfoID", "dbo.TradeInfomations");
            DropIndex("dbo.Users", new[] { "TradeInfoID" });
            DropColumn("dbo.Users", "TradeInfoID");
            DropColumn("dbo.Users", "Amount");
            DropTable("dbo.TradeInfomations");
        }
    }
}
