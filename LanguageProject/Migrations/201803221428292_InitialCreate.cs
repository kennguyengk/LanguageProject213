namespace LanguageProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        Title = c.String(nullable: false),
                        ImagePanePath = c.String(),
                        Cost = c.Int(nullable: false),
                        Language_Id = c.Int(nullable: false),
                        Teacher_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Teacher_ID, cascadeDelete: true)
                .Index(t => t.Language_Id)
                .Index(t => t.Teacher_ID);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FlagImgPath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FName = c.String(nullable: false),
                        LName = c.String(nullable: false),
                        Balance = c.Int(nullable: false),
                        AvatarPath = c.String(),
                        YouTubeLink = c.String(),
                        Quote = c.String(),
                        TimeZone = c.String(),
                        Email = c.String(nullable: false),
                        NativeLang_Id = c.Int(),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Languages", t => t.NativeLang_Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.NativeLang_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LanguageSkills",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "Teacher_ID", "dbo.Users");
            DropForeignKey("dbo.LanguageSkills", "User_ID", "dbo.Users");
            DropForeignKey("dbo.LanguageSkills", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.Users", "NativeLang_Id", "dbo.Languages");
            DropForeignKey("dbo.Courses", "Language_Id", "dbo.Languages");
            DropIndex("dbo.LanguageSkills", new[] { "User_ID" });
            DropIndex("dbo.LanguageSkills", new[] { "Language_Id" });
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.Users", new[] { "NativeLang_Id" });
            DropIndex("dbo.Courses", new[] { "Teacher_ID" });
            DropIndex("dbo.Courses", new[] { "Language_Id" });
            DropTable("dbo.LanguageSkills");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Languages");
            DropTable("dbo.Courses");
        }
    }
}
