namespace LanguageProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderID = c.String(maxLength: 128),
                        ReceiveID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ReceiveID)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderID)
                .Index(t => t.SenderID)
                .Index(t => t.ReceiveID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SenderID = c.String(maxLength: 128),
                        ReceiveID = c.String(maxLength: 128),
                        Content = c.String(),
                        when = c.DateTime(nullable: false),
                        ChatSession_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ChatSessions", t => t.ChatSession_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ReceiveID)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderID)
                .Index(t => t.SenderID)
                .Index(t => t.ReceiveID)
                .Index(t => t.ChatSession_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FName = c.String(nullable: false),
                        LName = c.String(nullable: false),
                        Balance = c.Int(nullable: false),
                        AvatarPath = c.String(),
                        YouTubeLink = c.String(),
                        Quote = c.String(),
                        TimeZone = c.String(),
                        Country = c.String(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        NativeLang_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.NativeLang_Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.NativeLang_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
                        Teacher_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.Teacher_Id, cascadeDelete: true)
                .Index(t => t.Language_Id)
                .Index(t => t.Teacher_Id);
            
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
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.LanguageSkills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Level = c.Int(nullable: false),
                        Language_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.Language_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Language_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.CourseSessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        When = c.DateTime(nullable: false),
                        Status = c.String(),
                        Course_Id = c.Int(),
                        Student_Id = c.String(maxLength: 128),
                        Teacher_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Student_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Teacher_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Student_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.CourseSessions", "Teacher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseSessions", "Student_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CourseSessions", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.ChatSessions", "SenderID", "dbo.AspNetUsers");
            DropForeignKey("dbo.ChatSessions", "ReceiveID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "SenderID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ReceiveID", "dbo.AspNetUsers");
            DropForeignKey("dbo.LanguageSkills", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LanguageSkills", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "NativeLang_Id", "dbo.Languages");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "Teacher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "Language_Id", "dbo.Languages");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Messages", "ChatSession_Id", "dbo.ChatSessions");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.CourseSessions", new[] { "Teacher_Id" });
            DropIndex("dbo.CourseSessions", new[] { "Student_Id" });
            DropIndex("dbo.CourseSessions", new[] { "Course_Id" });
            DropIndex("dbo.LanguageSkills", new[] { "User_Id" });
            DropIndex("dbo.LanguageSkills", new[] { "Language_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.Courses", new[] { "Teacher_Id" });
            DropIndex("dbo.Courses", new[] { "Language_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "NativeLang_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Messages", new[] { "ChatSession_Id" });
            DropIndex("dbo.Messages", new[] { "ReceiveID" });
            DropIndex("dbo.Messages", new[] { "SenderID" });
            DropIndex("dbo.ChatSessions", new[] { "ReceiveID" });
            DropIndex("dbo.ChatSessions", new[] { "SenderID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.CourseSessions");
            DropTable("dbo.LanguageSkills");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Languages");
            DropTable("dbo.Courses");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Messages");
            DropTable("dbo.ChatSessions");
        }
    }
}
