using COAssistance.COMMONS.Models.Paging;
using COAssistance.API.Services.RoleService;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using COAssistance.COMMONS.Models.Clients;

namespace COAssistance.API.Services.UserService
{
    public class UserService : IUserService
    {
        #region Fields

        private ApplicationUserManager _userManager;
        private IRoleService _roleService;
        private COAssistanceDbContext _context;

        #endregion Fields

        #region Constructor

        public UserService(
            ApplicationUserManager userManager,
            COAssistanceDbContext db,
            IRoleService roleService)
        {
            _userManager = userManager;
            _roleService = roleService;
            _context = db;
        }

        #endregion Constructor

        #region Methods

      
        public int GetMaintenanceSettingsId()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            return _context.Admin
                .AsNoTracking()
                .First(m => m.AdminId == userId)
                .MaintenanceId;
        }


        public List<string> GetClientsCardMembership()
        {
            return _context.Clients.Include(x => x.UserLoginData).Select(x => x.UserLoginData.UserName).ToList();
        }

        #endregion Methods
    }
}