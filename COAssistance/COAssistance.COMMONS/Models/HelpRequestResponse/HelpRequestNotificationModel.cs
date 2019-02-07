using System.Runtime.Serialization;

namespace COAssistance.COMMONS.Models.HelpRequestResponse
{
    [DataContract]
    public class HelpRequestNotificationModel<T>
    {
        [DataMember(Name = "to")]
        public string To { get; set; }

        [DataMember(Name = "priority")]
        public string Priority { get; set; }

        [DataMember(Name = "data")]
        public T Data { get; set; }
    }
}
