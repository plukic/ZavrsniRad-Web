namespace COAssistance.DATA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        AdminId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MaintenanceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdminId)
                .ForeignKey("dbo.Maintenance", t => t.MaintenanceId, cascadeDelete: true)
                .ForeignKey("dbo.UserLoginData", t => t.AdminId)
                .Index(t => t.AdminId)
                .Index(t => t.MaintenanceId);
            
            CreateTable(
                "dbo.Maintenance",
                c => new
                    {
                        MaintenanceId = c.Int(nullable: false, identity: true),
                        PasswordLength = c.Int(nullable: false),
                        PasswordSpecialCharacters = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaintenanceId);
            
            CreateTable(
                "dbo.PredefinedTextTemplates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        Key = c.String(),
                        MaintenanceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Maintenance", t => t.MaintenanceId, cascadeDelete: true)
                .Index(t => t.MaintenanceId);
            
            CreateTable(
                "dbo.UserAudits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ActionType = c.String(),
                        Description = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        AdminId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Admins", t => t.AdminId)
                .Index(t => t.AdminId);
            
            CreateTable(
                "dbo.UserLoginData",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        IsActive = c.Boolean(nullable: false),
                        OriginUsername = c.String(nullable: false),
                        Email = c.String(maxLength: 256),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
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
                .ForeignKey("dbo.UserLoginData", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        BloodType = c.Int(),
                        ChronicDiseases = c.String(),
                        Diagnose = c.String(),
                        HistoryOfCriticalIllness = c.String(),
                        ClientConfigurationGroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.ClientConfigurationGroups", t => t.ClientConfigurationGroupId)
                .ForeignKey("dbo.UserLoginData", t => t.ClientId)
                .Index(t => t.ClientId)
                .Index(t => t.ClientConfigurationGroupId);
            
            CreateTable(
                "dbo.ClientConfigurationGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ConfigurationGroupEnum = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        SupportNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Languages", t => t.LanguageId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.Languages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LanguageName = c.String(nullable: false),
                        LanguageCode = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmergencyContactNumbers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.String(nullable: false, maxLength: 128),
                        ContactFullName = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.FirebaseTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(nullable: false),
                        UniqueMobileId = c.String(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ClientId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.HelpRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RequestDate = c.DateTime(nullable: false),
                        HelpRequestCategory = c.Int(nullable: false),
                        IsSolved = c.Boolean(nullable: false),
                        Langitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        ClientId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.HelpRequestResponses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HelpRequestState = c.String(nullable: false),
                        ShortInstruction = c.String(nullable: false),
                        HelpIncomingDateTimeUtc = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        HelpRequestId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.HelpRequests", t => t.HelpRequestId, cascadeDelete: true)
                .ForeignKey("dbo.Languages", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.HelpRequestId)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.ClientTokens",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Token = c.String(nullable: false),
                        ValidUntil = c.DateTime(nullable: false),
                        IsUsed = c.Boolean(nullable: false),
                        ClientId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.UserLoginData", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.RefreshTokens",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Subject = c.String(nullable: false, maxLength: 50),
                        OAuthClientId = c.String(nullable: false, maxLength: 128),
                        IssuedUtc = c.DateTime(nullable: false),
                        ExpiresUtc = c.DateTime(nullable: false),
                        ProtectedTicket = c.String(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.OAuthClients", t => t.OAuthClientId, cascadeDelete: true)
                .ForeignKey("dbo.UserLoginData", t => t.UserId, cascadeDelete: true)
                .Index(t => t.OAuthClientId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.OAuthClients",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 100),
                        Active = c.Boolean(nullable: false),
                        RefreshTokenLifeTime = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OAuthClientRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OAuthClientId = c.String(maxLength: 128),
                        IdentityRoleId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetRoles", t => t.IdentityRoleId)
                .ForeignKey("dbo.OAuthClients", t => t.OAuthClientId)
                .Index(t => t.OAuthClientId)
                .Index(t => t.IdentityRoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.UserLoginData", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.UserLoginData");
            DropForeignKey("dbo.RefreshTokens", "UserId", "dbo.UserLoginData");
            DropForeignKey("dbo.RefreshTokens", "OAuthClientId", "dbo.OAuthClients");
            DropForeignKey("dbo.OAuthClientRoles", "OAuthClientId", "dbo.OAuthClients");
            DropForeignKey("dbo.OAuthClientRoles", "IdentityRoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.UserLoginData");
            DropForeignKey("dbo.Clients", "ClientId", "dbo.UserLoginData");
            DropForeignKey("dbo.ClientTokens", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.HelpRequests", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.HelpRequestResponses", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.HelpRequestResponses", "HelpRequestId", "dbo.HelpRequests");
            DropForeignKey("dbo.FirebaseTokens", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.EmergencyContactNumbers", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.ClientConfigurationGroups", "LanguageId", "dbo.Languages");
            DropForeignKey("dbo.Clients", "ClientConfigurationGroupId", "dbo.ClientConfigurationGroups");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.UserLoginData");
            DropForeignKey("dbo.Admins", "AdminId", "dbo.UserLoginData");
            DropForeignKey("dbo.UserAudits", "AdminId", "dbo.Admins");
            DropForeignKey("dbo.PredefinedTextTemplates", "MaintenanceId", "dbo.Maintenance");
            DropForeignKey("dbo.Admins", "MaintenanceId", "dbo.Maintenance");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.OAuthClientRoles", new[] { "IdentityRoleId" });
            DropIndex("dbo.OAuthClientRoles", new[] { "OAuthClientId" });
            DropIndex("dbo.RefreshTokens", new[] { "UserId" });
            DropIndex("dbo.RefreshTokens", new[] { "OAuthClientId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.ClientTokens", new[] { "ClientId" });
            DropIndex("dbo.HelpRequestResponses", new[] { "LanguageId" });
            DropIndex("dbo.HelpRequestResponses", new[] { "HelpRequestId" });
            DropIndex("dbo.HelpRequests", new[] { "ClientId" });
            DropIndex("dbo.FirebaseTokens", new[] { "ClientId" });
            DropIndex("dbo.EmergencyContactNumbers", new[] { "ClientId" });
            DropIndex("dbo.ClientConfigurationGroups", new[] { "LanguageId" });
            DropIndex("dbo.Clients", new[] { "ClientConfigurationGroupId" });
            DropIndex("dbo.Clients", new[] { "ClientId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.UserLoginData", "UserNameIndex");
            DropIndex("dbo.UserAudits", new[] { "AdminId" });
            DropIndex("dbo.PredefinedTextTemplates", new[] { "MaintenanceId" });
            DropIndex("dbo.Admins", new[] { "MaintenanceId" });
            DropIndex("dbo.Admins", new[] { "AdminId" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.OAuthClientRoles");
            DropTable("dbo.OAuthClients");
            DropTable("dbo.RefreshTokens");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.ClientTokens");
            DropTable("dbo.HelpRequestResponses");
            DropTable("dbo.HelpRequests");
            DropTable("dbo.FirebaseTokens");
            DropTable("dbo.EmergencyContactNumbers");
            DropTable("dbo.Languages");
            DropTable("dbo.ClientConfigurationGroups");
            DropTable("dbo.Clients");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.UserLoginData");
            DropTable("dbo.UserAudits");
            DropTable("dbo.PredefinedTextTemplates");
            DropTable("dbo.Maintenance");
            DropTable("dbo.Admins");
        }
    }
}
