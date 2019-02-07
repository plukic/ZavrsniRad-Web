using COAssistance.COMMONS.Extensions;
using System;
using System.Web;
using System.Web.Mvc;

namespace COAssistance.API.Extensions
{
    /// <summary>
    /// Extends existing UrlHelper(<see cref="UrlHelper"/>) methods
    /// </summary>
    public static class UrlExtensions
    {
        #region Methods

        /// <summary>
        /// Converts specified relative path to absolute path
        /// </summary>
        /// <param name="urlHelper">Current UrlHelper instance</param>
        /// <returns>Generated Uri</returns>
        public static string ToAbsolute(this UrlHelper urlHelper, string path, bool forceHttps = false)
        {
            if (path.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(path));

            // Convert virtual path to applications absolute path
            var relativePath = VirtualPathUtility.ToAbsolute(path);

            // Grab Uri for current request from RequestContext instance.
            // Current Uri will be used as a base for new Uri generation.
            var builder = new UriBuilder(urlHelper.RequestContext.HttpContext.Request.Url)
            {
                Path = relativePath
            };

            if (forceHttps)
                builder.Scheme = Uri.UriSchemeHttps;

            // When we are done with arguments, use Uri property to generate instance with passed arguments.
            return builder.Uri.AbsoluteUri;
        }

        #endregion Methods
    }
}