using COAssistance.API.Services.ClientService;
using COAssistance.API.Services.Logger;
using COAssistance.API.Util;
using COAssistance.COMMONS.Models.Clients;
using COAssistance.COMMONS.Models.Paging;
using COAssistance.DATA.Model;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace COAssistance.API.Controllers
{
    [Authorize(Roles = EntityRoles.AdminRole)]
    [RoutePrefix("api/clients")]
    public class ClientController : BaseController
    {
        #region Fields

        private readonly PagingResultFactory _pagingResultFactory;
        private readonly IClientService _clientService;
        private readonly ILoggerService _loggerService;

        #endregion Fields

        #region Constructor

        public ClientController(
            PagingResultFactory pagingResultFactory,
            IClientService clientService,
            ILoggerService loggerService
           )
        {
            _pagingResultFactory = pagingResultFactory;
            _clientService = clientService;
            _loggerService = loggerService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        [Route("registered")]
        public IHttpActionResult Get(int page = 1, int pageSize = 20, string searchParameter = null)
        {
            var result = _clientService.GetClients(searchParameter);

            PagingResultVM<ClientModel> pagingResult =
                _pagingResultFactory.CreatePagingResult(result, pageSize, page, (x) => x.UserLoginData.UserName, GetClientViewModelSelector());

         
            return Ok(pagingResult);
        }

        [HttpPut]
        [Route("")]
        public async Task<IHttpActionResult> Edit([FromBody]ClientEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, GetValidationErrors());
            }

            var client = await _clientService.FindById(model.Id);

            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            client.FirstName = model.FirstName;
            client.UserLoginData.OriginUsername = model.CardNumber;
            client.UserLoginData.UserName = model.CardNumber.Replace(" ", string.Empty);
            client.LastName = model.LastName;
            client.PhoneNumber = model.PhoneNumber;
            client.Address = model.Address;
            client.Email = model.Email;
            client.City = model.City;

            _clientService.Edit(client);

            _loggerService.Log(UserId, ActionType.Create, $"Cliend account edited - {model.CardNumber}");

            return Ok();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> Create([FromBody]ClientCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, GetValidationErrors());
            }

            var result = await _clientService.Create(model);

            if (result)
            {
                _loggerService.Log(UserId, ActionType.Create, $"Cliend account created - {model.CardNumber}");

                return Ok();
            }

            return InternalServerError();
        }

        [HttpGet]
        [Route("{id}/details")]
        public async Task<IHttpActionResult> Details(string id)
        {
            var client = await _clientService.FindById(id);

            if (client == null)
            {
                return NotFound();
            }

            var model = new ClientsDetailsModel
            {
                Id = client.ClientId,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                FirstName = client.FirstName,
                IsActive = client.UserLoginData.IsActive,
                LastName = client.LastName,
                UserName = client.UserLoginData.UserName,
                CardNumber = client.UserLoginData.OriginUsername,
                Address = client.Address,
                City = client.City,
                BloodType = client.BloodType,
                ChronicDiseases = client.ChronicDiseases,
                Diagnose = client.Diagnose,
                ConfigurationGroup = client.ClientConfigurationGroup.ConfigurationGroupEnum,
                SupportNumber = client.ClientConfigurationGroup.SupportNumber,
                HistoryOfCriticalIllness = client.HistoryOfCriticalIllness
            };

            return Ok(model);
        }

        [HttpPut]
        [Route("status/update")]
        public IHttpActionResult StatusUpdate([FromBody] string clientId)
        {
            var result = _clientService.ChangeClientStatus(clientId);
            if (result)
                return Ok();
            return NotFound();
        }

        #endregion Methods

        #region Utils

        private Func<Client, ClientModel> GetClientViewModelSelector()
        {
            return c => new ClientModel
            {
                Id = c.ClientId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                CardMembershipNumber = c.UserLoginData.UserName,
                IsActive = c.UserLoginData.IsActive,
            };
        }

        #endregion Utils
    }
}