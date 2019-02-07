using COAssistance.COMMONS.Resources;
using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models
{
    public enum Genders
    {
        [Display(Name = "Male", ResourceType = typeof(Resource))]
        Male = 1,

        [Display(Name = "Female", ResourceType = typeof(Resource))]
        Female = 2
    }
}