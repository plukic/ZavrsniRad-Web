using COAssistance.COMMONS.Models.Languages;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace COAssistance.API.Services.LanguageService
{
    public class LanguageService : ILanguageService
    {
        #region Fields

        private readonly COAssistanceDbContext _context;

        #endregion Fields

        #region Constructor

        public LanguageService(COAssistanceDbContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Methods

        public IEnumerable<Language> GetAll(bool trackEntites = true)
        {
            var query = trackEntites ? _context.Language : _context.Language.AsNoTracking();

            return query.ToList();
        }

        public Language GetByCodeName(string langCode)
        {
            var result = _context.Language.FirstOrDefault(x => x.LanguageCode.Equals(langCode));
            if (result == null)
                return GetDefaultLanguage();
            return result;
        }

        public Language GetDefaultLanguage()
        {
            var defaultCode = ConfigurationManager.AppSettings["LanguageDefaultCode"];
            var defaultName = ConfigurationManager.AppSettings["LanguageDefaultName"];

            var result = _context.Language.FirstOrDefault(x => x.LanguageCode.Equals(defaultCode));
            if (result == null)
            {
                Language l = new Language
                {
                    LanguageCode = defaultCode,
                    LanguageName = defaultName
                };
                _context.Language.Add(l);
                _context.SaveChanges();

                return l;
            }
            else
            {
                return result;
            }
        }

        public IEnumerable<LanguageDropDownViewModel> GetDropDownData()
        {
            return _context.Language
                .AsNoTracking()
                .Select(l => new LanguageDropDownViewModel
                {
                    Id = l.Id,
                    Name = l.LanguageName
                })
                .ToList();
        }

        #endregion Methods
    }
}