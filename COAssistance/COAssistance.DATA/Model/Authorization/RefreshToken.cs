using COAssistance.DATA.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.DATA.Model.Authorization
{
    public class RefreshToken
    {
        public string Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Subject { get; set; }
        [Required]
        [MaxLength(50)]
        public string OAuthClientId { get; set; }
        public OAuthClient OAuthClient { get; set; }

        [Required]
        public DateTime IssuedUtc { get; set; }
        [Required]
        public DateTime ExpiresUtc { get; set; }
        [Required]
        public string ProtectedTicket { get; set; }


        public string UserId{ get; set; }
        public UserLoginData User{ get; set; }


    }
}
