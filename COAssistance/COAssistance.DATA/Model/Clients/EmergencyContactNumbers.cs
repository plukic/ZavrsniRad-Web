using System.ComponentModel.DataAnnotations;

namespace COAssistance.DATA.Model.Clients
{
    public class EmergencyContactNumbers : BaseEntity<int>
    {

        public string ClientId { get; set; }
        public Client Client { get; set; }

        [Required]
        public string ContactFullName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}
