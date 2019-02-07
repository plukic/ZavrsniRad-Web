using COAssistance.WEB.Constants;
using System;
using System.Web;
using System.Web.Mvc;

namespace COAssistance.WEB.Infrastructure
{
    public class AuthorizeTokenAttribute : AuthorizeAttribute
    {
        #region Methods

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            HttpCookie cookie = HttpContext.Current.Request.Cookies[AssistanceConstants.Cookies.AuthenticationTokenKey];

            return cookie != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException(nameof(filterContext));

            filterContext.Result = new RedirectToRouteResult(AccessRoutes.SignIn, null);
        }

        #endregion Methods
    }
}