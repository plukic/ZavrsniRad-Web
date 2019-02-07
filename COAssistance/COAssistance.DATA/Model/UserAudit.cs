using COAssistance.DATA.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.DATA.Model
{
    public class UserAudit
    {
        public int Id { get; set; }
        public string ActionType { get; set; }
        public string  Description { get; set; }
        public DateTime DateTime { get; set; }

        public string AdminId{ get; set; }
        public Admin Admin { get; set; }
    }
}
