using COAssistance.WEB.Infrastructure;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace COAssistance.WEB.App_Start
{
    /// <summary>
    /// Configures startup application settings
    /// </summary>
    public static class Bootstrapper
    {
        #region Methods

        public static void Bootstrap()
        {
            MvcHandler.DisableMvcResponseHeader = true;

            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());
            ModelBinders.Binders.Add(typeof(double?), new DoubleModelBinder());

            //GlobalFilters.Filters.Add(new RequireHttpsAttribute());

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new CORazorViewEngine());
        }

        #endregion Methods
    }
}