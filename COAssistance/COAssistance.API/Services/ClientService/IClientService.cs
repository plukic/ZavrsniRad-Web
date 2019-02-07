using COAssistance.COMMONS.Clients;
using COAssistance.COMMONS.Models.Clients;
using COAssistance.COMMONS.Models.FirebaseTokens;
using COAssistance.DATA.Model;
using COAssistance.DATA.Model.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COAssistance.API.Services.ClientService
{
    public interface IClientService
    {
        #region Methods

        IQueryable<Client> GetClients(string searchParameter);

        IEnumerable<Client> Get(int size = 0);

        Task<Client> FindByUsername(string username);

        Task<Client> FindById(string Id);

        Task<bool> Create(ClientCreateModel client);
        Task<bool> Create(MobileClientCreateModel client);

        void AddFirebaseToken(FirebaseTokenCreateModel firebaseCreateModel, string userId);

        void Edit(Client client);



        bool ChangeClientStatus(string clientId);
        bool UpdateUserProfile(ClientAccountUpdateModel clientAccountUpdateModel, string userId);

        #endregion Methods
    }
}