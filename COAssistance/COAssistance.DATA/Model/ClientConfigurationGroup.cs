using COAssistance.COMMONS.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace COAssistance.DATA.Model
{
    public class ClientConfigurationGroup
    {
        public int Id { get; set; }

        [EnumDataType(typeof(ConfigurationGroupEnum))]
        public ConfigurationGroupEnum ConfigurationGroupEnum { get; set; }

        public int LanguageId { get; set; }

        public Language Language { get; set; }

        [Required]
        public string SupportNumber { get; set; }

        public ICollection<Client> Clients { get; set; }
    }
}