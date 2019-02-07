using COAssistance.COMMONS.Models.Account;
using COAssistance.WEB.Constants;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace COAssistance.WEB.Services
{
    /// <summary>
    /// Common services implementation
    /// </summary>
    public class CommonService : ICommonService
    {
        #region Constants

        private const string ProtectionPurpose = "token.encryption";

        #endregion Constants

        #region Fields

        private readonly ICookieService _cookieService;

        #endregion Fields

        #region Constructor

        public CommonService(ICookieService cookieService)
        {
            _cookieService = cookieService;
        }

        #endregion Constructor

        #region Methods

        public string GenerateQueryStringUrl(string target, params KeyValuePair<string, object>[] parameters)
        {
            for (int i = 0; i < parameters.Count(); i++)
            {
                target += $"{(i == 0 ? "?" : "&")}{parameters[i].Key}={parameters[i].Value}";
            }

            return target;
        }

        public async Task<string> CheckForValidationErrors(HttpResponseMessage response, string replacementMessage)
        {
            var content = await response.Content.ReadAsAsync<List<KeyValuePair<string, string>>>();

            string result = string.Empty;

            if (content != null)
            {
                foreach (var item in content)
                {
                    result += item.Value;
                }
            }

            return string.IsNullOrEmpty(result) ? replacementMessage : result;
        }

        public string ResolveIcon(string fileName, bool removeRootChar = false)
        {
            var urlPrefix = ConfigurationManager.AppSettings["UrlPrefix"];

            var filePath = !string.IsNullOrEmpty(urlPrefix) ?
                $"/{urlPrefix}/{AssistanceConstants.Paths.ImagesLocation}" :
                AssistanceConstants.Paths.ImagesLocation;

            var path = this.MapPath(AppDomain.CurrentDomain.BaseDirectory, AssistanceConstants.Paths.ImagesLocation);

            if (!Directory.Exists(path))
                return string.Empty;

            var file = Directory
                .GetFiles(path)
                .Select(p => Path.GetFileName(p))
                .FirstOrDefault(p => p.Contains(fileName));

            if (file != null)
            {
                return removeRootChar ?
                    Path.Combine(filePath, file).Replace("~", "") :
                    Path.Combine(filePath, file);
            }

            return string.Empty;
        }

        public void StoreTokenData(TokenModel token)
        {
            if (token == null)
            {
                throw new ArgumentNullException(nameof(token));
            }

            var serialized = ProtectToken(JsonConvert.SerializeObject(token));

            var cookie = _cookieService.Get(AssistanceConstants.Cookies.AuthenticationTokenKey)
                ?? new HttpCookie(AssistanceConstants.Cookies.AuthenticationTokenKey)
                {
                    Secure = false, // Change this to true in production
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(50)
                };

            cookie.Value = serialized;

            _cookieService.Add(cookie);
        }

        public TokenModel GetTokenData(string key)
        {
            var cookie = _cookieService.Get(key);

            var result = JsonConvert.DeserializeObject<TokenModel>(UnprotectToken(cookie.Value));

            return result;
        }

        public string ProtectToken(string unprotectedContent)
        {
            var data = Encoding.UTF8.GetBytes(unprotectedContent);
            var encryptedData = MachineKey.Protect(data, ProtectionPurpose);
            var result = Convert.ToBase64String(encryptedData);

            return result;
        }

        public string UnprotectToken(string protectedContent)
        {
            var data = Convert.FromBase64String(protectedContent);
            var decryptedData = MachineKey.Unprotect(data, ProtectionPurpose);
            var result = Encoding.UTF8.GetString(decryptedData);

            return result;
        }

        public string MapPath(params string[] segments)
        {
            var root = AppDomain.CurrentDomain.BaseDirectory;
            var segmentsCombined = string.Join("/", segments);

            var path = Path.Combine(root, segmentsCombined);

            return path.Replace("~", " ")/*.Replace("//", "")*/;
        }

        #endregion Methods
    }
}