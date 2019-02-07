using COAssistance.COMMONS.Resources;
using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models
{
    public enum HelpRequestCategory
    {
        [Display(Name = nameof(Resource.Accident), ResourceType = typeof(Resource))]
        Accident = 1,

        [Display(Name = nameof(Resource.Allergies), ResourceType = typeof(Resource))]
        Allergies = 2,

        [Display(Name = nameof(Resource.Other), ResourceType = typeof(Resource))]
        Other = 3
    }
}