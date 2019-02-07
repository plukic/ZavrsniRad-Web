using System.Collections.Generic;
using System.Threading.Tasks;
using COAssistance.COMMONS.Models.Clients;
using COAssistance.DATA.Model;

namespace COAssistance.API.Services.UserService
{
    public interface IUserService
    {
        //#region Methods

        //List<string> GetClientsCardMembership();

        int GetMaintenanceSettingsId();

        //#endregion Methods
    }
}