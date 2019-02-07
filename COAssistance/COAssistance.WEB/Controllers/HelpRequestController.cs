using COAssistance.COMMONS.Models;
using COAssistance.COMMONS.Models.HelpRequestResponse;
using COAssistance.COMMONS.Models.HelpRequests;
using COAssistance.COMMONS.Resources;
using COAssistance.WEB.Constants;
using COAssistance.WEB.Factories;
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
    /// HelpRequest controller
    /// </summary>
    [AuthorizeToken]
    [RoutePrefix("Help-Requests")]
    public class HelpRequestController : BaseController
    {
        #region Fields

        private readonly IHttpClientService _httpClientService;
        private readonly ICommonService _commonService;
        private readonly IHelpRequestFactory _helpRequestFactory;

        #endregion Fields

        #region Constructor

        public HelpRequestController(
            IHttpClientService httpClientService,
            ICommonService commonService,
            IHelpRequestFactory helpRequestFactory)
        {
            _httpClientService = httpClientService;
            _commonService = commonService;
            _helpRequestFactory = helpRequestFactory;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Returns data for home page using asynchronous call
        /// </summary>
        /// <param name="helpRequestId">Help request identifier</param>
        /// <returns>ActionResult</returns>
        [Route("{helpRequestId:int=0}", Name = HelpRequestsRoutes.Index)]
        public ActionResult Index(int helpRequestId = 0)
        {
            var url = helpRequestId > 0 ?
                Url.RouteUrl(HelpRequestsRoutes.Details, new { helpRequestId }) :
                Url.RouteUrl(HelpRequestsRoutes.Data);

            return View(model: url);
        }

        /// <summary>
        /// Returns data for home page using asynchronous call
        /// </summary>
        /// <param name="helpRequestCategory">Help request category</param>
        /// <param name="page">Current page</param>
        /// <returns>ActionResult</returns>
        [Route("data/{helpRequestCategory=0}/{page:int=1}", Name = HelpRequestsRoutes.Data)]
        public async Task<ActionResult> Data(HelpRequestCategory helpRequestCategory = 0, int page = 1)
        {
            var response = await _httpClientService
                .GetJsonContent(_commonService.GenerateQueryStringUrl(
                    "api/helprequests/all",
                    Parameters.Pair("helpRequestCategory", helpRequestCategory),
                    Parameters.Pair("page", page),
                    Parameters.Pair("pageSize", AssistanceConstants.PageSize)));

            var model = new HelpRequestsManageModel
            {
                PagingResult = await response.Content.ReadAsAsync<PagerModel<HelpRequestsModel>>(),
                HelpRequestCategory = helpRequestCategory
            };

            return PartialView("_Data", model: model);
        }

        /// <summary>
        /// Returns details modal dialog for selected help request
        /// </summary>
        /// <param name="helpRequestId">Identifier</param>
        /// <returns>ActionResult</returns>
        [Route("{helpRequestId:int}/details", Name = HelpRequestsRoutes.Details)]
        public async Task<ActionResult> Details(int helpRequestId)
        {
            var response = await _httpClientService
                .GetJsonContent($"api/helprequests/{helpRequestId}");

            if (response.IsSuccessStatusCode)
            {
                var model = new HelpRequestsManageDetailsModel
                {
                    Details = await response.Content.ReadAsAsync<HelpRequestsDetailsModel>(),
                    CreateModel = new HelpRequestResponseCreateModel
                    {
                        ExpirationDateUtc = DateTime.Now
                    }
                };

                model.CreateModel.Language = model.Details.ClientsLanguage;
                model.CreateModel.ExpirationTime = model.CreateModel.ExpirationDateUtc.ToString("HH:mm");

                await _helpRequestFactory.PrepareResponseModel(model.CreateModel);

                return PartialView("_Details", model: model);
            }

            return HttpNotFound();
        }

        /// <summary>
        /// Returns responses modal dialog for selected help request
        /// </summary>
        /// <param name="helpRequestId">Identifier</param>
        /// <returns>ActionResult</returns>
        [Route("{helpRequestId:int}/responses", Name = HelpRequestsRoutes.Responses)]
        public async Task<ActionResult> Responses(int helpRequestId)
        {
            var response = await _httpClientService
                .GetJsonContent($"api/helprequests/{helpRequestId}/responses");

            var model = await response.Content.ReadAsAsync<IEnumerable<HelpRequestResponseModel>>();

            return PartialView("_Responses", model: model);
        }

        /// <summary>
        /// Returns response details modal dialog for selected response
        /// </summary>
        /// <param name="responseId">Identifier</param>
        /// <returns>ActionResult</returns>
        [Route("{helpRequestId:int}/response/{responseId:int}/details", Name = HelpRequestsRoutes.ResponseDetails)]
        public async Task<ActionResult> ResponseDetails(int responseId)
        {
            var response = await _httpClientService
                .GetJsonContent($"api/helprequest/response/{responseId}/details");

            var model = await response.Content.ReadAsAsync<HelpRequestResponseModel>();

            return PartialView("_ResponseDetails", model: model);
        }

        /// <summary>
        /// Saves new record into the database
        /// </summary>
        /// <param name="model">Create model</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [Route("createResponse")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateResponse(HelpRequestResponseCreateModel model)
        {
            async Task<ActionResult> RedisplayResult()
            {
                await _helpRequestFactory.PrepareResponseModel(model);

                return PartialView("_CreateResponse", model: model);
            }

            if (!ModelState.IsValid)
                return await RedisplayResult();

            var newDate = model.ExpirationDateUtc.Add(TimeSpan.Parse(model.ExpirationTime));

            model.ExpirationDateUtc = newDate.ToUniversalTime();

            var response = await _httpClientService.SendRequest("api/helprequest/response", HttpMethod.Post, model);

            if (response.IsSuccessStatusCode)
            {
                return NotificationSuccessResult(Resource.RecordAddSuccess, Url.RouteUrl(HelpRequestsRoutes.Responses, new { model.HelpRequestId }));
            }

            var errorMessage = await _commonService.CheckForValidationErrors(response, Resource.ApplicationErrorText);

            ModelState.AddModelError(string.Empty, errorMessage);

            return await RedisplayResult();
        }

        /// <summary>
        /// Marks selected help request as completed
        /// </summary>
        /// <param name="model">Confirmation model</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("complete", Name = HelpRequestsRoutes.Complete)]
        public async Task<ActionResult> Complete(ConfirmationModel model)
        {
            var response = await _httpClientService
                .SendRequest("api/helprequests", HttpMethod.Put, model.Id);

            if (response.IsSuccessStatusCode)
                return NotificationSuccessResult(Resource.RecordEditSuccess, Url.RouteUrl(HelpRequestsRoutes.Data));

            var errorMessage = await _commonService.CheckForValidationErrors(response, Resource.ApplicationErrorText);

            return NotificationErrorResult(errorMessage, Url.RouteUrl(HelpRequestsRoutes.Data));
        }

        /// <summary>
        /// Activates selected help request
        /// </summary>
        /// <param name="responseId">Response identifier</param>
        /// <param name="helpRequestId">Request identifier</param>
        /// <returns>ActionResult</returns>
        [Route("{helpRequestId:int}/response/{responseId:int}/activate", Name = HelpRequestsRoutes.ResponseActivation)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResponseActivate(int responseId, int helpRequestId)
        {
            var model = new HelpRequestActivationModel
            {
                HelpRequestId = helpRequestId,
                HelpRequestResponseId = responseId
            };

            var response = await _httpClientService
                .SendRequest("api/helprequest/response", HttpMethod.Put, model);

            if (response.IsSuccessStatusCode)
                return NotificationSuccessResult(Resource.RecordEditSuccess, Url.RouteUrl(HelpRequestsRoutes.Responses, new { model.HelpRequestId }));

            var errorMessage = await _commonService.CheckForValidationErrors(response, Resource.ApplicationErrorText);

            return NotificationErrorResult(errorMessage, Url.RouteUrl(HelpRequestsRoutes.Responses, new { model.HelpRequestId }));
        }

        #endregion Methods
    }
}