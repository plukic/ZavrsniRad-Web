using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using COAssistance.COMMONS.Clients;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model.Authorization;
using Microsoft.Owin.Security;

namespace COAssistance.API.Services.TokenService
{
    public interface ITokenService
    {
        UserLoginData FindUser(string userName, string password);
        //bool IsValidClientId(string clientId);
        bool IsAuthorizedToAccessResource(string userId, string oAuthClientId);
        DateTime? GetRefreshTokenExpirationTime(string oAuthClientId, string userId);

        OAuthClient FindOAuthClient(string clientId);

        Task<bool> AddRefreshToken(RefreshToken token);
        Task<RefreshToken> FindRefreshToken(string refreshTokenId);
        IEnumerable<ClientLoginData> GetLoginDataById(string id);
        void RemoveToken(string id);
    }
}