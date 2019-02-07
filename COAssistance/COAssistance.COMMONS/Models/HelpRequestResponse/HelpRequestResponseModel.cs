using System;

namespace COAssistance.COMMONS.Models.HelpRequestResponse
{
    public class HelpRequestResponseModel : BaseModel<int>
    {
        public string ClientId { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string HelpRequestState { get; set; }
        public int HelpRequestId { get; set; }
        public string ShortInstructions { get; set; }
        public DateTime HelpIncomingDateTime { get; set; }
        public bool IsActive { get; set; }
    }
}