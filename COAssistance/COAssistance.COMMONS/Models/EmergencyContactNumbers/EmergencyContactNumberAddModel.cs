using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.COMMONS.Models.EmergencyContactNumbers
{
    public class EmergencyContactNumberAddModel:BaseModel<int>
    {
        [Required]
        public string ContactFullName { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
