using System.Runtime.Serialization;

namespace COAssistance.COMMONS.Models.Languages
{
    [DataContract]
    public class LanguageDropDownViewModel : BaseModel<int>
    {
        [DataMember]
        public override int Id { get => base.Id; set => base.Id = value; }

        [DataMember]
        public string Name { get; set; }
    }
}