using COAssistance.COMMONS.Models;
using COAssistance.COMMONS.Models.UserGroups;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace COAssistance.API.Services.ClientConfiguration
{
    public class ClientConfigurationService : IClientConfigurationService
    {
        #region Fields

        private COAssistanceDbContext _context;

        #endregion Fields

        #region Constructor

        public ClientConfigurationService(COAssistanceDbContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Methods

        public void CreateConfiguration(ClientConfigurationGroup clientConfigurationGroup)
        {
            if (clientConfigurationGroup == null)
            {
                throw new ArgumentNullException(nameof(clientConfigurationGroup));
            }

            _context.ClientConfigurationGroup.Add(clientConfigurationGroup);
            _context.SaveChanges();
        }

        public IEnumerable<UserGroupsModel> Get()
        {
            return _context.ClientConfigurationGroup
                .AsNoTracking()
                .Select(ccg => new UserGroupsModel
                {
                    Language = ccg.Language.LanguageName,
                    Id = ccg.Id,
                    ConfigurationGroup = ccg.ConfigurationGroupEnum,
                    SupportNumber = ccg.SupportNumber,
                    GroupName = ccg.ConfigurationGroupEnum.ToString()
                })
                .ToList();
        }

        public ClientConfigurationGroup GetById(int groupId)
        {
            return _context.ClientConfigurationGroup.Find(groupId);
        }

        public void Edit(UserGroupsEditModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var group = this.GetById(model.Id);

            if (group == null)
            {
                throw new ArgumentNullException(nameof(group));
            }

            group.LanguageId = model.LanguageId;
            group.SupportNumber = model.SupportNumber;

            _context.SaveChanges();
        }

        public ClientConfigurationGroup GetClientConfiguration(ConfigurationGroupEnum configurationType)
        {
            var clientConfiguration = _context
                .ClientConfigurationGroup
                .FirstOrDefault(x => x.ConfigurationGroupEnum == configurationType);

            if (clientConfiguration == null)
            {
                clientConfiguration = CreateClientConfiguration(configurationType);
                _context.ClientConfigurationGroup.Add(clientConfiguration);
                _context.SaveChanges();
            }
            return clientConfiguration;
        }

        private ClientConfigurationGroup CreateClientConfiguration(ConfigurationGroupEnum configurationType)
        {
            switch (configurationType)
            {
                case ConfigurationGroupEnum.CO:
                    return new ClientConfigurationGroup
                    {
                        Language = new Language
                        {
                            LanguageName = ConfigurationManager.AppSettings["LanguageDefaultName"],
                            LanguageCode = ConfigurationManager.AppSettings["LanguageDefaultCode"]
                        },
                        SupportNumber = ConfigurationManager.AppSettings["COSupportNumber"],
                        ConfigurationGroupEnum = ConfigurationGroupEnum.CO
                    };

                case ConfigurationGroupEnum.COHome:
                    return new ClientConfigurationGroup
                    {
                        Language = new Language
                        {
                            LanguageName = ConfigurationManager.AppSettings["LanguageDefaultName"],
                            LanguageCode = ConfigurationManager.AppSettings["LanguageDefaultCode"]
                        },
                        SupportNumber = ConfigurationManager.AppSettings["COHomeSupportNumber"],
                        ConfigurationGroupEnum = ConfigurationGroupEnum.COHome
                    };
            }

            throw new NotImplementedException();
        }

        public ClientConfigurationGroup GetByUserId(string userId)
        {
            return _context.Clients
                .Where(x => x.ClientId == userId)
                .Select(x => x.ClientConfigurationGroup)
                .First();
        }

        #endregion Methods
    }
}