using COAssistance.COMMONS.Resources;
using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models.Account
{
    public class SignInModel
    {
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = nameof(Resource.UserName), ResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        [Display(Name = nameof(Resource.Password), ResourceType = typeof(Resource))]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}