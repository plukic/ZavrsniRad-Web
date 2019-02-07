namespace COAssistance.COMMONS.Models.HelpRequests
{
    public class HelpRequestsDetailsModel : HelpRequestsModel
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string ClientsLanguage { get; set; }
    }
}