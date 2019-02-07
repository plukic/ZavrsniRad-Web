using COAssistance.COMMONS.Models.Maintenance;
using COAssistance.API.Services.UserService;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace COAssistance.API.Services.MaintenanceService
{
    public class MaintenanceService : IMaintenanceService
    {
        #region Fields

        private COAssistanceDbContext _context;
        private IUserService _userService;

        #endregion Fields

        #region Constructor

        public MaintenanceService(COAssistanceDbContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        #endregion Constructor

        #region Methods

        public void Create(Maintenance maintenance)
        {
            if (maintenance == null)
            {
                throw new ArgumentNullException(nameof(maintenance));
            }

            _context.Maintenances.Add(maintenance);
            _context.SaveChanges();
        }

        public void Edit(MaintenanceEditModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var maintenance = this.GetById(model.MaintenanceId);

            if (maintenance == null)
            {
                throw new ArgumentNullException(nameof(maintenance));
            }

            maintenance.PasswordLength = model.PasswordLength;
            maintenance.PasswordSpecialCharacters = model.PasswordSpecialCharacters;

            _context.SaveChanges();
        }

        public Maintenance GetById(int maintenanceId)
        {
            return _context.Maintenances.Find(maintenanceId);
        }

        public IEnumerable<MaintenanceModel> GetAll()
        {
            return _context.Maintenances
                .AsNoTracking()
                .Select(m => new MaintenanceModel
                {
                    MaintenanceId = m.MaintenanceId,
                    PasswordLength = m.PasswordLength,
                    PasswordSpecialCharacters = m.PasswordSpecialCharacters,
                })
                .ToList();
        }

        public Maintenance GetAdminSettings()
        {
            var usersMaintenanceId = _userService.GetMaintenanceSettingsId();

            return _context.Maintenances
                .AsNoTracking()
                .FirstOrDefault(m => m.MaintenanceId == usersMaintenanceId);
        }

        #endregion Methods
    }
}