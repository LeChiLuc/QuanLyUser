namespace QuanLyUser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtradeagain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "TradeInfoID", "dbo.TradeInfomations");
            DropIndex("dbo.Users", new[] { "TradeInfoID" });
            AddColumn("dbo.TradeInfomations", "UserID", c => c.Int(nullable: false));
            CreateIndex("dbo.TradeInfomations", "UserID");
            AddForeignKey("dbo.TradeInfomations", "UserID", "dbo.Users", "ID", cascadeDelete: true);
            DropColumn("dbo.Users", "TradeInfoID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "TradeInfoID", c => c.Int(nullable: false));
            DropForeignKey("dbo.TradeInfomations", "UserID", "dbo.Users");
            DropIndex("dbo.TradeInfomations", new[] { "UserID" });
            DropColumn("dbo.TradeInfomations", "UserID");
            CreateIndex("dbo.Users", "TradeInfoID");
            AddForeignKey("dbo.Users", "TradeInfoID", "dbo.TradeInfomations", "ID", cascadeDelete: true);
        }
    }
}
