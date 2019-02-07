namespace COAssistance.COMMONS.Models.HelpRequests
{
    public class HelpRequestsManageModel
    {
        public HelpRequestCategory HelpRequestCategory { get; set; }
        public PagerModel<HelpRequestsModel> PagingResult { get; set; }
    }
}