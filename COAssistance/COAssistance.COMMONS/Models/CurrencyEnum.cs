using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models
{
    public enum CurrencyEnum
    {
        [Display(Description = "KM")]
        KM = 1,

        [Display(Description = "KN")]
        KN = 2,

        [Display(Description = "€")]
        EURO = 3
    }
}