using COAssistance.COMMONS.Resources;
using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models.Profile
{
    public class ProfileChangePasswordModel : BaseModel<string>
    {
        [Display(Name = nameof(Resource.OldPassword), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string OldPassword { get; set; }

        [Display(Name = nameof(Resource.NewPassword), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string NewPassword { get; set; }

        [Display(Name = nameof(Resource.ConfirmedPassword), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        [Compare(nameof(NewPassword), ErrorMessageResourceName = nameof(Resource.PasswordMismatch), ErrorMessageResourceType = typeof(Resource))]
        public string ConfirmedPassword { get; set; }
    }
}