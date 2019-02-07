using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models
{
    public enum ConfigurationGroupEnum
    {
        [Display(Name = "Central Osiguranje")]
        CO = 0,

        [Display(Name = "Central Home")]
        COHome = 1
    }
}