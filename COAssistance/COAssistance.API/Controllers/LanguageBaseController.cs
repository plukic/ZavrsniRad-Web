using COAssistance.API.Services.LanguageService;
using COAssistance.DATA.Model;
using System.Collections.Generic;
using System.Linq;

namespace COAssistance.API.Controllers
{
    public class LanguageBaseController : BaseController
    {
        #region Fields

        private ILanguageService languageService;

        #endregion Fields

        #region Constructor

        public LanguageBaseController(ILanguageService languageService)
        {
            this.languageService = languageService;
        }

        #endregion Constructor

        #region Methods

        protected Language Getlanguage()
        {
            Request.Headers.TryGetValues("Accept-Language", out IEnumerable<string> languages);

            var langName = languages.FirstOrDefault();
            if (langName == null)
                return languageService.GetDefaultLanguage();

            return languageService.GetByCodeName(langName.ToString());
        }

        protected Language Language => Getlanguage();
        #endregion Methods
    }
}
