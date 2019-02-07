using COAssistance.COMMONS.Resources;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace COAssistance.COMMONS.Models.Admin
{
    [DataContract]
    public class AdminChangePasswordViewModel
    {
        [DataMember]
        [Required]
        public string Id { get; set; }

        [DataMember]
        [Display(Name = nameof(Resource.OldPassword), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string OldPassword { get; set; }

        [DataMember]
        [Display(Name = nameof(Resource.NewPassword), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string NewPassword { get; set; }
    }
}