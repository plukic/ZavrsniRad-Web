using COAssistance.COMMONS.Clients;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model;
using COAssistance.DATA.Model.Authorization;
using COAssistance.DATA.Model.Clients;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace COAssistance.API.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private COAssistanceDbContext context;
        private ApplicationUserManager applicationUserManager;

        public TokenService(COAssistanceDbContext context, ApplicationUserManager applicationUserManager)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.applicationUserManager = applicationUserManager ?? throw new ArgumentNullException(nameof(applicationUserManager));
        }

        public async Task<bool> AddRefreshToken(RefreshToken token)
        {
            //var existingTokens = context
            //    .RefreshTokens
            //    .Where(r => r.OAuthClientId == token.OAuthClientId && r.UserId == token.UserId)
            //    .ToList();
            //if (existingTokens != null)
            //{
            //    try
            //    {
            //        context.RefreshTokens.RemoveRange(existingTokens);
            //        await context.SaveChangesAsync();
            //    }
            //    catch (Exception e)
            //    {

            //    }
            //}

            context.RefreshTokens.Add(token);
            return await context.SaveChangesAsync() > 0;
        }

        public OAuthClient FindOAuthClient(string clientId)
        {
            return context.OAuthClients.FirstOrDefault(x => x.Id == clientId);
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            var refreshToken = await context.RefreshTokens.FindAsync(refreshTokenId);
            return refreshToken;
        }

        public UserLoginData FindUser(string userName, string password)
        {
            return applicationUserManager.FindAsync(userName, password).Result;
        }

        public DateTime? GetRefreshTokenExpirationTime(string oAuthClientId, string userId)
        {
            if (context.Clients.Any(x => x.ClientId == userId))
                return DateTime.UtcNow.AddMinutes(20);

                return DateTime.UtcNow.AddDays(2);
        }

   
    
        public bool IsAuthorizedToAccessResource(string userId, string oAuthClientId)
        {
            var oAuthClient = context.OAuthClients
                .Where(x => x.Id == oAuthClientId)
                .Include(x => x.OAuthClientRoles)
                .FirstOrDefault(x => x.Id == oAuthClientId);

            if (oAuthClient == null)
                return false;
            var userRoles = context.Users.Where(x => x.Id == userId).Include(x => x.Roles).FirstOrDefault().Roles;
            return userRoles.Any(x => oAuthClient.OAuthClientRoles.Any(y => y.IdentityRoleId == x.RoleId));
        }

        public IEnumerable<ClientLoginData> GetLoginDataById(string id)
        {
            return context.RefreshTokens
                .Where(x => x.UserId == id)
                .GroupBy(x => x.OAuthClientId)
                .Select(x => x.OrderByDescending(y => y.IssuedUtc).FirstOrDefault())
                .Select(x => new ClientLoginData
                {
                    OAuthClient = x.OAuthClient.Name,
                    Expires = x.ExpiresUtc,
                    Issued = x.IssuedUtc,
                    TokenId = x.Id,
                    UserId = x.UserId
                }).ToList();
        }


        public void RemoveToken(string id)
        {
            var token = context.RefreshTokens
                .Where(x => x.Id == id).FirstOrDefault();
            if (token == null)
                return;

            var tokens = context
                .RefreshTokens
                .Where(x => x.OAuthClientId == token.OAuthClientId && x.UserId == token.UserId);

            context.RefreshTokens.RemoveRange(tokens);
            context.SaveChanges();
        }
    }
}