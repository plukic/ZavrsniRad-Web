using COAssistance.COMMONS.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace COAssistance.COMMONS.Models.HelpRequestResponse
{
    public class HelpRequestResponseCreateModel
    {
        [Display(Name = nameof(Resource.RequestState), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string HelpRequestState { get; set; }

        [Display(Name = nameof(Resource.Instruction), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string ShortInstruction { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = nameof(Resource.ArrivalDate), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public DateTime ExpirationDateUtc { get; set; }

        [DataType(DataType.Time)]
        [Display(Name = nameof(Resource.ArrivalTime), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public string ExpirationTime { get; set; }

        [Required]
        public int HelpRequestId { get; set; }

        [Display(Name = nameof(Resource.Language), ResourceType = typeof(Resource))]
        [Required(ErrorMessageResourceName = nameof(Resource.RequiredField), ErrorMessageResourceType = typeof(Resource))]
        public int LanguageId { get; set; }

        public string Language { get; set; }

        public SelectList Languages { get; set; }

        public List<DropDownModel> InstructionTemplates { get; set; }
    }
}