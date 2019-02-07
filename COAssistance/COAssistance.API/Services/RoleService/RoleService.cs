using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace COAssistance.API.Services.RoleService
{
    public class RoleService : IRoleService
    {
        private RoleManager<IdentityRole> roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public IdentityRole GetAdminRole()
        {
            if (!roleManager.RoleExists(EntityRoles.AdminRole))
            {
                roleManager.Create(new IdentityRole
                {
                    Name = EntityRoles.AdminRole
                });
            }
            return roleManager.FindByName(EntityRoles.AdminRole);
        }

        public IdentityRole GetClientRole()
        {
            if (!roleManager.RoleExists(EntityRoles.ClientRole))
            {
                roleManager.Create(new IdentityRole
                {
                    Name = EntityRoles.ClientRole
                });
            }
            return roleManager.FindByName(EntityRoles.ClientRole);
        }

    
    }
}