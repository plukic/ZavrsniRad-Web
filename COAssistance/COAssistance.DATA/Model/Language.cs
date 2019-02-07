using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.DATA.Model
{
 
    public class Language
    {
        public int Id { get; set; }
        [Required]
        public string LanguageName { get; set; }//English, Bosnian, Serbian..
        [Required]
        public string LanguageCode { get; set; }//en-US,bs,srl,src,hrv

        public ICollection<ClientConfigurationGroup> ClientConfigurationGroup { get; set; }
    }

}
