using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.DATA.Model.HelpRequests
{
    public class HelpRequestResponse
    {
        public int Id { get; set; }
        [Required]
        public string HelpRequestState { get; set; }
        [Required]
        public string ShortInstruction { get; set; }

        [Required]
        public DateTime HelpIncomingDateTimeUtc { get; set; }

        [Required]
        public bool IsActive { get; set; }


        public int HelpRequestId { get; set; }
        public HelpRequest HelpRequest { get; set; }

        public int LanguageId { get; set; }
        public Language Language{ get; set; }

    }
}
