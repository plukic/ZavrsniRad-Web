using COAssistance.API.Services.TokenService;
using COAssistance.DATA.Model.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Threading.Tasks;
using Unity;

namespace COAssistance.API.Providers
{
    public class ApplicationRefreshTokenProvider : AuthenticationTokenProvider
    {
        public override async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["client_id"];
            var userId = context.Ticket.Identity.GetUserId();

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }
            var userManager = UnityConfig.Container.Resolve<ApplicationUserManager>();
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
                return;

            var refreshTokenId = Guid.NewGuid().ToString("n");

            var tokenService = UnityConfig.Container.Resolve<ITokenService>();
            var expiresUtc = tokenService.GetRefreshTokenExpirationTime(clientid, user.Id);
            if (!expiresUtc.HasValue)
                return;

            var token = new RefreshToken
            {
                Id = refreshTokenId,
                OAuthClientId = clientid,
                Subject = user.UserName,
                ExpiresUtc = expiresUtc.Value,
                IssuedUtc = DateTime.UtcNow,
                UserId=userId
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();
            var result = await tokenService.AddRefreshToken(token);

            if (result)
                context.SetToken(refreshTokenId);
        }

        public override async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            string tokenId = context.Token;
            var tokenService = UnityConfig.Container.Resolve<ITokenService>();

            var refreshToken = await tokenService.FindRefreshToken(tokenId);
            if (refreshToken != null)
            {
                //Get protectedTicket from refreshToken class
                context.DeserializeTicket(refreshToken.ProtectedTicket);
            }
        }

    }
}