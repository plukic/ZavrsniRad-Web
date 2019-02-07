using COAssistance.WEB.Constants;
using System.Web.Mvc;
using System.Web.Routing;

namespace COAssistance.WEB
{
    /// <summary>
    /// Registers route settings and maps route templates
    /// </summary>
    public class RouteConfig
    {
        #region Methods

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.AppendTrailingSlash = true;
            routes.LowercaseUrls = true;
            routes.RouteExistingFiles = false;

            routes.MapMvcAttributeRoutes();

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("favicon.ico");

            #region GasStation controller

            routes.MapRoute(
                name: GasStationRoutes.Index,
                url: "Gas-Stations",
                defaults: new { controller = "GasStation", action = "Index" });

            routes.MapRoute(
                name: GasStationRoutes.Data,
                url: "Gas-Stations/Data",
                defaults: new { controller = "GasStation", action = "Data" });

            routes.MapRoute(
                name: GasStationRoutes.Create,
                url: "Gas-Stations/Create",
                defaults: new { controller = "GasStation", action = "Create" });

            routes.MapRoute(
                name: GasStationRoutes.Edit,
                url: "Gas-Stations/Edit/{gasStationId}",
                defaults: new { controller = "GasStation", action = "Edit" });

            #endregion GasStation controller

            #region GasCompany controller

            routes.MapRoute(
                   name: GasCompanyRoutes.Data,
                   url: "Gas-Companies/Data",
                   defaults: new { controller = "GasCompany", action = "Data" });

            routes.MapRoute(
                name: GasCompanyRoutes.Create,
                url: "Gas-Companies/Create",
                defaults: new { controller = "GasCompany", action = "Create" });

            routes.MapRoute(
                name: GasCompanyRoutes.CreateLogo,
                url: "Gas-Companies/Create-Logo",
                defaults: new { controller = "GasCompany", action = "CreateLogo" });

            routes.MapRoute(
                name: GasCompanyRoutes.EditLogo,
                url: "Gas-Companies/Edit-Logo",
                defaults: new { controller = "GasCompany", action = "EditLogo" });

            routes.MapRoute(
                name: GasCompanyRoutes.Edit,
                url: "Gas-Companies/Edit/{gasCompanyId}",
                defaults: new { controller = "GasCompany", action = "Edit" });

            routes.MapRoute(
                name: GasCompanyRoutes.CancelCreation,
                url: "Gas-Companies/Cancel-Creation/{gasCompanyId}",
                defaults: new { controller = "GasCompany", action = "CancelCreation" });

            routes.MapRoute(
                name: GasCompanyRoutes.Details,
                url: "Gas-Companies/Details/{gasCompanyId}",
                defaults: new { controller = "GasCompany", action = "Details" });

            #endregion GasCompany controller

            #region Common controller

            routes.MapRoute(
                name: CommonRoutes.Confirmation,
                url: "Common/Confirmation/{Id}/{routeName}",
                defaults: new { controller = "Common", action = "Confirmation" });

            #endregion Common controller

            // Default route template
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional });
        }

        #endregion Methods
    }
}