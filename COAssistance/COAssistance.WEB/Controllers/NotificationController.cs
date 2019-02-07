using COAssistance.COMMONS.Models.HelpRequests;
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
    /// Notification controller
    /// </summary>
    [AuthorizeToken]
    [RoutePrefix("Notifications")]
    public class NotificationController : BaseController
    {
        #region Fields

        private readonly IHttpClientService _httpClientService;
        private readonly ICommonService _commonService;

        #endregion Fields

        #region Constructor

        public NotificationController(
            IHttpClientService httpClientService,
            ICommonService commonService)
        {
            _httpClientService = httpClientService;
            _commonService = commonService;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Returns data for home page using asynchronous call
        /// </summary>
        /// <returns>ActionResult</returns>
        [Route("data", Name = NotificationRoutes.Data)]
        public async Task<ActionResult> Data()
        {
            var response = await _httpClientService.GetJsonContent("api/helprequests/unfinished");

            var model = await response.Content.ReadAsAsync<IEnumerable<HelpRequestsModel>>();

            return PartialView("_Data", model: model);
        }

        #endregion Methods
    }
}