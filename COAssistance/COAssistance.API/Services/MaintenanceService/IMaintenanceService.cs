using COAssistance.COMMONS.Models.Maintenance;

using COAssistance.DATA.Model;
using System.Collections.Generic;

namespace COAssistance.API.Services.MaintenanceService
{
    public interface IMaintenanceService
    {
        #region Methods

        void Create(Maintenance maintenance);

        IEnumerable<MaintenanceModel> GetAll();

        Maintenance GetById(int maintenanceId);

        Maintenance GetAdminSettings();

        void Edit(MaintenanceEditModel model);

        #endregion Methods
    }
}