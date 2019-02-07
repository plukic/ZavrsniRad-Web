using COAssistance.COMMONS.Resources;
using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models.Admin
{
    public class AdminAccountCreateViewModel
    {
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        [MinLength(6, ErrorMessageResourceName = nameof(Resource.ErrorUsernameLength), ErrorMessageResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Display(Name = nameof(Resource.RequiredField), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.PhoneNumber), ErrorMessageResourceType = typeof(Resource))]
        public string PhoneNumber { get; set; }

        [Display(Name = nameof(Resource.RequiredField), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.FirstName), ErrorMessageResourceType = typeof(Resource))]
        public string FirstName { get; set; }

        [Display(Name = nameof(Resource.RequiredField), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.LastName), ErrorMessageResourceType = typeof(Resource))]
        public string LastName { get; set; }
    }
}