namespace COAssistance.COMMONS.Models.Staff
{
    public class StaffChangeStatusModel : BaseModel<string>
    {
        public bool IsActive { get; set; }
    }
}