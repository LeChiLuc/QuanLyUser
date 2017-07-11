namespace QuanLyUser.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Email = c.String(maxLength: 50),
                        UserName = c.String(maxLength: 32),
                        Password = c.String(maxLength: 32),
                        CreatedDate = c.DateTime(),
                        Phone = c.String(maxLength: 50),
                        Status = c.Boolean(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
