using COAssistance.COMMONS.Infrastructure;
using COAssistance.COMMONS.Resources;
using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models.Admin
{
    public class AdminResetPasswordViewModel
    {
        [Required]
        public string Id { get; set; }

        [Display(Name = nameof(Resource.Length), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public int Length { get; set; }

        [Display(Name = nameof(Resource.SpecialCharacters), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public int SpecialCharacters { get; set; }
    }
}