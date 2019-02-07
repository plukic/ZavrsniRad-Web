namespace COAssistance.DATA.Model
{
    public class PredefinedTextTemplate
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public string Key { get; set; }

        public int MaintenanceId { get; set; }

        public Maintenance Maintenance { get; set; }
    }
}