using COAssistance.COMMONS.Models.UserGroups;
using COAssistance.COMMONS.Resources;
using COAssistance.WEB.Constants;
using COAssistance.WEB.Factories;
using COAssistance.WEB.Infrastructure;
using COAssistance.WEB.Services;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace COAssistance.WEB.Controllers
{
    /// <summary>
    /// UserGroups controller
    /// </summary>
    [AuthorizeToken]
    [RoutePrefix("User-Groups")]
    public class UserGroupsController : BaseController
    {
        #region Fields

        private readonly IHttpClientService _httpClientService;
        private readonly ICommonService _commonService;
        private readonly IUserGroupsFactory _userGroupsFactory;

        #endregion Fields

        #region Constructor

        public UserGroupsController(
            IHttpClientService httpClientService,
            ICommonService commonService,
            IUserGroupsFactory userGroupsFactory)
        {
            _httpClientService = httpClientService;
            _commonService = commonService;
            _userGroupsFactory = userGroupsFactory;
        }

        #endregion Constructor

        #region Methods

        /// <summary>
        /// Returns home page for this controller
        /// </summary>
        /// <returns>ActionResult</returns>
        [Route("", Name = UserGroupsRoutes.Index)]
        public ActionResult Index() => View();

        /// <summary>
        /// Returns data for home page using asynchronous call
        /// </summary>
        /// <returns>ActionResult</returns>
        [Route("data", Name = UserGroupsRoutes.Data)]
        public async Task<ActionResult> Data()
        {
            var response = await _httpClientService.GetJsonContent("api/clientconfiguration");

            var model = await response.Content.ReadAsAsync<IEnumerable<UserGroupsModel>>();

            return PartialView("_Data", model: model);
        }

        /// <summary>
        /// Returns edit modal dialog for selected group
        /// </summary>
        /// <param name="groupId">Identifier</param>
        /// <returns>ActionResult</returns>
        [Route("{groupId:int}/edit", Name = UserGroupsRoutes.Edit)]
        public async Task<ActionResult> Edit(int groupId)
        {
            var response = await _httpClientService.GetJsonContent($"api/clientconfiguration/{groupId}");

            if (response.IsSuccessStatusCode)
            {
                var model = await response.Content.ReadAsAsync<UserGroupsEditModel>();

                await _userGroupsFactory.PrepareModel(model);

                return PartialView("_Edit", model: model);
            }

            return HttpNotFound();
        }

        /// <summary>
        /// Saves changes to dabase
        /// </summary>
        /// <param name="model">Edit model</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [Route("edit")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(UserGroupsEditModel model)
        {
            if (!ModelState.IsValid)
            {
                await _userGroupsFactory.PrepareModel(model);

                return PartialView("_Edit", model: model);
            }

            var result = await _httpClientService.SendRequest("api/ClientConfiguration", HttpMethod.Put, model);

            if (result.IsSuccessStatusCode)
                return NotificationSuccessResult(Resource.RecordEditSuccess, Url.RouteUrl(UserGroupsRoutes.Data));

            var errorMessage = await _commonService.CheckForValidationErrors(result, Resource.ApplicationErrorText);

            return NotificationErrorResult(errorMessage, Url.RouteUrl(UserGroupsRoutes.Data));
        }

        #endregion Methods
    }
}