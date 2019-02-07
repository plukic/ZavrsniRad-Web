namespace COAssistance.COMMONS.Models.UserGroups
{
    public class UserGroupsModel : BaseModel<int>
    {
        public string GroupName { get; set; }
        public ConfigurationGroupEnum ConfigurationGroup { get; set; }
        public int LanguageId { get; set; }
        public string Language { get; set; }
        public string SupportNumber { get; set; }
    }
}