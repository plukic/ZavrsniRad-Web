using COAssistance.COMMONS.Models;
using System.Collections.Generic;

namespace COAssistance.API.Services.PredefinedTextTemplates
{
    public interface IPredefinedTextTemplateService
    {
        #region Methods

        IList<DropDownModel> GetDropDownData(int maintenanceId);

        #endregion Methods
    }
}