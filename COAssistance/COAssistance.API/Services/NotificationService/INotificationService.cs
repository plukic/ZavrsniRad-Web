using System.Threading.Tasks;
using COAssistance.COMMONS.Models.HelpRequestResponse;
using COAssistance.COMMONS.Models.HelpRequests;
using COAssistance.DATA.Model.HelpRequests;

namespace COAssistance.API.Services.NotificationService
{
    public interface INotificationService
    {
        void PushHelpRequestCreatedNotification(HelpRequestsDetailsModel model);
        Task<bool> SendHelpRequestNotification(string clientId, HelpRequestResponseModel result);
        Task<bool> SendHelpRequestNotification(string clientId, HelpRequestResponse newRequest);
    }
}