using COAssistance.COMMONS.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace COAssistance.COMMONS.Models.Clients
{
    [DataContract]
    public class ClientCreateModel
    {
        [DataMember]
        [Display(Name = nameof(Resource.FirstName), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string FirstName { get; set; }

        [DataMember]
        [Display(Name = nameof(Resource.LastName), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string LastName { get; set; }

        [DataMember]
        [Display(Name = nameof(Resource.CardNumber), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string CardNumber { get; set; }

        [DataMember]
        [Display(Name = nameof(Resource.PhoneNumber), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string PhoneNumber { get; set; }

        [DataMember]
        [Display(Name = nameof(Resource.Email), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        [EmailAddress(ErrorMessageResourceName = nameof(Resource.EmailInvalidFormat), ErrorMessageResourceType = typeof(Resource))]
        public string Email { get; set; }

        [DataMember]
        [Display(Name = nameof(Resource.Address), ResourceType = typeof(Resource))]
        public string Address { get; set; }

        [DataMember]
        [Display(Name = nameof(Resource.City), ResourceType = typeof(Resource))]
        public string City { get; set; }

        [DataMember]
        [Display(Name = nameof(Resource.Group), ResourceType = typeof(Resource))]
        public ConfigurationGroupEnum ConfigurationGroup { get; set; }

    }
}