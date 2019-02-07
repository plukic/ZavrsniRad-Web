using COAssistance.API.Services.LanguageService;
using System.Web.Http;

namespace COAssistance.API.Controllers
{
    /// <summary>
    /// Language Controller
    /// </summary>
    [RoutePrefix("api/language")]
    public class LanguageController : LanguageBaseController
    {
        #region Fields

        private readonly ILanguageService _languageService;

        #endregion Fields

        #region Constructor

        public LanguageController(ILanguageService languageService)
            : base(languageService)
        {
            _languageService = languageService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        [Route("test")]
        public IHttpActionResult GetTest()
        {
            var lang = Getlanguage();
            return Ok(lang.LanguageName);
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_languageService.GetAll(trackEntites: false));
        }

        [HttpGet]
        [Route("DropDownData")]
        public IHttpActionResult GetDropDownData()
        {
            return Ok(_languageService.GetDropDownData());
        }

        #endregion Methods
    }
}