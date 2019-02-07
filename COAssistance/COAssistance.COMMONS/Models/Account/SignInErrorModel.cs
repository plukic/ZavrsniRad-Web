using System.Runtime.Serialization;

namespace COAssistance.COMMONS.Models.Account
{
    [DataContract]
    public class SignInErrorModel
    {
        [DataMember(Name = "error")]
        public string Type { get; set; }

        [DataMember(Name = "error_description")]
        public string Text { get; set; }
    }
}