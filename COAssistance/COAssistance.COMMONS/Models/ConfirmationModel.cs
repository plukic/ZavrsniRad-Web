namespace COAssistance.COMMONS.Models
{
    public class ConfirmationModel: BaseModel<string>
    {
        public string RouteName { get; set; }
        public string UpdateTargetId { get; set; }
        public object AdditionalData { get; set; }
    }
}