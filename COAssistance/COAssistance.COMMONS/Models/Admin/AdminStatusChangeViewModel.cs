using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace COAssistance.COMMONS.Models.Admin
{
    [DataContract]
    public class AdminStatusChangeViewModel
    {
        [Required]
        [DataMember]
        public string Id { get; set; }

        [Required]
        [DataMember]
        public bool IsActive { get; set; }
    }
}