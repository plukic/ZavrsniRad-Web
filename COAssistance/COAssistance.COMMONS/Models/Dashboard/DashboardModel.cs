using COAssistance.COMMONS.Models.HelpRequests;
using System.Collections.Generic;

namespace COAssistance.COMMONS.Models.Dashboard
{
    public class DashboardModel
    {
        public IEnumerable<HelpRequestsModel> HelpRequest { get; set; }
    }
}