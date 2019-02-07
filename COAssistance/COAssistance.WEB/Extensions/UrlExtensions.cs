using COAssistance.WEB.Constants;
using System;
using System.Web.Mvc;

namespace COAssistance.WEB.Extensions
{
    /// <summary>
    /// Extends existing UrlHelper(<see cref="UrlHelper"/>) extensions
    /// </summary>
    public static class UrlExtensions
    {
        #region Methods

        /// <summary>
        /// Return previous url(referrer), otherwise redirects to home page
        /// </summary>
        /// <param name="urlHelper">Current helper instance</param>
        /// <param name="referrer">Refferer uri for current request</param>
        /// <returns>Url as string</returns>
        public static string ReturnUrl(this UrlHelper urlHelper, Uri referrer)
        {
            if (referrer != null)
            {
                var url = referrer.AbsolutePath.ToString();

                // Do not use referrer if it points to external site
                if (!urlHelper.IsLocalUrl(url))
                    return urlHelper.RouteUrl(HomeRoutes.Index);

                return url;
            }

            // No referrer url, redirect to home page
            return urlHelper.RouteUrl(HomeRoutes.Index);
        }

        #endregion Methods
    }
}