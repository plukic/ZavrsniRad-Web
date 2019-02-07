using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models
{
    public enum BloodTypes
    {
        [Display(Name = "0")]
        Zero = 1,

        [Display(Name = "A")]
        A = 2,

        [Display(Name = "B")]
        B = 3,

        [Display(Name = "AB")]
        AB = 4
    }
}