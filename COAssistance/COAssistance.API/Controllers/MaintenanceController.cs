using COAssistance.API.Services.MaintenanceService;
using COAssistance.API.Services.PredefinedTextTemplates;
using COAssistance.COMMONS.Models.Maintenance;
using COAssistance.DATA.Model;
using System.Net;
using System.Web.Http;

namespace COAssistance.API.Controllers
{
    /// <summary>
    /// Maintenance Controller
    /// </summary>
    [RoutePrefix("api/maintenance")]
    [Authorize(Roles = EntityRoles.AdminRole)]
    public class MaintenanceController : BaseController
    {
        #region Fields

        private readonly IMaintenanceService _maintenanceService;
        private readonly IPredefinedTextTemplateService _predefinedTextTemplateService;

        #endregion Fields

        #region Constructor

        public MaintenanceController(
            IMaintenanceService maintenanceService,
            IPredefinedTextTemplateService predefinedTextTemplateService)
        {
            _maintenanceService = maintenanceService;
            _predefinedTextTemplateService = predefinedTextTemplateService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_maintenanceService.GetAll());
        }

        [HttpGet]
        [Route("{maintenanceId}")]
        public IHttpActionResult Get(int maintenanceId)
        {
            return Ok(_maintenanceService.GetById(maintenanceId));
        }

        [HttpGet]
        [Route("templates")]
        public IHttpActionResult GetTemplates()
        {
            var adminSettings = _maintenanceService.GetAdminSettings();
            var result = _predefinedTextTemplateService.GetDropDownData(adminSettings.MaintenanceId);
            return Ok(result);
        }

        [HttpGet]
        [Route("adminsettings")]
        public IHttpActionResult GetAdminSettings()
        {
            return Ok(_maintenanceService.GetAdminSettings());
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult Edit(MaintenanceEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, GetValidationErrors());
            }

            _maintenanceService.Edit(model);

            return Ok();
        }

        #endregion Methods
    }
}