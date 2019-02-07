using COAssistance.COMMONS.Models.Profile;
using COAssistance.COMMONS.Resources;
using COAssistance.WEB.Constants;
using COAssistance.WEB.Infrastructure;
using COAssistance.WEB.Services;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace COAssistance.WEB.Controllers
{
    /// <summary>
    /// Profile controller
    /// </summary>
    [AuthorizeToken]
    [RoutePrefix("Profile")]
    public class ProfileController : BaseController
    {
        #region Fields

        private readonly IHttpClientService _httpClientService;
        private readonly ICommonService _commonService;

        #endregion Fields

        #region Constructor

        public ProfileController(
            IHttpClientService httpClientService,
            ICommonService commonService)
        {
            _httpClientService = httpClientService;
            _commonService = commonService;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Returns home page for this controller
        /// </summary>
        /// <returns></returns>
        [Route("", Name = ProfileRoutes.Index)]
        public ActionResult Index() => View();

        /// <summary>
        /// Returns data for home page using asynchronous call
        /// </summary>
        /// <returns>ActionResult</returns>
        [Route("data", Name = ProfileRoutes.Data)]
        public ActionResult Data() => PartialView("_Data");

        /// <summary>
        /// Returns change password modal dialog for current user
        /// </summary>
        /// <returns>ActionResult</returns>
        [Route("change-password", Name = ProfileRoutes.ChangePassword)]
        public ActionResult ChangePassword() => PartialView("_ChangePassword", model: new ProfileChangePasswordModel());

        /// <summary>
        /// Saves changed password to database
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [Route("change-password")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ProfileChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ChangePassword", model: model);
            }

            model.Id = "0";

            var response = await _httpClientService.SendRequest("api/admin/password/update", HttpMethod.Put, model);

            if (response.IsSuccessStatusCode)
            {
                return NotificationSuccessResult(Resource.PasswordResetSuccess, Url.RouteUrl(ProfileRoutes.Data));
            }

            var errorMessage = await _commonService.CheckForValidationErrors(response, Resource.ApplicationErrorText);

            return NotificationErrorResult(errorMessage);
        }

        #endregion Methods
    }
}