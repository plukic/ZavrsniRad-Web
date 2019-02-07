using COAssistance.COMMONS.Models;
using COAssistance.COMMONS.Models.HelpRequest;
using COAssistance.COMMONS.Models.HelpRequests;
using COAssistance.DATA.Model;
using System.Collections.Generic;
using System.Linq;

namespace COAssistance.API.Services.HelpRequestService
{
    public interface IHelpRequestService
    {
        #region Methods

        HelpRequestsDetailsModel AddNewRequest(HelpRequestCreateModel model, string clientId);

        IQueryable<HelpRequest> GetAll(HelpRequestCategory helpRequestCategory);

        HelpRequestsDetailsModel GetById(int helpRequestId);

        void Complete(int helpRequestId);
        IList<HelpRequestsModel> GetLastRecords(int size);
        IList<HelpRequestsModel> GetUnfinished();

        #endregion Methods
    }
}