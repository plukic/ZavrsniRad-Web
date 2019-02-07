using COAssistance.API.Services.TokenService;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Unity;

namespace COAssistance.API.Providers
{
    public enum AccountType
    {
        Admin, Client
    }

    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }
            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var tokenService = UnityConfig.Container.Resolve<ITokenService>();
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            UserLoginData user = tokenService.FindUser(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("login_error", Resources.Resource.UsernameOrPasswordNotValid);
                return;
            }
            else if (!tokenService.IsAuthorizedToAccessResource(user.Id, context.ClientId))
            {
                context.SetError("login_error", COMMONS.Resources.Resource.UserNotAuthorizedToAccessResource);
                return;
            }
            else if (!user.IsActive)
            {
                context.SetError("login_error", Resources.Resource.UserAccountNotActive);
                return;
            }

            //ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
            //    CookieAuthenticationDefaults.AuthenticationType);
            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);
            AuthenticationProperties properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "client_id", (context.ClientId == null) ? string.Empty : context.ClientId
                    },
                    {
                        "userName", context.UserName
                    }
                });

            //= CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            //context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override async Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["client_id"];
            var currentClient = context.ClientId;
            if (originalClient != currentClient)
            {
                context.SetError("refresh_token", "Refresh token is issued to a different clientId.");
                return;
            }

            var id = context.Ticket.Identity.GetUserId();
            var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
            var tokenService = UnityConfig.Container.Resolve<ITokenService>();

            var user = userManager.FindById(id);
            if (user == null)
            {
                context.SetError("refresh_token", Resources.Resource.UsernameOrPasswordNotValid);
                return;
            }
            if (!user.IsActive)
            {
                context.SetError("refresh_token", Resources.Resource.UserAccountNotActive);
                return;
            }
        
            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);

            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, context.Ticket.Properties);
            context.Validated(ticket);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = string.Empty;
            string clientSecret = string.Empty;
            OAuthClient client = null;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                context.SetError("login_error", COMMONS.Resources.Resource.LoginClientIdMissingOrInvalid);
                return Task.FromResult<object>(null);
            }

            var tokenService = UnityConfig.Container.Resolve<ITokenService>();
            client = tokenService.FindOAuthClient(context.ClientId);
            if (client == null)
            {
                context.SetError("login_error", COMMONS.Resources.Resource.LoginClientIdMissingOrInvalid);
                return Task.FromResult<object>(null);
            }
            if (!client.Active)
            {
                context.SetError("login_error", COMMONS.Resources.Resource.OAuthClientIdInactive);
                return Task.FromResult<object>(null);
            }
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}