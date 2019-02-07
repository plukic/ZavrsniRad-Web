using System.Collections.Generic;

namespace COAssistance.DATA.Model
{
    public class Maintenance
    {
        public int MaintenanceId { get; set; }
        public int PasswordLength { get; set; }
        public int PasswordSpecialCharacters { get; set; }
        public ICollection<Admin> Admins { get; set; }
        public ICollection<PredefinedTextTemplate> PredefinedTextTemplates { get; set; }
    }
}