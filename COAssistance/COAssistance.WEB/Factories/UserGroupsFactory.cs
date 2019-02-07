using COAssistance.COMMONS.Models.Languages;
using COAssistance.COMMONS.Models.UserGroups;
using COAssistance.WEB.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace COAssistance.WEB.Factories
{
    public class UserGroupsFactory : IUserGroupsFactory
    {
        #region Fields

        private readonly IHttpClientService _httpClientService;
        private readonly ICommonService _commonService;

        #endregion Fields

        #region Constructor

        public UserGroupsFactory(
            IHttpClientService httpClientService,
            ICommonService commonService)
        {
            _httpClientService = httpClientService;
            _commonService = commonService;
        }

        #endregion Constructor

        #region Methods

        public async Task PrepareModel(UserGroupsEditModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var response = await _httpClientService.GetJsonContent("api/language/dropdowndata");

            model.Languages = new SelectList(
                await response.Content.ReadAsAsync<List<LanguageDropDownViewModel>>(),
                nameof(LanguageDropDownViewModel.Id),
                nameof(LanguageDropDownViewModel.Name));
        }

        #endregion Methods
    }
}