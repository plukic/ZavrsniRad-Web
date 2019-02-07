using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.COMMONS.Clients
{
    public class ClientLoginData
    {
        public string TokenId { get; set; }
        public string  OAuthClient{ get; set;}
        public DateTime  Issued{ get; set; }
        public DateTime  Expires{ get; set; }
        public string UserId { get; set; }
    }
}
