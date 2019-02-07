using COAssistance.COMMONS.Models.Languages;
using COAssistance.DATA.Model;
using System.Collections.Generic;

namespace COAssistance.API.Services.LanguageService
{
    public interface ILanguageService
    {
        #region Methods

        IEnumerable<Language> GetAll(bool trackEntites = true);

        IEnumerable<LanguageDropDownViewModel> GetDropDownData();

        Language GetDefaultLanguage();

        Language GetByCodeName(string langCode);

        #endregion Methods
    }
}