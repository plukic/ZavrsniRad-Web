using COAssistance.COMMONS.Models.UserGroups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.WEB.Factories
{
    public interface IUserGroupsFactory
    {
        Task PrepareModel(UserGroupsEditModel model);
    }
}
