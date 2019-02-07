using COAssistance.COMMONS.Logging;
using COAssistance.WEB.App_Start;
using COAssistance.WEB.Controllers;
using COAssistance.WEB.Infrastructure;
using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Unity;

namespace COAssistance.WEB
{
    public class MvcApplication : HttpApplication
    {
        #region Methods

        protected void Application_Start()
        {
            var webApiUri = ConfigurationManager.AppSettings["WebApiUri"];

            if (string.IsNullOrEmpty(webApiUri))
            {
                throw new ArgumentNullException(nameof(webApiUri));
            }

            Bootstrapper.Bootstrap();
        }

        public void Application_Error(object sender, EventArgs args)
        {
            // Disable custom erros by setting <customErrors mode="Off" /> in Web.config
            if (HttpContext.Current.IsCustomErrorEnabled)
            {
                var exception = Server.GetLastError();
                var routeData = new RouteData();

                routeData.Values.Add("controller", "Common");

                if (exception is COUnauthorizedException)
                {
                    routeData.Values.Add("action", "Unauthorized");
                }
                else if (exception is HttpException && ((HttpException)exception).GetHttpCode() == 404)
                {
                    routeData.Values.Add("action", "NotFound");
                }
                else
                {
                    LogError(exception);
                    routeData.Values.Add("action", "ApplicationError");
                }

                Server.ClearError();
                Response.Clear();
                Response.TrySkipIisCustomErrors = true;

                IController targetController = UnityConfig.Container.Resolve<CommonController>();

                targetController.Execute(
                    new RequestContext(new HttpContextWrapper(Context),
                    routeData));
            }
        }

        #endregion Methods

        #region Utils

        private void LogError(Exception exception)
        {
            if (exception == null)
                return;

            var logger = UnityConfig.Container.Resolve<ILogger>();

            logger.LogToFile(exception);
        }

        #endregion Utils
    }
}