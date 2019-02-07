using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.COMMONS.Models.Email
{
    public class NewPasswordEmailModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NewPassword { get; set; }
    }
}
