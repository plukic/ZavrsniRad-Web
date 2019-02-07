using COAssistance.COMMONS.Clients;
using COAssistance.COMMONS.Models;
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
    /// ClientLoginData controller
    /// </summary>
    [AuthorizeToken]
    [RoutePrefix("Client-Login-Data")]
    public class ClientLoginDataController : BaseController
    {
        #region Fields

        private readonly IHttpClientService _httpClientService;
        private readonly ICommonService _commonService;

        #endregion Fields

        #region Constructor

        public ClientLoginDataController(
            IHttpClientService httpClientService,
            ICommonService commonService)
        {
            _httpClientService = httpClientService;
            _commonService = commonService;
        }

        #endregion Constructor

        /// <summary>
        /// Returns data for home page using asynchronous call
        /// </summary>
        /// <param name="userId">User identifier</param>
        /// <returns>ActionResult</returns>
        [Route("{userId}/data", Name = ClientLoginDataRoutes.Data)]
        public async Task<ActionResult> Data(string userId)
        {
            var response = await _httpClientService
                .GetJsonContent($"api/clients/logindata/{userId}");

            var clientLoginData = await response.Content.ReadAsAsync<IEnumerable<ClientLoginData>>();

            return PartialView("_Data", model: clientLoginData);
        }

        /// <summary>
        /// Deactivates selected client
        /// </summary>
        /// <param name="model">Confirmation model</param>
        /// <returns>ActionResult</returns>
        [Route("deactivate", Name = ClientLoginDataRoutes.Deactivate)]
        public async Task<ActionResult> Deactivate(ConfirmationModel model)
        {
            var response = await _httpClientService
                .SendRequest($"api/clients/logindata/deactivate", HttpMethod.Put, model.Id);

            if (response.IsSuccessStatusCode && model.AdditionalData != null)
            {
                var userId = (string[])model.AdditionalData;

                return NotificationSuccessResult(Resource.RecordEditSuccess, Url.RouteUrl(ClientLoginDataRoutes.Data, new { userId = userId[0] }));
            }

            var errorMessage = await _commonService.CheckForValidationErrors(response, Resource.ApplicationErrorText);

            return NotificationErrorResult(errorMessage);
        }
    }
}