namespace backendapi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AuthContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Secret = c.String(),
                        Name = c.String(nullable: false, maxLength: 100),
                        AppType = c.Int(nullable: false),
                        Active_Flag = c.Boolean(nullable: false),
                        RefreshToken_LifeSpan = c.Int(nullable: false),
                        Allowed_Origin = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(nullable: false, maxLength: 100),
                        ClientId = c.String(nullable: false, maxLength: 100),
                        IssuedUTC = c.DateTime(nullable: false),
                        ExpiresUTC = c.DateTime(nullable: false),
                        Protected_Ticket = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.APIRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.APIUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.APIRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.APIUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.APIUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
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
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.APIUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.APIUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.APIUserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.APIUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.APIUserRoles", "UserId", "dbo.APIUsers");
            DropForeignKey("dbo.APIUserLogin", "UserId", "dbo.APIUsers");
            DropForeignKey("dbo.APIUserClaims", "UserId", "dbo.APIUsers");
            DropForeignKey("dbo.APIUserRoles", "RoleId", "dbo.APIRoles");
            DropIndex("dbo.APIUserLogin", new[] { "UserId" });
            DropIndex("dbo.APIUserClaims", new[] { "UserId" });
            DropIndex("dbo.APIUsers", "UserNameIndex");
            DropIndex("dbo.APIUserRoles", new[] { "RoleId" });
            DropIndex("dbo.APIUserRoles", new[] { "UserId" });
            DropIndex("dbo.APIRoles", "RoleNameIndex");
            DropTable("dbo.APIUserLogin");
            DropTable("dbo.APIUserClaims");
            DropTable("dbo.APIUsers");
            DropTable("dbo.APIUserRoles");
            DropTable("dbo.APIRoles");
            DropTable("dbo.RefreshTokens");
            DropTable("dbo.Clients");
        }
    }
}
