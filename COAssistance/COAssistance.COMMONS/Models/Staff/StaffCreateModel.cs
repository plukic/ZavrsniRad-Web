using COAssistance.COMMONS.Resources;
using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models.Staff
{
    public class StaffCreateModel
    {
        [Display(Name = nameof(Resource.UserName), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        [MinLength(6, ErrorMessageResourceName = nameof(Resource.ErrorUsernameLength), ErrorMessageResourceType = typeof(Resource))]
        public string UserName { get; set; }

        [Display(Name = nameof(Resource.Email), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        [EmailAddress(ErrorMessageResourceName = nameof(Resource.EmailInvalidFormat), ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Display(Name = nameof(Resource.PhoneNumber), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string PhoneNumber { get; set; }

        [Display(Name = nameof(Resource.FirstName), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string FirstName { get; set; }

        [Display(Name = nameof(Resource.LastName), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string LastName { get; set; }
    }
}