namespace QuanLyUser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editTypeAmount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TradeInfomations", "Amount", c => c.Double(nullable: false));
            AlterColumn("dbo.Users", "Amount", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Amount", c => c.Int(nullable: false));
            AlterColumn("dbo.TradeInfomations", "Amount", c => c.Int(nullable: false));
        }
    }
}
