using COAssistance.API.Services.ClientConfiguration;
using COAssistance.API.Services.LanguageService;
using COAssistance.COMMONS.Models;
using COAssistance.COMMONS.Models.UserGroups;
using COAssistance.DATA.Model;
using System.Net;
using System.Web.Http;

namespace COAssistance.API.Controllers
{
    [RoutePrefix("api/clientconfiguration")]
    [Authorize(Roles = EntityRoles.AdminRole)]
    public class ClientConfigurationController : BaseController
    {
        #region Fields

        private IClientConfigurationService _configurationService;
        private ILanguageService _languageService;

        #endregion Fields

        #region Constructor

        public ClientConfigurationController(IClientConfigurationService configurationService, ILanguageService languageService)
        {
            _configurationService = configurationService;
            _languageService = languageService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_configurationService.Get());
        }

        [HttpGet]
        [Route("{groupId}")]
        public IHttpActionResult Get(int groupId)
        {
            return Ok(_configurationService.GetById(groupId));
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Edit(UserGroupsEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, GetValidationErrors());
            }

            _configurationService.Edit(model);

            return Ok();
        }

        #endregion Methods

        
    }
}