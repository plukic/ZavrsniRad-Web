using COAssistance.DATA.EF;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COAssistance.API.Services.RoleService
{
    public interface IRoleService
    {
        IdentityRole GetAdminRole();
        IdentityRole GetClientRole();
    }
}
