using System.Web.Mvc;

namespace COAssistance.WEB.Infrastructure
{
    public class CORazorViewEngine : RazorViewEngine
    {
        #region Constructor

        public CORazorViewEngine()
        {
            AreaViewLocationFormats = new[]
                {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            AreaMasterLocationFormats = new[]
            {
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            AreaPartialViewLocationFormats = new[]
            {
                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                "~/Areas/{2}/Views/Shared/{0}.cshtml"
            };

            ViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            MasterLocationFormats = new[]
            {
                "~/Views/Shared/{0}.cshtml"
            };

            PartialViewLocationFormats = new[]
            {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            FileExtensions = new[]
            {
                "cshtml"
            };
        }

        #endregion Constructor
    }
}