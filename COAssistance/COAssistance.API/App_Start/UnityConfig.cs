using COAssistance.API.Services.ApiClient;
using COAssistance.API.Services.ClientConfiguration;
using COAssistance.API.Services.ClientEmergencyNumbers;
using COAssistance.API.Services.ClientService;
using COAssistance.API.Services.EmailService;
using COAssistance.API.Services.FileService;
using COAssistance.API.Services.HelpRequestResponseService;
using COAssistance.API.Services.HelpRequestService;
using COAssistance.API.Services.LanguageService;
using COAssistance.API.Services.Logger;
using COAssistance.API.Services.MaintenanceService;
using COAssistance.API.Services.NotificationService;
using COAssistance.API.Services.PasswordRecovery;
using COAssistance.API.Services.PredefinedTextTemplates;
using COAssistance.API.Services.RoleService;
using COAssistance.API.Services.TokenService;
using COAssistance.API.Services.UserService;
using COAssistance.API.Util;
using COAssistance.COMMONS.Logging;
using COAssistance.DATA.EF;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Configuration;
using System.Data.Entity;
using System.Net.Http;
using Unity;
using Unity.Injection;

namespace COAssistance.API
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Properties

        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;

        #endregion Properties

        #region Methods

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext, COAssistanceDbContext>();
            container.RegisterType<ILoggerService, DbLoggerService>();
            container.RegisterType<IUserStore<UserLoginData>, UserStore<UserLoginData>>();
            container.RegisterType<IRoleStore<IdentityRole, string>, RoleStore<IdentityRole, string, IdentityUserRole>>();
            container.RegisterType<IRoleService, RoleService>();
            container.RegisterType<IMaintenanceService, MaintenanceService>();
            container.RegisterType<HttpClient>(new InjectionFactory(x => new HttpClient { BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseUrl"]) }));
            container.RegisterType<IApiClient, ApiClient>();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IClientConfigurationService, ClientConfigurationService>();
            container.RegisterType<PagingResultFactory>();
            container.RegisterType<IClientService, ClientService>();
            container.RegisterType<IHelpRequestService, HelpRequestService>();
            container.RegisterType<ILanguageService, LanguageService>();
            container.RegisterType<IFileService, FileService>();
            container.RegisterType<IEmailService, EmailService>();
            container.RegisterType<IPasswordRecoveryService, PasswordRecoveryService>();
            container.RegisterType<ITokenService, TokenService>();
            container.RegisterType<IPredefinedTextTemplateService, PredefinedTextTemplateService>();
            container.RegisterType<IClientEmergencyNumberService, ClientEmergencyNumberService>();

            var emailService = new EmailService
            {
                EmailFrom = ConfigurationManager.AppSettings["EmailAddress"]
            };

            container.RegisterType<EmailService>(new InjectionFactory(x => emailService));
            container.RegisterType<ILogger>(new InjectionFactory((x) =>
            new Logger(ConfigurationManager.AppSettings["LogsFileNameFormat"], ConfigurationManager.AppSettings["LogsLocation"])));

            container.RegisterType<IHelpRequestResponseService, HelpRequestResponseService>();
            container.RegisterType<INotificationService, NotificationService>();
        }

        #endregion Methods
    }
}