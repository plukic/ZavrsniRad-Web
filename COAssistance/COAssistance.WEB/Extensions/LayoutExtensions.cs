using COAssistance.COMMONS.Extensions;
using COAssistance.COMMONS.Resources;
using COAssistance.WEB.Constants;
using System;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace COAssistance.WEB.Extensions
{
    /// <summary>
    /// Html helper extensions for Layout page
    /// </summary>
    public static class LayoutExtensions
    {
        #region Methods

        /// <summary>
        /// Renders page title
        /// </summary>
        /// <param name="html">Html helper instance</param>
        /// <returns>Html string</returns>
        public static IHtmlString Title(this HtmlHelper html)
        {
            var pageTitle = html.ViewBag.PageTitle as string;

            return new HtmlString(
                !pageTitle.IsNullOrEmpty() ?
                $"{AssistanceConstants.ApplicationName} > {pageTitle}" :
                AssistanceConstants.ApplicationName);
        }

        /// <summary>
        /// Renders copyrights section
        /// </summary>
        /// <param name="html">Html helper instance</param>
        /// <returns>Html string</returns>
        public static IHtmlString CopyRights(this HtmlHelper html)
        {
            StringBuilder builder = new StringBuilder();

            if (!AssistanceConstants.ApplicationVersion.IsNullOrEmpty())
            {
                builder.Append($"<div class=\"pull-right hidden-xs\"><b>{Resource.Version}</b> {AssistanceConstants.ApplicationVersion}</div>");
            }
            builder.Append($"<strong>&copy; {DateTime.UtcNow.Year} <a target=\"blank\" href=\"{AssistanceConstants.DeveloperSite}\">{AssistanceConstants.Developer}</a>.</strong>");

            return new HtmlString(builder.ToString());
        }

        #endregion Methods
    }
}