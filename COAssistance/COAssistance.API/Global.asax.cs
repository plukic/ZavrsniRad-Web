using COAssistance.COMMONS.Models.Paging;

using COAssistance.API.Services.ClientConfiguration;
using COAssistance.API.Services.ClientService;
using COAssistance.API.Services.RoleService;
using COAssistance.API.Services.UserService;
using COAssistance.COMMONS.Logging;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model;
using COAssistance.DATA.Model.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Unity;
using COAssistance.COMMONS.Models.Clients;
using COAssistance.COMMONS.Models;

namespace COAssistance.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Database.SetInitializer<COAssistanceDbContext>(new Initializer());
            UnityConfig.Container.Resolve<COAssistanceDbContext>().Database.Initialize(true);
        }

        public void Application_Error(object sender, EventArgs args)
        {
            var exception = Server.GetLastError();
            LogError(exception);
        }

        private void LogError(Exception exception)
        {
            if (exception == null)
                return;

            if (exception is HttpException && ((HttpException)exception).GetHttpCode() == (int)HttpStatusCode.NotFound)
            {
                return;
            }

            var logger = UnityConfig.Container.Resolve<ILogger>();
            logger.LogToFile(exception);
        }
    }

    internal class Initializer : IDatabaseInitializer<COAssistanceDbContext>
    {
        public void InitializeDatabase(COAssistanceDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var roleService = UnityConfig.Container.Resolve<IRoleService>();
            var clientService = UnityConfig.Container.Resolve<IClientService>();
            var userService = UnityConfig.Container.Resolve<IUserService>();
            var clientConfigurationService = UnityConfig.Container.Resolve<IClientConfigurationService>();
            var applicationUserManager = UnityConfig.Container.Resolve<ApplicationUserManager>();

            var adminRole = roleService.GetAdminRole();
            var clientRole = roleService.GetClientRole();

            var clientConfiguration = clientConfigurationService.GetClientConfiguration(COMMONS.Models.ConfigurationGroupEnum.CO);
            if (!context.Admin.Any())
            {
                var maintenanceId = GetMaintenanceId(context);
                var userToInsert = new UserLoginData
                {
                    UserName = "admin",
                    OriginUsername = "admin",
                    IsActive = true,
                    Email = "admin@admin.com"
                };
                var admin = new Admin
                {
                    FirstName = "Admin",
                    LastName = "Sistema",
                    MaintenanceId = maintenanceId
                };

                bool isSuccess = applicationUserManager.CreateAsync(userToInsert, "Password!1").Result.Succeeded;
                if (!isSuccess)
                    throw new Exception("Initialization failed");

                admin.AdminId = userToInsert.Id;
                applicationUserManager.AddToRole(userToInsert.Id, EntityRoles.AdminRole);
                context.Admin.Add(admin);
                context.SaveChanges();
            }
            if (!context.Clients.Any())
            {

                var client = new ClientCreateModel
                {
                    CardNumber = "000 210",
                    Email = "petar@ito.ba",
                    FirstName = "Client",
                    LastName = "Sistema",
                    PhoneNumber = "+38763103730",
                    Address = "Adresa",
                    City = "Grad",
                    ConfigurationGroup = ConfigurationGroupEnum.CO

                };
                clientService.Create(client);
            }
            if (!context.OAuthClients.Any())
            {
                var clients = BuildClientsList(adminRole, clientRole);

                context.OAuthClients.AddRange(clients);
                context.SaveChanges();
            }
        }

        private List<OAuthClient> BuildClientsList(IdentityRole adminRole, IdentityRole clientRole)
        {
            var webClientId = ConfigurationManager.AppSettings["WebClientId"];
            var androidClientId = ConfigurationManager.AppSettings["MobileClientId"];

            List<OAuthClient> ClientsList = new List<OAuthClient>
            {
                new OAuthClient
                {   Id = webClientId,
                    Name="Admin Web UI Application",
                    Active = true,
                    OAuthClientRoles = new List<OAuthClientRoles>{
                        new OAuthClientRoles{IdentityRoleId=adminRole.Id,OAuthClientId=webClientId}
                    },
                    RefreshTokenLifeTime = 7200,
                },
                new OAuthClient
                {   Id =androidClientId,
                    Name="Android Client Application",
                    Active = true,
                      OAuthClientRoles = new List<OAuthClientRoles>{
                        new OAuthClientRoles{IdentityRoleId=clientRole.Id,OAuthClientId=androidClientId}
                    },
                    RefreshTokenLifeTime = 0
                }
            };

            return ClientsList;
        }

        private int GetMaintenanceId(COAssistanceDbContext context)
        {
            if (context.Maintenances.Any())
            {
                return context.Maintenances.First().MaintenanceId;
            }
            var maintenance = new Maintenance()
            {
                PasswordLength = 8,
                PasswordSpecialCharacters = 2,
            };
            context.Maintenances.Add(maintenance);
            context.SaveChanges();

            return maintenance.MaintenanceId;
        }

    }
}