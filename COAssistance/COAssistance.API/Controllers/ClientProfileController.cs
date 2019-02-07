using COAssistance.API.Services.ClientService;
using COAssistance.COMMONS.Models.Clients;
using COAssistance.COMMONS.Models.FirebaseTokens;
using COAssistance.DATA.Model;
using System.Threading.Tasks;
using System.Web.Http;

namespace COAssistance.API.Controllers
{
    [RoutePrefix("api/client/profile")]
    [Authorize(Roles = EntityRoles.ClientRole)]
    public class ClientProfileController : BaseController
    {
        #region Fields

        private IClientService _clientService;

        #endregion Fields

        #region Constructor

        public ClientProfileController(IClientService clientService)
        {
            _clientService = clientService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetProfileDetails()
        {
            var client = await _clientService.FindById(UserId);
            var result = new ClientsDetailsModel
            {
                Id = client.ClientId,
                Email = client.Email,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                UserName = client.UserLoginData.UserName,
                CardNumber = client.UserLoginData.OriginUsername,
                Address = client.Address,
                City = client.City,
                BloodType = client.BloodType,
                ChronicDiseases = client.ChronicDiseases,
                ConfigurationGroup = client.ClientConfigurationGroup.ConfigurationGroupEnum,
                SupportNumber = client.ClientConfigurationGroup.SupportNumber,
                Diagnose = client.Diagnose,
                HistoryOfCriticalIllness = client.HistoryOfCriticalIllness,
                IsActive = client.UserLoginData.IsActive
            };




            return Ok(result);
        }

        [HttpPut]
        [Route("firebase")]
        public IHttpActionResult UpdateFirebaseToken([FromBody] FirebaseTokenCreateModel firebaseModel)
        {
            _clientService.AddFirebaseToken(firebaseModel, UserId);
            return Ok();
        }

        [HttpPut]
        [Route("account")]
        public IHttpActionResult UpdateUserAccountInformation([FromBody] ClientAccountUpdateModel clientAccountUpdateModel)
        {

            var result = _clientService.UpdateUserProfile(clientAccountUpdateModel, UserId);
            if (result)
                return Ok();
            else
                return NotFound();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async  Task<IHttpActionResult> Register([FromBody] MobileClientCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var username = model.CardNumber.Replace(" ", string.Empty);
            var user = await _clientService.FindByUsername(username);
            if (user != null)
            {
                ModelState.AddModelError("register_error", "Korisničko ime je zauzeto.");
                return BadRequest(ModelState);
            }

            var result = await _clientService.Create(model);
            if (result)
            {
                return Ok();
            }
            else return InternalServerError();            
        }
        #endregion Methods
    }
}