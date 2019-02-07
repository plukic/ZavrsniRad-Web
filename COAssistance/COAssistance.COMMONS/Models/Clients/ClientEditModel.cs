using COAssistance.COMMONS.Resources;
using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models.Clients
{
    public class ClientEditModel : BaseModel<string>
    {
        [Display(Name = nameof(Resource.FirstName), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string FirstName { get; set; }

        [Display(Name = nameof(Resource.LastName), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string LastName { get; set; }

        [Display(Name = nameof(Resource.CardNumber), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string CardNumber { get; set; }

        [Display(Name = nameof(Resource.PhoneNumber), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string PhoneNumber { get; set; }

        [Display(Name = nameof(Resource.Email), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        [EmailAddress(ErrorMessageResourceName = nameof(Resource.EmailInvalidFormat), ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; }

        [Display(Name = nameof(Resource.Address), ResourceType = typeof(Resource))]
        public string Address { get; set; }

        [Display(Name = nameof(Resource.City), ResourceType = typeof(Resource))]
        public string City { get; set; }
    }
}