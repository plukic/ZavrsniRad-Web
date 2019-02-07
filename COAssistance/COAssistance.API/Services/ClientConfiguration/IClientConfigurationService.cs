using COAssistance.COMMONS.Models;
using COAssistance.COMMONS.Models.UserGroups;
using COAssistance.DATA.Model;
using System.Collections.Generic;

namespace COAssistance.API.Services.ClientConfiguration
{
    public interface IClientConfigurationService
    {
        #region Methods

        IEnumerable<UserGroupsModel> Get();

        void CreateConfiguration(ClientConfigurationGroup clientConfigurationGroup);

        void Edit(UserGroupsEditModel model);

        ClientConfigurationGroup GetClientConfiguration(ConfigurationGroupEnum configurationType);

        ClientConfigurationGroup GetById(int groupId);

        ClientConfigurationGroup GetByUserId(string userId);

        #endregion Methods
    }
}