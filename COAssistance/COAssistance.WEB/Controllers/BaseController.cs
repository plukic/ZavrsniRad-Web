using COAssistance.WEB.ActionResults;
using COAssistance.WEB.Infrastructure;
using System.IO;
using System.Web.Mvc;

namespace COAssistance.WEB.Controllers
{
    /// <summary>
    /// Base controller
    /// </summary>
    public class BaseController : Controller
    {
        #region Methods

        ///// <summary>
        ///// Returns JSON notification
        ///// </summary>
        ///// <param name="operationStatus">Operation status</param>
        ///// <param name="type">Notification type</param>
        ///// <param name="message">Message to display</param>
        ///// <param name="redirectUrl">Redirection url</param>
        ///// <param name="content">Additional content</param>
        ///// <returns></returns>
        //protected JsonResult Notification(bool operationStatus,
        //    NotificationType type,
        //    string message,
        //    string redirectUrl = "",
        //    string content = "")
        //{
        //    return Json(data: new
        //    {
        //        operationStatus,
        //        message,
        //        redirectUrl,
        //        type = ("text-" + type).ToLower(),
        //        content
        //    }, behavior: JsonRequestBehavior.AllowGet);
        //}

        /// <summary>
        /// Returns JsonNotificationResult instance constructed with passed arguments
        /// </summary>
        /// <param name="notificationType">Notification types</param>
        /// <param name="text">Notification text</param>
        /// <param name="redirectUrl">Redirect url</param>
        /// <returns>JsonNotificationResult</returns>
        protected JsonNotificationResult NotificationResult(NotificationType notificationType, string text, string redirectUrl)
        {
            return new JsonNotificationResult(notificationType, text, redirectUrl);
        }

        /// <summary>
        /// Shorthand method for success notification
        /// </summary>
        /// <returns>JsonNotificationResult</returns>
        protected JsonNotificationResult NotificationSuccessResult(string text, string redirectUrl = "")
        {
            return NotificationResult(NotificationType.Success, text, redirectUrl);
        }

        /// <summary>
        /// Shorthand method for error notification
        /// </summary>
        /// <returns>JsonNotificationResult</returns>
        protected JsonNotificationResult NotificationErrorResult(string text, string redirectUrl = "")
        {
            return NotificationResult(NotificationType.Error, text, redirectUrl);
        }

        /// <summary>
        /// Shorthand method for warning notification
        /// </summary>
        /// <returns>JsonNotificationResult</returns>
        protected JsonNotificationResult NotificationWarningResult(string text, string redirectUrl = "")
        {
            return NotificationResult(NotificationType.Warning, text, redirectUrl);
        }

        /// <summary>
        /// Renders selected view to string.
        /// Useful in cases where we need to return html content along with some additional data.
        /// </summary>
        /// <param name="viewName">View to render</param>
        /// <param name="model">View model</param>
        /// <returns>String</returns>
        public string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = this.ControllerContext.RouteData.GetRequiredString("action");

            this.ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
                var viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData, writer);
                viewResult.View.Render(viewContext, writer);

                return writer.GetStringBuilder().ToString();
            }
        }

        #endregion Methods
    }
}