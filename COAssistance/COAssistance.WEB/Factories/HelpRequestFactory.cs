using COAssistance.COMMONS.Models;
using COAssistance.COMMONS.Models.HelpRequestResponse;
using COAssistance.COMMONS.Models.Languages;
using COAssistance.WEB.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace COAssistance.WEB.Factories
{
    public class HelpRequestFactory : IHelpRequestFactory
    {
        #region Fields

        private readonly IHttpClientService _httpClientService;
        private readonly ICommonService _commonService;

        #endregion Fields

        #region Constructor

        public HelpRequestFactory(
            IHttpClientService httpClientService,
            ICommonService commonService)
        {
            _httpClientService = httpClientService;
            _commonService = commonService;
        }

        #endregion Constructor

        #region Methods

        public async Task PrepareResponseModel(HelpRequestResponseCreateModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            var response = await _httpClientService.GetJsonContent("api/language/dropdowndata");

            model.Languages = new SelectList(
                await response.Content.ReadAsAsync<IEnumerable<LanguageDropDownViewModel>>(),
                nameof(LanguageDropDownViewModel.Id),
                nameof(LanguageDropDownViewModel.Name));

            var templateResponse = await _httpClientService.GetJsonContent("api/maintenance/templates");

            model.InstructionTemplates = await templateResponse.Content.ReadAsAsync<List<DropDownModel>>();
        }

        #endregion Methods
    }
}