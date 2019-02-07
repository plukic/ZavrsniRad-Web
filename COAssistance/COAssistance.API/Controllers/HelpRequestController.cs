using COAssistance.COMMONS.Models.Paging;

using COAssistance.API.Services.HelpRequestResponseService;
using COAssistance.API.Services.HelpRequestService;
using COAssistance.API.Services.NotificationService;
using COAssistance.API.Util;
using COAssistance.COMMONS.Models;
using COAssistance.COMMONS.Models.HelpRequest;
using COAssistance.COMMONS.Models.HelpRequests;
using COAssistance.DATA.Model;
using System;
using System.Net;
using System.Web.Http;

namespace COAssistance.API.Controllers
{
    [RoutePrefix("api/helprequests")]
    [Authorize(Roles = EntityRoles.AdminRole + "," + EntityRoles.ClientRole)]
    public class HelpRequestController : BaseController
    {
        #region Fields

        private readonly IHelpRequestService _helpRequestService;
        private readonly IHelpRequestResponseService _helpRequestResponseService;
        private readonly PagingResultFactory _pagingResultFactory;
        private readonly INotificationService _notificationService;

        #endregion Fields

        #region Constructor

        public HelpRequestController(IHelpRequestService helpRequestService,
            PagingResultFactory pagingResultFactory,
            INotificationService notificationService,
            IHelpRequestResponseService helpRequestResponseService)
        {
            _helpRequestService = helpRequestService;
            _pagingResultFactory = pagingResultFactory;
            _notificationService = notificationService;
            _helpRequestResponseService = helpRequestResponseService;
        }

        #endregion Constructor

        #region Methods

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateNewRequest(HelpRequestCreateModel model)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, GetValidationErrors());

            var newHelpRequest = _helpRequestService.AddNewRequest(model, UserId);
            _notificationService.PushHelpRequestCreatedNotification(newHelpRequest);

            return Ok();
        }

        [HttpGet]
        [Route("all")]
        public IHttpActionResult Get(HelpRequestCategory helpRequestCategory = 0, int page = 1, int pageSize = 15)
        {
            var query = _helpRequestService.GetAll(helpRequestCategory);

            PagingResultVM<HelpRequestsModel> pagingResult =
                _pagingResultFactory.CreatePagingResultDesc(query, pageSize, page, x => x.RequestDate, GetHelpRequestViewModelSelector());

            return Ok(pagingResult);
        }

        [HttpGet]
        [Route("unfinished")]
        public IHttpActionResult GetUnfinished()
        {
            var result = _helpRequestService.GetUnfinished();

            return Ok(result);
        }

        [HttpGet]
        [Route("{helpRequestId}")]
        public IHttpActionResult Get(int helpRequestId)
        {
            var result = _helpRequestService.GetById(helpRequestId);

            return Ok(result);
        }

        [HttpGet]
        [Route("{helpRequestId}/responses")]
        public IHttpActionResult GetRespones(int helpRequestId)
        {
            var result = _helpRequestResponseService.GetHelpRequestResponses(helpRequestId);

            return Ok(result);
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Complete([FromBody] int helpRequestId)
        {
            _helpRequestService.Complete(helpRequestId);

            return Ok();
        }

        #endregion Methods

        #region Utils

        private Func<HelpRequest, HelpRequestsModel> GetHelpRequestViewModelSelector()
        {
            return hp => new HelpRequestsModel
            {
                Id = hp.Id,
                Client = hp.Client.FirstName + " " + hp.Client.LastName,
                ClientId = hp.ClientId,
                HelpRequestCategory = hp.HelpRequestCategory,
                IsSolved = hp.IsSolved,
                RequestDate = hp.RequestDate
            };
        }

        #endregion Utils
    }
}