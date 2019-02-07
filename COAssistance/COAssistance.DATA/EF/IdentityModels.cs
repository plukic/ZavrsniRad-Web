using COAssistance.DATA.Model;
using COAssistance.DATA.Model.Authorization;
using COAssistance.DATA.Model.Clients;
using COAssistance.DATA.Model.FirebaseTokens;
using COAssistance.DATA.Model.HelpRequests;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace COAssistance.DATA.EF
{
    public class UserLoginData : IdentityUser
    {
        public bool IsActive { get; set; }
        public virtual Client Client { get; set; }
        public virtual Admin Admin { get; set; }

        [Required]
        public string OriginUsername { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<UserLoginData> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
    }

    public class COAssistanceDbContext : IdentityDbContext<UserLoginData>
    {
        public DbSet<UserAudit> UserAudit { get; set; }
        public DbSet<PredefinedTextTemplate> PredefinedTextTemplates { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Maintenance> Maintenances { get; set; }
        public DbSet<HelpRequest> HelpRequests { get; set; }
        public DbSet<ClientConfigurationGroup> ClientConfigurationGroup { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientToken> ClientTokens { get; set; }
        public DbSet<HelpRequestResponse> HelpRequestsResponses { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<OAuthClient> OAuthClients { get; set; }

        public DbSet<EmergencyContactNumbers> EmergencyContactNumbers { get; set; }
        public DbSet<FirebaseToken> FirebaseTokens { get; set; }

        public COAssistanceDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove(new System.Data.Entity.ModelConfiguration.Conventions.OneToManyCascadeDeleteConvention());
            modelBuilder.Conventions.Remove(new System.Data.Entity.ModelConfiguration.Conventions.ManyToManyCascadeDeleteConvention());

            #region UserLoginData

            modelBuilder.Entity<UserLoginData>()
                .ToTable("UserLoginData");

            modelBuilder.Entity<UserLoginData>()
                        .Ignore(x => x.EmailConfirmed)
                        .Ignore(x => x.PhoneNumber)
                        .Ignore(x => x.PhoneNumberConfirmed)
                        .Ignore(x => x.TwoFactorEnabled)
                        .Ignore(x => x.LockoutEnabled)
                        .Ignore(x => x.LockoutEndDateUtc)
                        .Ignore(x => x.AccessFailedCount);

            modelBuilder.Entity<UserLoginData>()
                .HasOptional(x => x.Admin)
                .WithRequired(x => x.UserLoginData);

            modelBuilder.Entity<UserLoginData>()
                .HasOptional(x => x.Client)
                .WithRequired(x => x.UserLoginData);

            modelBuilder.Entity<UserLoginData>()
                .HasMany(x => x.RefreshTokens)
                .WithRequired(x => x.User)
                .HasForeignKey(x => x.UserId);

            #endregion UserLoginData

            #region RefreshToken

            modelBuilder.Entity<RefreshToken>()
                .HasRequired(x => x.User)
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.UserId);

            #endregion RefreshToken

            #region Admin

            modelBuilder.Entity<Admin>()
                .HasMany(x => x.UserAudit)
                .WithRequired(x => x.Admin)
                .HasForeignKey(x => x.AdminId)
                .WillCascadeOnDelete(false);

          
       

            #endregion Admin

            #region UserAudti

            modelBuilder.Entity<UserAudit>()
                .HasRequired(x => x.Admin)
                .WithMany(x => x.UserAudit)
                .HasForeignKey(x => x.AdminId)
                .WillCascadeOnDelete(false);

            #endregion UserAudti

            #region Language

            modelBuilder.Entity<Language>()
                .HasMany(x => x.ClientConfigurationGroup)
                .WithRequired(x => x.Language)
                .HasForeignKey(x => x.LanguageId)
                .WillCascadeOnDelete(false);
          

            #endregion Language

            #region ClientConfigurationGroup

            modelBuilder.Entity<ClientConfigurationGroup>()
                .HasRequired(x => x.Language)
                .WithMany(x => x.ClientConfigurationGroup)
                .HasForeignKey(x => x.LanguageId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClientConfigurationGroup>()
                .HasMany(x => x.Clients)
                .WithRequired(x => x.ClientConfigurationGroup)
                .HasForeignKey(x => x.ClientConfigurationGroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ClientConfigurationGroup>()
                            .HasMany(x => x.Clients)
                            .WithRequired(x => x.ClientConfigurationGroup)
                            .HasForeignKey(x => x.ClientConfigurationGroupId)
                            .WillCascadeOnDelete(false);

            #endregion ClientConfigurationGroup

            #region Client

            modelBuilder.Entity<Client>()
                .HasRequired(x => x.UserLoginData)
                .WithOptional(x => x.Client);

            modelBuilder.Entity<Client>()
                            .HasMany(x => x.Tokens)
                            .WithRequired(x => x.Client)
                            .HasForeignKey(x => x.ClientId);

            modelBuilder.Entity<Client>()
                .HasMany(x => x.HelpRequests)
                .WithRequired(x => x.Client)
                .HasForeignKey(x => x.ClientId);

          

            modelBuilder.Entity<Client>()
                .HasMany(x => x.EmergencyContactNumbers)
                .WithRequired(x => x.Client)
                .HasForeignKey(x => x.ClientId);

            modelBuilder.Entity<Client>()
                .HasMany(x => x.FirebaseTokens)
                .WithRequired(x => x.Client)
                .HasForeignKey(x => x.ClientId);

            #endregion Client

            #region ClientTokens

            modelBuilder.Entity<ClientToken>()
                            .HasRequired(x => x.Client)
                            .WithMany(x => x.Tokens)
                            .HasForeignKey(x => x.ClientId);

            #endregion ClientTokens

            #region HelpRequest

            modelBuilder.Entity<HelpRequest>()
                .HasRequired(x => x.Client)
                .WithMany(x => x.HelpRequests)
                .HasForeignKey(x => x.ClientId);

            #endregion HelpRequest

            #region Maintenance

            modelBuilder.Entity<Maintenance>().ToTable("Maintenance");
            modelBuilder.Entity<Maintenance>().HasKey(m => m.MaintenanceId);
            modelBuilder.Entity<Maintenance>().HasMany(m => m.Admins).WithRequired(u => u.Maintenance).HasForeignKey(u => u.MaintenanceId);
            modelBuilder.Entity<Maintenance>().HasMany(m => m.PredefinedTextTemplates).WithRequired(u => u.Maintenance).HasForeignKey(u => u.MaintenanceId);

            #endregion Maintenance

            #region PredefinedTextTemplates

            modelBuilder.Entity<PredefinedTextTemplate>().ToTable("PredefinedTextTemplates");
            modelBuilder.Entity<PredefinedTextTemplate>().HasRequired(m => m.Maintenance).WithMany(u => u.PredefinedTextTemplates).HasForeignKey(u => u.MaintenanceId);

            #endregion PredefinedTextTemplates

         


            #region EmergencyContactNumbers

            modelBuilder.Entity<EmergencyContactNumbers>()
                .HasRequired(x => x.Client)
                .WithMany(x => x.EmergencyContactNumbers)
                .HasForeignKey(x => x.ClientId);

            #endregion EmergencyContactNumbers

            #region FirebaseTokens

            modelBuilder.Entity<FirebaseToken>()
                .HasRequired(x => x.Client)
                .WithMany(x => x.FirebaseTokens)
                .HasForeignKey(x => x.ClientId);

            #endregion FirebaseTokens
        }

        public static COAssistanceDbContext Create()
        {
            return new COAssistanceDbContext();
        }
    }
}