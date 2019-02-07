using COAssistance.DATA.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.DATA.Model
{
    public class Admin
    {

        [ForeignKey("UserLoginData")]
        public string AdminId { get; set; }
        public virtual UserLoginData UserLoginData { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }


        public int MaintenanceId { get; set; }
        public Maintenance Maintenance { get; set; }


        public ICollection<UserAudit> UserAudit { get; set; }

    }
}
