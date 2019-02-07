using COAssistance.COMMONS.Models.HelpRequestResponse;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace COAssistance.API.Services.ApiClient
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;

        public ApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public Task<HttpResponseMessage> SendNotification<T>(HelpRequestNotificationModel<T> content)
        {
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();

                var authorization = ConfigurationManager.AppSettings["FCMAuthorization"];
                var sender = ConfigurationManager.AppSettings["FCMSender"];
                httpClient.DefaultRequestHeaders.Clear();
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Sender",sender);
                httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",authorization);

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return httpClient.PostAsJsonAsync("/fcm/send", content);
            }
        }
    }
}