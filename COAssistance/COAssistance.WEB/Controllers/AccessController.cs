using COAssistance.COMMONS.Models.Account;
using COAssistance.WEB.Constants;
using COAssistance.WEB.Services;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace COAssistance.WEB.Controllers
{
    /// <summary>
    /// Access controller
    /// </summary>
    [RoutePrefix("Access")]
    public class AccessController : BaseController
    {
        #region Fields

        private readonly IHttpClientService _httpClientService;
        private readonly ICommonService _commonService;
        private readonly ICookieService _cookieService;

        #endregion Fields

        #region Constructor

        public AccessController(
            IHttpClientService httpClientService,
            ICommonService commonService,
            ICookieService cookieService)
        {
            _httpClientService = httpClientService;
            _commonService = commonService;
            _cookieService = cookieService;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Returns sign-in page
        /// </summary>
        /// <returns>ActionResult</returns>
        [Route("sign-in", Name = AccessRoutes.SignIn)]
        public ActionResult SignIn() => View();

        /// <summary>
        /// Submits entered sign-in data
        /// </summary>
        /// <param name="model">Sign-in model</param>
        /// <returns>ActionResult</returns>
        [Route("sign-in")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(SignInModel model)
        {
            if (!ModelState.IsValid)
                return View(model: model);

            var accessData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", model.UserName),
                new KeyValuePair<string, string>("password", model.Password),
                new KeyValuePair<string, string>("client_id", ConfigurationManager.AppSettings["WebClientId"])
            };

            var content = new FormUrlEncodedContent(accessData);

            var result = await _httpClientService.PostUrlEncodedContent("token", content);

            if (result.IsSuccessStatusCode)
            {
                var response = await result.Content.ReadAsAsync<TokenModel>();

                _commonService.StoreTokenData(response);

                return RedirectToRoute(HomeRoutes.Index);
            }
            else
            {
                var response = await result.Content.ReadAsAsync<SignInErrorModel>();

                ModelState.AddModelError(string.Empty, response.Text);
            }

            return View(model: model);
        }

        /// <summary>
        /// Signs out user and redirects to sign-in page
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("sign-out", Name = AccessRoutes.SignOut)]
        public ActionResult SignOut()
        {
            _cookieService.Remove(AssistanceConstants.Cookies.AuthenticationTokenKey);

            return RedirectToRoute(AccessRoutes.SignIn);
        }

        #endregion Methods
    }
}