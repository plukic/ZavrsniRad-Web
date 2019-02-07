using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models.ClientPassword
{
    public class ClientRequestPasswordResetModel
    {
        [Required]
        public string Username { get; set; }
    }
}