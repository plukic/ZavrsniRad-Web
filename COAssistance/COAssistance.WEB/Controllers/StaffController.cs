using COAssistance.COMMONS.Models.Maintenance;
using COAssistance.COMMONS.Models.Staff;
using COAssistance.COMMONS.Resources;
using COAssistance.WEB.Constants;
using COAssistance.WEB.Infrastructure;
using COAssistance.WEB.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace COAssistance.WEB.Controllers
{
    /// <summary>
    /// Staff controller
    /// </summary>
    [AuthorizeToken]
    [RoutePrefix("Staff")]
    public class StaffController : BaseController
    {
        #region Fields

        private readonly IHttpClientService _httpClientService;
        private readonly ICommonService _commonService;

        #endregion Fields

        #region Constructor

        public StaffController(
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
        /// <returns>ActionResult</returns>
        [Route("", Name = StaffRoutes.Index)]
        public ViewResult Index() => View();

        /// <summary>
        /// Returns data for home page using asynchronous call
        /// </summary>
        /// <returns>ActionResult</returns>
        [Route("data", Name = StaffRoutes.Data)]
        public async Task<PartialViewResult> Data()
        {
            var response = await _httpClientService.GetJsonContent("api/admin");

            var staff = await response.Content.ReadAsAsync<IEnumerable<StaffModel>>();

            return PartialView("_Data", model: staff);
        }

        /// <summary>
        /// Returns create modal dialog
        /// </summary>
        /// <returns>ActionResult</returns>
        [Route("create", Name = StaffRoutes.Create)]
        public PartialViewResult Create() => PartialView("_Create", new StaffCreateModel());

        /// <summary>
        /// Saves new record into the database
        /// </summary>
        /// <param name="model">Create model</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StaffCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Create", model: model);
            }

            var result = await _httpClientService.SendRequest("api/admin", HttpMethod.Post, model);

            if (result.IsSuccessStatusCode)
            {
                return NotificationSuccessResult(Resource.RecordAddSuccess, Url.RouteUrl(StaffRoutes.Data));
            }

            var errorMessage = await _commonService.CheckForValidationErrors(result, Resource.ApplicationErrorText);

            return NotificationErrorResult(errorMessage, Url.RouteUrl(StaffRoutes.Data));
        }

        /// <summary>
        /// Saves changed status to database
        /// </summary>
        /// <param name="staffId">Identifier</param>
        /// <param name="status">Selected status</param>
        /// <returns>ActionResult</returns>
        [Route("{staffId}/change-status/{status}", Name = StaffRoutes.ChangeStatus)]
        public async Task<ActionResult> ChangeStatus(string staffId, bool status)
        {
            var model = new StaffChangeStatusModel
            {
                Id = staffId,
                IsActive = status
            };

            var result = await _httpClientService.SendRequest("api/admin/status", HttpMethod.Put, model);

            if (result.IsSuccessStatusCode)
            {
                return NotificationSuccessResult(status ? Resource.RecordActive : Resource.RecordInactive, Url.RouteUrl(StaffRoutes.Data));
            }

            var errorMessage = await _commonService.CheckForValidationErrors(result, Resource.ApplicationErrorText);

            return NotificationErrorResult(errorMessage, Url.RouteUrl(StaffRoutes.Data));
        }

        /// <summary>
        /// Returns reset password modal dialog for selected user
        /// </summary>
        /// <param name="staffId">Identifier</param>
        /// <returns>ActionResult</returns>
        [Route("{staffId}/reset-password", Name = StaffRoutes.ResetPassword)]
        public async Task<ActionResult> ResetPassword(string staffId)
        {
            var response = await _httpClientService.GetJsonContent("api/maintenance/adminsettings");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<MaintenanceEditModel>();
                var passwordLength = content != null ? content.PasswordLength : 0;
                var specialCharacters = content != null ? content.PasswordSpecialCharacters : 0;

                var model = new StaffResetPasswordModel
                {
                    Id = staffId,
                    PasswordLength = passwordLength,
                    PasswordSpecialCharacters = specialCharacters,
                    Length = passwordLength,
                    SpecialCharacters = specialCharacters
                };

                return PartialView("_ResetPassword", model: model);
            }

            return HttpNotFound();
        }

        /// <summary>
        /// Saves changed password to database
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [Route("reset-password")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(StaffResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ResetPassword", model: model);
            }

            var result = await _httpClientService.SendRequest("api/admin/password/reset", HttpMethod.Put, model);

            if (result.IsSuccessStatusCode)
            {
                return NotificationSuccessResult(Resource.UserPasswordResetSuccess);
            }

            var errorMessage = await _commonService.CheckForValidationErrors(result, Resource.ApplicationErrorText);

            return NotificationErrorResult(errorMessage);
        }

        #endregion Methods
    }
}