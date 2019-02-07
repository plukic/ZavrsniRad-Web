using COAssistance.COMMONS.Models;
using COAssistance.COMMONS.Resources;
using COAssistance.WEB.Constants;
using COAssistance.WEB.Infrastructure;
using System;
using System.Net;
using System.Web.Mvc;

namespace COAssistance.WEB.Controllers
{
    /// <summary>
    /// Common controller
    /// </summary>
    [AuthorizeToken]
    public class CommonController : BaseController
    {
        #region Methods

        /// <summary>
        /// Handles global(except 404 and 401) exceptions thrown by application
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult ApplicationError()
        {
            Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (Request.IsAjaxRequest())
                return NotificationErrorResult(Resource.ApplicationErrorText);

            return View(model: Resource.ApplicationErrorText);
        }

        /// <summary>
        /// Handles 404-NotFound exceptions thrown by application
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult NotFound()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;

            if (Request.IsAjaxRequest())
                return NotificationErrorResult(Resource.DataNotFound);

            return View();
        }

        /// <summary>
        /// Handles 401-Unauthorized exceptions thrown by application
        /// </summary>
        /// <returns>ActionResult</returns>
        public ActionResult Unauthorized()
        {
            Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            if (Request.IsAjaxRequest())
                return NotificationErrorResult(Resource.DataNotFound, Url.RouteUrl(AccessRoutes.SignIn));

            return RedirectToRoute(AccessRoutes.SignIn);
        }

        /// <summary>
        /// Returns confirmation modal dialog to confirm selected operation. Ie. when deleting record etc.
        /// </summary>
        /// <param name="Id">Record identifier</param>
        /// <param name="routeName">Route name to invoke</param>
        /// <param name="updateTargetId">HTML element id to refresh</param>
        /// <param name="additionalData">Additional data</param>
        /// <returns>ActionResult</returns>
        public ActionResult Confirmation(string Id, string routeName, string updateTargetId = null, object additionalData = null)
        {
            if (string.IsNullOrEmpty(routeName))
                throw new ArgumentNullException(nameof(routeName));

            var model = new ConfirmationModel
            {
                RouteName = routeName,
                Id = Id,
                AdditionalData = additionalData,
                UpdateTargetId = updateTargetId
            };

            return PartialView("_Confirmation", model: model);
        }

        #endregion Methods
    }
}