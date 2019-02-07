using COAssistance.COMMONS.Models.Account;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace COAssistance.WEB.Services
{
    public interface ICommonService
    {
        #region Methods

        string GenerateQueryStringUrl(string target, params KeyValuePair<string, object>[] parameters);

        Task<string> CheckForValidationErrors(HttpResponseMessage response, string replacementMessage);

        string ResolveIcon(string fileName, bool removeRootChar = false);

        void StoreTokenData(TokenModel token);

        TokenModel GetTokenData(string key);

        string ProtectToken(string unprotectedContent);

        string UnprotectToken(string protectedContent);

        string MapPath(params string[] segments);

        #endregion Methods
    }
}