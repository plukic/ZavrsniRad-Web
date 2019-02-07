using COAssistance.COMMONS.Models.Maintenance;
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
    /// Maintenance controller
    /// </summary>
    [AuthorizeToken]
    [RoutePrefix("Maintenance")]
    public class MaintenanceController : BaseController
    {
        #region Fields

        private readonly IHttpClientService _httpClientService;
        private readonly ICommonService _commonService;

        #endregion Fields

        #region Constructor

        public MaintenanceController(
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
        [Route("", Name = MaintenanceRoutes.Index)]
        public ActionResult Index() => View();

        /// <summary>
        /// Returns data for home page using asynchronous call
        /// </summary>
        /// <returns>ActionResult</returns>
        [Route("data", Name = MaintenanceRoutes.Data)]
        public async Task<ActionResult> Data()
        {
            var response = await _httpClientService.GetJsonContent("api/maintenance");

            var model = await response.Content.ReadAsAsync<IEnumerable<MaintenanceModel>>();

            return PartialView("_Data", model: model);
        }

        /// <summary>
        /// Returns edit modal dialog for selected record
        /// </summary>
        /// <param name="maintenanceId">Identifier</param>
        /// <returns>ActionResult</returns>
        [Route("{maintenanceId:int}/edit", Name = MaintenanceRoutes.Edit)]
        public async Task<ActionResult> Edit(int maintenanceId)
        {
            var response = await _httpClientService
                .GetJsonContent($"api/maintenance/{maintenanceId}");

            if (response.IsSuccessStatusCode)
            {
                var model = await response.Content.ReadAsAsync<MaintenanceEditModel>();

                return PartialView("_Edit", model: model);
            }

            return HttpNotFound();
        }

        /// <summary>
        /// Saves changes to datbase
        /// </summary>
        /// <param name="model">Edit model</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [Route("edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(MaintenanceEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_Edit", model: model);
            }

            var response = await _httpClientService.SendRequest("api/maintenance", HttpMethod.Put, model);

            if (response.IsSuccessStatusCode)
            {
                return NotificationSuccessResult(Resource.RecordEditSuccess, Url.RouteUrl(MaintenanceRoutes.Data));
            }

            var errorMessage = await _commonService.CheckForValidationErrors(response, Resource.ApplicationErrorText);

            return NotificationErrorResult(errorMessage, Url.RouteUrl(MaintenanceRoutes.Data));
        }

        #endregion Methods
    }
}