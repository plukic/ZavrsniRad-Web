using COAssistance.API.Services.ClientEmergencyNumbers;
using COAssistance.COMMONS.Models.EmergencyContactNumbers;
using COAssistance.DATA.Model;
using COAssistance.DATA.Model.Clients;
using System;
using System.Linq;
using System.Web.Http;

namespace COAssistance.API.Controllers
{
    /// <summary>
    /// ClientEmergencyNumber controller
    /// </summary>
    [RoutePrefix("api/emergency/numbers")]
    [Authorize(Roles =EntityRoles.AdminRole +","+EntityRoles.ClientRole)]
    public class ClientEmergencyNumberController : BaseController
    {
        #region Fields

        private readonly IClientEmergencyNumberService _clientEmergencyNumberService;

        #endregion Fields

        #region Constructor

        public ClientEmergencyNumberController(IClientEmergencyNumberService clientEmergencyNumberService)
        {
            _clientEmergencyNumberService = clientEmergencyNumberService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        [Route("{clientId}")]
        public IHttpActionResult GetClientNumbers(string clientId)
        {
            var numbers = _clientEmergencyNumberService
                .GetClientNumbers(clientId)
                .Select(ProjectToModel());

            return Ok(numbers);
        }
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetClientNumbers()
        {
            var numbers = _clientEmergencyNumberService
                .GetClientNumbers(UserId)
                .Select(ProjectToModel()).ToList();

            return Ok(numbers);
        }
        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteClientNumber(int id)
        {
            _clientEmergencyNumberService
                .DeleteClientEmergencyNumber(id);
            return Ok();
        }
        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateClientNumber([FromBody]EmergencyContactNumbers model)
        {
            model.ClientId = UserId;
            _clientEmergencyNumberService.Create(model);
            return Ok();
        }
        [HttpPut]
        [Route("")]
        public IHttpActionResult UpdateClientNumber([FromBody]EmergencyContactNumbers model)
        {
            _clientEmergencyNumberService.Edit(model);
            return Ok();
        }

        #endregion Methods

        #region Utils

        private Func<EmergencyContactNumbers, EmergencyContactNumberModel> ProjectToModel()
        {
            return ecn => new EmergencyContactNumberModel
            {
                ContactFullName = ecn.ContactFullName,
                ClientId=ecn.ClientId,
                Id=ecn.Id,
                PhoneNumber = ecn.PhoneNumber,
            };
        }

        #endregion Utils
    }
}