namespace COAssistance.COMMONS.Models.HelpRequest
{
    public class HelpRequestCreateModel
    {
        public HelpRequestCategory HelpRequestCategory { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}