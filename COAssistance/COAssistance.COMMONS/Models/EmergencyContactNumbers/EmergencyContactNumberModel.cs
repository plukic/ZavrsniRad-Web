namespace COAssistance.COMMONS.Models.EmergencyContactNumbers
{
    public class EmergencyContactNumberModel : BaseModel<int>
    {
        public string ClientId { get; set; }
        public string ContactFullName { get; set; }
        public string PhoneNumber { get; set; }
    }
}