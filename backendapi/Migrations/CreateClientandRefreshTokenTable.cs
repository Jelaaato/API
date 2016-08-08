using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace backendapi.Migrations
{
    public partial class CreateClientandRefreshTokenTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Client",
                b => new
                {
                    Id = b.String(nullable: false, maxLength: 128),
                    Secret = b.String(nullable: false),
                    Name = b.String(nullable: false, maxLength: 100),
                    AppType = b.Int(nullable: false),
                    Active_Flag = b.Boolean(nullable: false),
                    RefreshToken_LifeSpan = b.Int(nullable: false),
                    Allowed_Origin = b.String(maxLength: 100),
                })
                .PrimaryKey(a => a.Id);

            CreateTable(
                "dbo.RefreshToken",
                d => new
                {
                    Id = d.String(nullable: false, maxLength: 128),
                    Subject = d.String(nullable: false, maxLength: 100),
                    ClientId = d.String(nullable: false, maxLength: 100),
                    IssuedUTC = d.DateTime(nullable: false),
                    ExpiresUTC = d.DateTime(nullable: false),
                    Protected_Ticket = d.String(nullable: false),
                })
                .PrimaryKey(a => a.Id);

            CreateTable(
                "dbo.AspNetRoles",
                d => new
                {
                    Id = d.String(nullable: false, maxLength: 128),
                    Name = d.String(nullable: false, maxLength: 256),
                })
                .PrimaryKey(a => a.Id)
                .Index(a => a.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                "dbo.AspNetUserRoles",
                 d => new
                {
                    UserId = d.String(nullable: false, maxLength: 128),
                    RoleId = d.String(nullable: false, maxLength: 128),
                })
                .PrimaryKey(a => new { a.UserId, a.RoleId })
                .ForeignKey("dbo.AspNetRoles", a => a.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", a => a.UserId, cascadeDelete: true)
                .Index(a => a.UserId)
                .Index(a => a.RoleId);

            CreateTable(
                "dbo.AspNetUsers",
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
        }
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles" , "RoleNameIndex");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Client");
            DropTable("dbo.RefreshToken");
        }
    }
}