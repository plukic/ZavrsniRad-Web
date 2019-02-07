using System.Web.Optimization;

namespace COAssistance.WEB.App_Start
{
    /// <summary>
    /// Bundles and minifies all .css and .js files in packages to improve page load time
    /// </summary>
    public class BundleConfig
    {
        #region Methods

        public static void RegisterBundles(BundleCollection bundles)
        {
            // Set value of property bellow to true in production. true enables file minification
            BundleTable.EnableOptimizations = false;
            AddCssFiles(bundles);
            AddJsFiles(bundles);
        }

        /// <summary>
        /// Registers all css files
        /// </summary>
        /// <param name="bundles">Bundle collection</param>
        public static void AddCssFiles(BundleCollection bundles)
        {
            /*
             This application uses [AdminLTE] template.
             Only basic files are included in bundle bellow.
             Remove all unnecessary .css classes when you are done with project.
             This way we gain some performance boost.
            */

            bundles.Add(new StyleBundle("~/bundles/css/core").Include(
                "~/Content/template/css/bootstrap.css",
                "~/Content/template/css/adminlte.css",
                "~/Content/template/css/fontawesome.css",
                "~/Content/template/css/skinred.css"));

            bundles.Add(new StyleBundle("~/bundles/css/summernote").Include(
                "~/Content/template/css/summernote.css"));

            bundles.Add(new StyleBundle("~/bundles/css/croppie").Include(
                "~/Content/template/css/croppie.css"));
        }

        /// <summary>
        /// Registers all js files
        /// </summary>
        /// <param name="bundles">Bundle collection</param>
        public static void AddJsFiles(BundleCollection bundles)
        {
            // Remove all unnecessary .js code when you are done with project

            // Main JavaScript files rendered in head section
            bundles.Add(new ScriptBundle("~/bundles/js/core").Include(
                "~/Scripts/jquery.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/App/adminlte.js",
                "~/Scripts/App/admin.js"));

            // Jquery validation and unobtrusive script to enable client-side validation
            bundles.Add(new ScriptBundle("~/bundles/js/validation").Include(
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js"));

            // JavaScript files to load at the bottom of body section
            bundles.Add(new ScriptBundle("~/bundles/js/rest").Include(
                "~/Scripts/jquery.signalR-2.3.0.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/summernote").Include(
                "~/Scripts/App/summernote.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/croppie").Include(
                "~/Scripts/App/croppie.js"));
        }

        #endregion Methods
    }
}