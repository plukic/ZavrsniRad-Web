using COAssistance.API.Services.ApiClient;
using COAssistance.API.Services.ClientConfiguration;
using COAssistance.API.Services.ClientService;
using COAssistance.API.Services.HelpRequestResponseService;
using COAssistance.API.Services.HelpRequestService;
using COAssistance.API.Services.LanguageService;
using COAssistance.API.Services.NotificationService;
using COAssistance.COMMONS.Models.HelpRequestResponse;
using COAssistance.COMMONS.Models.Notification;
using COAssistance.DATA.Model;
using COAssistance.DATA.Model.HelpRequests;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace COAssistance.API.Controllers
{
    [RoutePrefix("api/helprequest/response")]
    public class HelpRequestResponseController : LanguageBaseController
    {
        #region Fields
        private readonly IHelpRequestResponseService _helpRequestResponseService;
        private readonly IHelpRequestService _helpRequestService;
        private readonly IClientConfigurationService _clientConfigurationService;
        private readonly IClientService _clientService;
        private readonly INotificationService _notificationService;
        #endregion Fields

        #region Constructor

        public HelpRequestResponseController(
            ILanguageService languageService,
            IHelpRequestResponseService helpRequestResponseService,
            IClientConfigurationService clientConfigurationService,
            IHelpRequestService helpRequestService,
            IClientService clientService,
            INotificationService notificationService)
            : base(languageService)
        {
            _clientService = clientService;
            _helpRequestService = helpRequestService;
            _helpRequestResponseService = helpRequestResponseService;
            _clientConfigurationService = clientConfigurationService;
            _notificationService = notificationService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        [Route("")]
        [Authorize(Roles = EntityRoles.ClientRole)]
        public IHttpActionResult GetUserHelpRequestResponse()
        {
            var helpRequest = _helpRequestResponseService.GetClientHelpRequestResponse(UserId);
            if (helpRequest == null)
                return NotFound();
            return Ok(MapHelpRequestResponse(helpRequest, UserId));
        }

        [HttpGet]
        [Route("{helpRequestResponseId}/details")]
        [Authorize(Roles = EntityRoles.AdminRole)]
        public IHttpActionResult GetDetails(int helpRequestResponseId)
        {
            var result = _helpRequestResponseService.GetDetails(helpRequestResponseId);

            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = EntityRoles.AdminRole)]
        public async Task<IHttpActionResult> CreateHelpRequestResponse(/*[FromBody]*/HelpRequestResponseCreateModel model)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, GetValidationErrors());

            var helpRequest = _helpRequestService.GetById(model.HelpRequestId);

            if (helpRequest == null)
                return NotFound();

            var helpRequestResponse = _helpRequestResponseService.CreateClientHelpRequestResponse(model);

            if (helpRequestResponse == null)
                return InternalServerError();

            var client = await _clientService.FindById(helpRequest.ClientId);

            var result = MapHelpRequestResponse(helpRequestResponse, client.ClientId);
      
            bool isSuccess =   await _notificationService.SendHelpRequestNotification(client.ClientId, result);

            if (isSuccess)
            {
                    return Ok();
            }
            return InternalServerError();
        }

        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Activate(HelpRequestActivationModel model)
        {
            var client = _helpRequestResponseService.GetHelpRequestResponseClient(model.HelpRequestResponseId);
            _helpRequestResponseService.Activate(model);

            var newRequest = _helpRequestResponseService.GetClientHelpRequestResponse(client.ClientId);
            var result = await _notificationService.SendHelpRequestNotification(client.ClientId, newRequest);
            if (result)
                return Ok();

            return InternalServerError();
        }

        #endregion Methods

        #region Utils

        private HelpRequestResponseModel MapHelpRequestResponse(HelpRequestResponse helpRequest, string userId)
        {
            var clientConfiguration = _clientConfigurationService.GetByUserId(userId);

            var result = new HelpRequestResponseModel
            {
                ClientId = UserId,
                ContactPhoneNumber = clientConfiguration.SupportNumber,
                HelpIncomingDateTime = helpRequest.HelpIncomingDateTimeUtc,
                HelpRequestState = helpRequest.HelpRequestState,
                ShortInstructions = helpRequest.ShortInstruction
            };
            return result;
        }

        #endregion Utils
    }
}