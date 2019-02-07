using COAssistance.COMMONS.Models;
using COAssistance.COMMONS.Models.ClientPassword;
using COAssistance.COMMONS.Models.Clients;
using COAssistance.COMMONS.Models.EmergencyContactNumbers;
using COAssistance.COMMONS.Models.Maintenance;
using COAssistance.COMMONS.Resources;
using COAssistance.WEB.Constants;
using COAssistance.WEB.Infrastructure;
using COAssistance.WEB.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace COAssistance.WEB.Controllers
{
    /// <summary>
    /// Client controller
    /// </summary>
    [AuthorizeToken]
    [RoutePrefix("Clients")]
    public class ClientController : BaseController
    {
        #region Fields

        private readonly IHttpClientService _httpClientService;
        private readonly ICommonService _commonService;

        #endregion Fields

        #region Constructor

        public ClientController(
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
        [Route("", Name = ClientRoutes.Index)]
        public ActionResult Index() => View();

        /// <summary>
        /// Returns data for home page using asynchronous call
        /// </summary>
        /// <param name="searchText">Search parameter</param>
        /// <param name="page">Current page</param>
        /// <returns>ActionResult</returns>
        [Route("data/{page:int=1}/{searchText=}", Name = ClientRoutes.Data)]
        public async Task<ActionResult> Data(int page = 1, string searchText = "")
        {
            var response = await _httpClientService
                .GetJsonContent(_commonService.GenerateQueryStringUrl(
                    "api/clients/registered",
                    Parameters.Pair("page", page),
                    Parameters.Pair("pageSize", AssistanceConstants.PageSize),
                    Parameters.Pair("searchParameter", searchText)));

            var clients = new ClientsManageModel
            {
                SearchText = searchText,
                PagingResult = await response.Content.ReadAsAsync<PagerModel<ClientModel>>()
            };

            return PartialView("_Data", model: clients);
        }

        /// <summary>
        /// Returns details page for selected user
        /// </summary>
        /// <param name="clientId">Identifier</param>
        /// <returns>ActionResult</returns>
        [Route("{clientId}/details", Name = ClientRoutes.Details)]
        public async Task<ActionResult> Details(string clientId)
        {
            var response = await _httpClientService.GetJsonContent($"api/clients/{clientId}/details");

            if (response.IsSuccessStatusCode)
            {
                var model = await response.Content.ReadAsAsync<ClientsDetailsModel>();

                return View("Details", model: model);
            }

            return HttpNotFound();
        }

        /// <summary>
        /// Return create modal dialog
        /// </summary>
        /// <returns>ActionResult</returns>
        [HttpGet]
        [Route("create", Name = ClientRoutes.Create)]
        public async Task<ActionResult> Create()
        {
            var response = await _httpClientService.GetJsonContent("api/maintenance/adminsettings");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<MaintenanceEditModel>();

                var model = new ClientCreateModel
                {
                };

                return PartialView("_Create", model: model);
            }

            return HttpNotFound();
        }

        /// <summary>
        /// Saves new record into the database
        /// </summary>
        /// <param name="model">Create model</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClientCreateModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("_Create", model: model);

            var response = await _httpClientService.SendRequest("api/clients/create", HttpMethod.Post, model);

            if (response.IsSuccessStatusCode)
                return NotificationSuccessResult(Resource.RecordEditSuccess, Url.RouteUrl(ClientRoutes.Data));

            var errorMessage = await _commonService.CheckForValidationErrors(response, Resource.ApplicationErrorText);

            ModelState.AddModelError(string.Empty, errorMessage);

            return PartialView("_Create", model: model);
        }

        /// <summary>
        /// Returns emergency phone numbers for selected client
        /// </summary>
        /// <param name="clientId">Client identifier</param>
        /// <returns>ActionResult</returns>
        [Route("{clientId}/emergency-numbers", Name = ClientRoutes.EmergencyNumbers)]
        public async Task<ActionResult> EmergencyNumbers(string clientId)
        {
            var response = await _httpClientService
                .GetJsonContent($"api/emergency/numbers/{clientId}");

            var emergencyNumbers = await response.Content.ReadAsAsync<IEnumerable<EmergencyContactNumberModel>>();

            return PartialView("_EmergencyNumbers", model: emergencyNumbers);
        }

        /// <summary>
        /// Returns edit modal dialog for selected client
        /// </summary>
        /// <param name="clientId">Identifier</param>
        /// <returns>ActionResult</returns>
        [Route("{clientId}/edit", Name = ClientRoutes.Edit)]
        public async Task<ActionResult> Edit(string clientId)
        {
            var response = await _httpClientService.GetJsonContent($"api/clients/{clientId}/details");

            if (response.IsSuccessStatusCode)
            {
                var model = await response.Content.ReadAsAsync<ClientEditModel>();

                return PartialView("_Edit", model: model);
            }

            return HttpNotFound();
        }

        /// <summary>
        /// Saves changes to database
        /// </summary>
        /// <param name="model">Edit model</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [Route("edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ClientEditModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("_Edit", model: model);

            var response = await _httpClientService.SendRequest("api/clients", HttpMethod.Put, model);

            if (response.IsSuccessStatusCode)
                return NotificationSuccessResult(Resource.RecordEditSuccess, Url.RouteUrl(ClientRoutes.Data));

            var errorMessage = await _commonService.CheckForValidationErrors(response, Resource.ApplicationErrorText);

            ModelState.AddModelError(string.Empty, errorMessage);

            return PartialView("_Edit", model: model);
        }

        /// <summary>
        /// Returns password reset modal dialog for selected user
        /// </summary>
        /// <param name="clientId">Identifier</param>
        /// <returns>ActionResult</returns>
        [Route("{clientId}/reset-password", Name = ClientRoutes.ResetPassword)]
        public async Task<ActionResult> ResetPassword(string clientId)
        {
            var response = await _httpClientService.GetJsonContent("api/maintenance/adminsettings");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsAsync<MaintenanceEditModel>();
                var passwordLength = content != null ? content.PasswordLength : 0;
                var specialCharacters = content != null ? content.PasswordSpecialCharacters : 0;

                var model = new ClientsResetPasswordModel
                {
                    Id = clientId,
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
        /// Submit password change to dabase
        /// </summary>
        /// <param name="model">ResetPassword model</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("reset-password")]
        public async Task<ActionResult> ResetPassword(ClientsResetPasswordModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("_ResetPassword", model: model);

            var response = await _httpClientService
                .SendRequest("api/user/maintenance/resetpassword/internal", HttpMethod.Put, model);

            if (response.IsSuccessStatusCode)
                return NotificationSuccessResult(Resource.UserPasswordResetSuccess);

            var errorMessage = await _commonService.CheckForValidationErrors(response, Resource.ApplicationErrorText);

            return NotificationErrorResult(errorMessage);
        }

        /// <summary>
        /// Change profile status for selected user
        /// </summary>
        /// <param name="model">Confirmation model</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("change-status", Name = ClientRoutes.ChangeStatus)]
        public async Task<ActionResult> ChangeStatus(ConfirmationModel model)
        {
            var response = await _httpClientService.SendRequest("api/clients/status/update", HttpMethod.Put, model.Id);

            if (response.IsSuccessStatusCode)
                return NotificationSuccessResult(Resource.RecordEditSuccess, Url.RouteUrl(ClientRoutes.Data));

            var errorMessage = await _commonService.CheckForValidationErrors(response, Resource.ApplicationErrorText);

            return NotificationErrorResult(errorMessage, Url.RouteUrl(ClientRoutes.Data));
        }

        #endregion Methods
    }
}