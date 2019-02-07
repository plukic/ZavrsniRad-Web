using System;

namespace COAssistance.COMMONS.Models.Clients
{
    public class ClientsDetailsModel : BaseModel<string>
    {
        public string UserName { get; set; }
        public string CardNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public ConfigurationGroupEnum ConfigurationGroup { get; set; }
        public string SupportNumber { get; set; }
        public BloodTypes? BloodType { get; set; }
        public string ChronicDiseases { get; set; }
        public string Diagnose { get; set; }
        public string HistoryOfCriticalIllness { get; set; }
        public bool IsActive { get; set; }

  
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}