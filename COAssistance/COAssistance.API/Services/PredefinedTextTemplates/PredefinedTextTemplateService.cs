using COAssistance.COMMONS.Models;
using COAssistance.DATA.EF;
using System.Collections.Generic;
using System.Linq;

namespace COAssistance.API.Services.PredefinedTextTemplates
{
    public class PredefinedTextTemplateService : IPredefinedTextTemplateService
    {
        #region Fields

        private readonly COAssistanceDbContext _context;

        #endregion Fields

        #region Constructor

        public PredefinedTextTemplateService(COAssistanceDbContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Methods

        public IList<DropDownModel> GetDropDownData(int maintenanceId)
            => _context.PredefinedTextTemplates
                .AsNoTracking()
                .Where(ptt => ptt.MaintenanceId == maintenanceId)
                .Select(ptt => new DropDownModel
                {
                    Text = ptt.Value,
                    Value = ptt.Key
                }).ToList();

        #endregion Methods
    }
}