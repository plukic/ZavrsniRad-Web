using COAssistance.COMMONS.Models.Account;
using COAssistance.WEB.Constants;
using COAssistance.WEB.Infrastructure;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace COAssistance.WEB.Services
{
    public class HttpClientHandler : DelegatingHandler
    {
        #region Methods

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode && response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var commonService = UnityConfig.Container.Resolve<ICommonService>();

                var token = commonService.GetTokenData(AssistanceConstants.Cookies.AuthenticationTokenKey);

                if (token != null)
                {
                    var accessData = new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("grant_type", "refresh_token"),
                        new KeyValuePair<string, string>("refresh_token", token.RefreshToken),
                        new KeyValuePair<string, string>("client_id", ConfigurationManager.AppSettings["WebClientId"]),
                    };

                    var content = new FormUrlEncodedContent(accessData);

                    var baseUri = ConfigurationManager.AppSettings["WebApiUri"];

                    var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, baseUri + "token")
                    {
                        Content = content
                    };

                    var result = await base.SendAsync(httpRequestMessage, cancellationToken);

                    if (result.IsSuccessStatusCode)
                    {
                        var tokenResponse = await result.Content.ReadAsAsync<TokenModel>();

                        commonService.StoreTokenData(tokenResponse);

                        request.Headers.Remove("Authorization");

                        request.Headers.Add("Authorization", $"Bearer {tokenResponse.AccessToken}");

                        response = await base.SendAsync(request, cancellationToken);

                        return response;
                    }
                    else
                    {
                        throw new COUnauthorizedException();
                    }
                }

                throw new COUnauthorizedException();
            }

            return response;
        }

        #endregion Methods
    }
}