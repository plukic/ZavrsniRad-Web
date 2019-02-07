using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models
{
    public enum FuelTypeEnum
    {
        [Display(Name = "Petrol")]
        Petrol = 1,

        Diesel = 2,
        LPG = 3,
        OTHER = 4
    }
}