using COAssistance.COMMONS.Models.HelpRequestResponse;
using System.Threading.Tasks;

namespace COAssistance.WEB.Factories
{
    public interface IHelpRequestFactory
    {
        Task PrepareResponseModel(HelpRequestResponseCreateModel model);
    }
}