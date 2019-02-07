using COAssistance.COMMONS.Infrastructure;
using COAssistance.COMMONS.Resources;
using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models.Staff
{
    public class StaffResetPasswordModel : BaseModel<string>
    {
        [Display(Name = nameof(Resource.Length), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        [MinNumber(1, ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public int Length { get; set; }

        [Display(Name = nameof(Resource.SpecialCharacters), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        [MinNumber(1, ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public int SpecialCharacters { get; set; }

        public int PasswordLength { get; set; }

        public int PasswordSpecialCharacters { get; set; }
    }
}