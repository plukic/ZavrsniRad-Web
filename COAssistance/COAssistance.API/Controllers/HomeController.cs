using System.Web.Mvc;

namespace COAssistance.API.Controllers
{
    public class HomeController : Controller
    {
        #region Methods

        public ActionResult Index()
        {
            return View();
        }

        #endregion Methods
    }
}