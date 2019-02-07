using COAssistance.COMMONS.Resources;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace COAssistance.COMMONS.Models.UserGroups
{
    public class UserGroupsEditModel : BaseModel<int>
    {
        [ReadOnly(true)]
        [Display(Name = nameof(Resource.Group), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public ConfigurationGroupEnum ConfigurationGroup { get; set; }

        [Display(Name = nameof(Resource.Language), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public int LanguageId { get; set; }

        [Display(Name = nameof(Resource.PhoneNumber), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string SupportNumber { get; set; }

        public SelectList Languages { get; set; }
    }
}