using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.COMMONS.Models.FirebaseTokens
{
    public class FirebaseTokenCreateModel
    {
        [DataMember]
        [Required]
        public string Token { get; set; }
        [DataMember]
        [Required]
        public string UniqueMobileId { get; set; }
    }
}
