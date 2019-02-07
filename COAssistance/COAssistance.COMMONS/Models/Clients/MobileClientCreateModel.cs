using COAssistance.COMMONS.Resources;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace COAssistance.COMMONS.Models.Clients
{
    public class MobileClientCreateModel:ClientCreateModel
    {
        [DataMember]
        [Required]
        public string Password { get; set; }
    }
}