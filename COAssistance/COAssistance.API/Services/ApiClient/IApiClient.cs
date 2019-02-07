using COAssistance.COMMONS.Models.HelpRequestResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.API.Services.ApiClient
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> SendNotification<T>(HelpRequestNotificationModel<T> content);
    }
}
