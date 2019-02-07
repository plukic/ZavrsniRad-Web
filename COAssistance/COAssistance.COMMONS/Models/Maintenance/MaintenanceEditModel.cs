using COAssistance.COMMONS.Resources;
using System.ComponentModel.DataAnnotations;

namespace COAssistance.COMMONS.Models.Maintenance
{
    public class MaintenanceEditModel
    {
        public int MaintenanceId { get; set; }

        [Display(Name = nameof(Resource.PasswordLength), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public int PasswordLength { get; set; }

        [Display(Name = nameof(Resource.SpecialCharacters), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public int PasswordSpecialCharacters { get; set; }


    }
}