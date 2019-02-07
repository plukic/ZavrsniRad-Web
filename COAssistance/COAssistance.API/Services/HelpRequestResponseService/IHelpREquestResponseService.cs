using COAssistance.COMMONS.Models.HelpRequestResponse;
using COAssistance.DATA.Model;
using COAssistance.DATA.Model.HelpRequests;
using System.Collections.Generic;

namespace COAssistance.API.Services.HelpRequestResponseService
{
    public interface IHelpRequestResponseService
    {
        #region Methods

        HelpRequestResponse GetClientHelpRequestResponse(string userId);

        HelpRequestResponse CreateClientHelpRequestResponse(HelpRequestResponseCreateModel vm);

        HelpRequestResponseModel GetDetails(int httpRequestResponseId);

        IEnumerable<HelpRequestResponseModel> GetHelpRequestResponses(int helpRequestId);

        void Activate(HelpRequestActivationModel model);
        Client GetHelpRequestResponseClient(int helpRequestResponseId);
        #endregion Methods
    }
}