using System;
using System.Linq;
using System.Threading.Tasks;

using COAssistance.API.Services.ApiClient;
using COAssistance.API.SignalR;
using COAssistance.COMMONS.Models.HelpRequestResponse;
using COAssistance.COMMONS.Models.HelpRequests;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model.HelpRequests;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace COAssistance.API.Services.NotificationService
{
    public class NotificationService : INotificationService
    {
        private IHubConnectionContext<dynamic> clients = GlobalHost.ConnectionManager.GetHubContext<HelpRequestHub>().Clients;
        private readonly IApiClient _apiClient;
        private readonly  COAssistanceDbContext _context;

        public NotificationService(IApiClient apiClient,COAssistanceDbContext context)
        {

            _apiClient = apiClient ?? throw new ArgumentNullException(nameof(apiClient));
            _context=context?? throw new ArgumentNullException(nameof(context));
        }

        public void PushHelpRequestCreatedNotification(HelpRequestsDetailsModel model)
        {
            clients.All.OnNewHelpRequest(model);
        }

        public async Task<bool> SendHelpRequestNotification(string clientId,HelpRequestResponseModel model)
        {
            var tokens = _context.FirebaseTokens.Where(x => x.IsActive && x.ClientId == clientId);
            if (!tokens.Any())
                return await Task.FromResult(true);
            foreach (var item in tokens)
            {
                var requestResult = await _apiClient.SendNotification(new HelpRequestNotificationModel<HelpRequestResponseModel>
                {
                    Data = model,
                    Priority = "high",
                    To = item.Token
                });
            }
            return true;
        }

        public async Task<bool> SendHelpRequestNotification(string clientId, HelpRequestResponse newRequest)
        {
            HelpRequestResponseModel model;
            if (newRequest == null)
            {
                model = new HelpRequestResponseModel();
            }
            else
            {
                model = new HelpRequestResponseModel
                {
                    ClientId = clientId,
                    ContactPhoneNumber = "",
                    HelpIncomingDateTime = newRequest.HelpIncomingDateTimeUtc,
                    HelpRequestId = newRequest.HelpRequestId,
                    HelpRequestState = newRequest.HelpRequestState,
                    Id = newRequest.Id,
                    IsActive = newRequest.IsActive,
                    ShortInstructions = newRequest.ShortInstruction
                };
            }
            return await SendHelpRequestNotification(clientId, model);
        }
       
    }
}