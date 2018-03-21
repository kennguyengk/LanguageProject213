namespace LanguageProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SecondLanguages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Level = c.Int(nullable: false),
                        Language_Id = c.Int(),
                        User_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.Users", t => t.User_ID)
                .Index(t => t.Language_Id)
                .Index(t => t.User_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FName = c.String(),
                        LName = c.String(),
                        Balance = c.Int(nullable: false),
                        AvatarPath = c.String(),
                        Gender = c.Int(nullable: false),
                        Quote = c.String(),
                        Email = c.String(),
                        NativeLang_Id = c.Int(),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Languages", t => t.NativeLang_Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.NativeLang_Id)
                .Index(t => t.Role_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SecondLanguages", "User_ID", "dbo.Users");
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Users", "NativeLang_Id", "dbo.Languages");
            DropForeignKey("dbo.SecondLanguages", "Language_Id", "dbo.Languages");
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.Users", new[] { "NativeLang_Id" });
            DropIndex("dbo.SecondLanguages", new[] { "User_ID" });
            DropIndex("dbo.SecondLanguages", new[] { "Language_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.SecondLanguages");
            DropTable("dbo.Roles");
            DropTable("dbo.Languages");
        }
    }
}
