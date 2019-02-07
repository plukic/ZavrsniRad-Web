using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.COMMONS.Models.Clients
{
    [DataContract]
    public class ClientAccountUpdateModel
    {
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string PhoneNumber { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Address { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public BloodTypes? BloodType { get; set; }
        [DataMember]
        public string ChronicDiseases { get; set; }
        [DataMember]
        public string Diagnose { get; set; }
        [DataMember]
        public string HistoryOfCriticalIllness { get; set; }

    }
}
