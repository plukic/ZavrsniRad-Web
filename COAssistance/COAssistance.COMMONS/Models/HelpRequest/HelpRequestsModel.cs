using System;

namespace COAssistance.COMMONS.Models.HelpRequests
{
    public class HelpRequestsModel : BaseModel<int>
    {
        public DateTime RequestDate { get; set; }

        public HelpRequestCategory HelpRequestCategory { get; set; }

        public bool IsSolved { get; set; }

        public string Client { get; set; }

        public string ClientId { get; set; }
    }
}