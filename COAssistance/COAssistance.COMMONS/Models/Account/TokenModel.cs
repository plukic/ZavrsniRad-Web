using System.Runtime.Serialization;

namespace COAssistance.COMMONS.Models.Account
{
    [DataContract]
    public class TokenModel
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        [DataMember(Name = "expires_in")]
        public string ExpiresIn { get; set; }

        [DataMember(Name = ".issued")]
        public string Issued { get; set; }

        [DataMember(Name = ".expires")]
        public string Expires { get; set; }
    }
}