using System;

namespace COAssistance.COMMONS.Models.Clients
{
    public class ClientModel : BaseModel<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CardMembershipNumber { get; set; }
        public bool IsActive { get; set; }

     
        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}