using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.DATA.Model.FirebaseTokens
{
   public class FirebaseToken
    {
        public int Id { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string UniqueMobileId { get; set; }
        public bool IsActive { get; set; }

        public string ClientId { get; set; }
        public Client Client { get; set; }

    }
}
