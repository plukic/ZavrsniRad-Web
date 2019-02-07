using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.DATA.Model
{
    public class ClientToken
    {
        public int Id { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime ValidUntil { get; set; }

        [Required]
        public bool IsUsed { get; set; }

        public Client Client{ get; set; }
        public string ClientId { get; set; }
    }
}
