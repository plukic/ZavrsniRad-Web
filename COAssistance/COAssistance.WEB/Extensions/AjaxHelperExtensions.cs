using System.Web;
using System.Web.Mvc;

namespace COAssistance.WEB.Extensions
{
    /// <summary>
    /// Extends existing AjaxHelper methods
    /// </summary>
    public static class AjaxHelperExtensions
    {
        #region Methods

        /// <summary>
        /// Creates script element with call to admin.js method.
        /// </summary>
        /// <param name="ajaxHelper">Current AjaxHelper instance</param>
        /// <param name="method">JavaScript method</param>
        /// <param name="url">Url to invoke</param>
        /// <returns>HTML encoded string</returns>
        public static IHtmlString JavaScriptActionAsync(this AjaxHelper ajaxHelper, string method, string url)
        {
            var tagBuilder = new TagBuilder("script")
            {
                InnerHtml = $"{method}('{url}')"
            };

            return new HtmlString(tagBuilder.ToString());
        }

        #endregion Methods
    }
}