using COAssistance.COMMONS.Logging;
using COAssistance.WEB.Constants;
using COAssistance.WEB.Factories;
using COAssistance.WEB.Services;
using System;
using System.Configuration;
using System.Net.Http;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace COAssistance.WEB
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

        public static IUnityContainer Container => container.Value;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        public static void RegisterTypes(IUnityContainer container)
        {
            #region Services

            var client = HttpClientFactory.Create(new Services.HttpClientHandler());

            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiUri"]);

            container.RegisterType<IHttpClientService, HttpClientService>(new ContainerControlledLifetimeManager());
            container.RegisterType<HttpClient>(new ContainerControlledLifetimeManager(), new InjectionFactory(f => client));
            container.RegisterType<ICookieService, CookieService>();
            container.RegisterType<IFileService, FileService>();
            container.RegisterType<ICommonService, CommonService>();
            container.RegisterType<ILogger>(new InjectionFactory(f => new Logger(AssistanceConstants.LogsFileNameFormat, AssistanceConstants.Paths.LogsLocation)));

            #endregion Services

            #region Model Factories

         
            container.RegisterType<IHelpRequestFactory, HelpRequestFactory>();
            container.RegisterType<IUserGroupsFactory, UserGroupsFactory>();

            #endregion Model Factories
        }

        #endregion Methods
    }
}