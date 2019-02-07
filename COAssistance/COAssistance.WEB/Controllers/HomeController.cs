using COAssistance.COMMONS.Models.Dashboard;
using COAssistance.WEB.Constants;
using COAssistance.WEB.Infrastructure;
using COAssistance.WEB.Services;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace COAssistance.WEB.Controllers
{
    /// <summary>
    /// Home controller
    /// </summary>
    [AuthorizeToken]
    [RoutePrefix("Home")]
    public class HomeController : BaseController
    {
        #region Fields

        private readonly IHttpClientService _httpClientService;

        #endregion Fields

        #region Constructor

        public HomeController(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Returns home page for this controller
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [Route("~/", Name = HomeRoutes.Index)]
        public ActionResult Index() => View();

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Route("data", Name = HomeRoutes.Data)]
        public async Task<ActionResult> Data()
        {
            var response = await _httpClientService.GetJsonContent("api/dashboard");

            var result = await response.Content.ReadAsAsync<DashboardModel>();

            result.HelpRequest.OrderBy(x => x.RequestDate);

            return PartialView("_Data", model: result);
        }

        #endregion Methods
    }
}