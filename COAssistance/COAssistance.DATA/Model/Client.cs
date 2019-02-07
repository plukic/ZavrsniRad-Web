using COAssistance.COMMONS.Models;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model.Clients;
using COAssistance.DATA.Model.FirebaseTokens;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace COAssistance.DATA.Model
{
    public class Client
    {
        #region Scalar properties

        [ForeignKey("UserLoginData")]
        public string ClientId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public BloodTypes? BloodType { get; set; }
        public string ChronicDiseases { get; set; }
        public string Diagnose { get; set; }
        public string HistoryOfCriticalIllness { get; set; }
        public int ClientConfigurationGroupId { get; set; }

        #endregion Scalar properties

        #region Navigation properties

        public ClientConfigurationGroup ClientConfigurationGroup { get; set; }
        public virtual UserLoginData UserLoginData { get; set; }
        public ICollection<ClientToken> Tokens { get; set; }
        public ICollection<HelpRequest> HelpRequests { get; set; }
        public ICollection<EmergencyContactNumbers> EmergencyContactNumbers { get; set; }
        public ICollection<FirebaseToken> FirebaseTokens { get; set; }

        #endregion Navigation properties
    }
}