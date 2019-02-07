using COAssistance.API.SignalR;
using Microsoft.AspNet.SignalR;
using System.Web.Http;

using Unity.AspNet.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(COAssistance.API.UnityWebApiActivator), nameof(COAssistance.API.UnityWebApiActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(COAssistance.API.UnityWebApiActivator), nameof(COAssistance.API.UnityWebApiActivator.Shutdown))]
namespace COAssistance.API
{
    /// <summary>
    /// Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET.
    /// </summary>
    public static class UnityWebApiActivator
    {
        /// <summary>
        /// Integrates Unity when the application starts.
        /// </summary>
        public static void Start() 
        {
            // Use UnityHierarchicalDependencyResolver if you want to use
            // a new child container for each IHttpController resolution.
            // var resolver = new UnityHierarchicalDependencyResolver(UnityConfig.Container);
            var container = UnityConfig.Container;
            var resolver = new UnityDependencyResolver(container);
      
            GlobalConfiguration.Configuration.DependencyResolver = resolver;


        }

        /// <summary>
        /// Disposes the Unity container when the application is shut down.
        /// </summary>
        public static void Shutdown()
        {
            UnityConfig.Container.Dispose();
        }
    }
}