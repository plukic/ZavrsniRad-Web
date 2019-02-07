using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace COAssistance.API.Controllers
{
    /// <summary>
    /// Base Controller
    /// </summary>
    public class BaseController : ApiController
    {
        #region Utils

        /// <summary>
        /// Returns all validation errors from ModelState
        /// </summary>
        /// <returns>Collection</returns>
        protected List<KeyValuePair<string, string>> GetValidationErrors()
        {
            var values = new List<KeyValuePair<string, string>>();
            var keys = ModelState.Keys;

            foreach (var key in keys)
            {
                foreach (var error in ModelState[key].Errors)
                {
                    values.Add(new KeyValuePair<string, string>(
                        ParseKey(key),
                        string.IsNullOrEmpty(error.ErrorMessage) ? error.Exception.Message : error.ErrorMessage));
                }
            }

            return values;
        }

        /// <summary>
        /// Gets property name from key retrieved from ModelState dictionary
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Parsed value</returns>
        private string ParseKey(string key)
        {
            var segments = key.Split('.');

            return segments.Last();
        }

        #endregion Utils

        #region Properties

        protected string UserId
        {
            get
            {
                return HttpContext.Current.User.Identity.GetUserId();
            }
        }

        #endregion Properties
    }
}