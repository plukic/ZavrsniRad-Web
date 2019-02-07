namespace COAssistance.COMMONS.Models.Clients
{
    public class ClientsManageModel
    {
        public string SearchText { get; set; }
        public PagerModel<ClientModel> PagingResult { get; set; }
    }
}