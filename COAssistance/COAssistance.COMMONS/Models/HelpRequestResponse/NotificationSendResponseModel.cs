using System.Runtime.Serialization;

namespace COAssistance.COMMONS.Models.HelpRequestResponse
{
    [DataContract]
    public class NotificationSendResponseModel
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }
    }
}
