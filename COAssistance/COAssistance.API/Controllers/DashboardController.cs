using COAssistance.API.Services.HelpRequestService;
using COAssistance.COMMONS.Models.Dashboard;
using COAssistance.DATA.Model;
using System.Web.Http;

namespace COAssistance.API.Controllers
{
    [Authorize(Roles = EntityRoles.AdminRole)]
    [RoutePrefix("api/dashboard")]
    public class DashboardController : ApiController
    {
        #region Fields

        private readonly IHelpRequestService _helpRequestService;

        #endregion Fields

        #region Constructor

        public DashboardController( IHelpRequestService helpRequestService)
        {
            _helpRequestService = helpRequestService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(int size = 10)
        {
            var helpRequests = _helpRequestService.GetLastRecords(size);

            var result = new DashboardModel
            {
                HelpRequest = helpRequests,
            };
            return Ok(result);
        }

        #endregion Methods
    }
}