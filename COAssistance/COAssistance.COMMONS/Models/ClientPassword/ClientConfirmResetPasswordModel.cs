using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models.ClientPassword
{
    public class ClientConfirmResetPasswordModel
    {
        [Required]
        public string Token { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string Username { get; set; }
    }
}