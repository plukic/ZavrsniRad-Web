using COAssistance.DATA.Model.Clients;
using System.Collections.Generic;

namespace COAssistance.API.Services.ClientEmergencyNumbers
{
    public interface IClientEmergencyNumberService
    {
        #region Methods

        IEnumerable<EmergencyContactNumbers> GetClientNumbers(string clientId);

        void Create(EmergencyContactNumbers emergencyContactNumbers);

        void Edit(EmergencyContactNumbers emergencyContactNumbers);
        void DeleteClientEmergencyNumber(int id);

        #endregion Methods
    }
}