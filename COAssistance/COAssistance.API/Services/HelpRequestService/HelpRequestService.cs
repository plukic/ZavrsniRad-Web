using COAssistance.COMMONS.Models;
using COAssistance.COMMONS.Models.HelpRequest;
using COAssistance.COMMONS.Models.HelpRequests;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace COAssistance.API.Services.HelpRequestService
{
    public class HelpRequestService : IHelpRequestService
    {
        #region Fields

        private COAssistanceDbContext _context;

        #endregion Fields

        #region Constructor

        public HelpRequestService(COAssistanceDbContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Methods

        public HelpRequestsDetailsModel AddNewRequest(HelpRequestCreateModel model, string clientId)
        {
            var m = new HelpRequest
            {
                IsSolved = false,
                ClientId = clientId,
                HelpRequestCategory = model.HelpRequestCategory,
                RequestDate = DateTime.UtcNow,
                Longitude = model.Longitude,
                Langitude = model.Latitude
            };
            _context.HelpRequests.Add(m);

            _context.SaveChanges();

            return GetById(m.Id);
        }

        public void Complete(int helpRequestId)
        {
            var helpRequest = _context.HelpRequests.Find(helpRequestId);

            if (helpRequest == null)
            {
                throw new ArgumentNullException(nameof(helpRequest));
            }

            helpRequest.IsSolved = true;

            _context.SaveChanges();
        }

        public IQueryable<HelpRequest> GetAll(HelpRequestCategory helpRequestCategory)
        {
            return _context.HelpRequests
                .AsNoTracking()
                .Include(hp => hp.Client)
                .Where(hp => (helpRequestCategory == 0 || hp.HelpRequestCategory == helpRequestCategory));
        }

        public HelpRequestsDetailsModel GetById(int helpRequestId)
        {
            return _context.HelpRequests
                .AsNoTracking()
                .Select(hp => new HelpRequestsDetailsModel
                {
                    Id = hp.Id,
                    Client = hp.Client.FirstName + " " + hp.Client.LastName,
                    HelpRequestCategory = hp.HelpRequestCategory,
                    IsSolved = hp.IsSolved,
                    Latitude = hp.Langitude,
                    Longitude = hp.Longitude,
                    RequestDate = hp.RequestDate,
                    ClientId = hp.ClientId,
                    ClientsLanguage = hp.Client.ClientConfigurationGroup.Language.LanguageName
                })
                .FirstOrDefault(hp => hp.Id == helpRequestId);
        }

        public IList<HelpRequestsModel> GetLastRecords(int size)
        {
            return _context.HelpRequests
                  .AsNoTracking()
                  .OrderByDescending(x => x.RequestDate)
                  .Take(size)
                  .Select(hp => new HelpRequestsModel
                  {
                      Id = hp.Id,
                      Client = hp.Client.FirstName + " " + hp.Client.LastName,
                      HelpRequestCategory = hp.HelpRequestCategory,
                      IsSolved = hp.IsSolved,
                      RequestDate = hp.RequestDate,
                      ClientId = hp.ClientId
                  })
                  .ToList();
        }

        public IList<HelpRequestsModel> GetUnfinished()
        {
            return _context.HelpRequests
                 .Where(x => !x.IsSolved)
                   .Select(hp => new HelpRequestsModel
                   {
                       Id = hp.Id,
                       Client = hp.Client.FirstName + " " + hp.Client.LastName,
                       HelpRequestCategory = hp.HelpRequestCategory,
                       IsSolved = hp.IsSolved,
                       RequestDate = hp.RequestDate,
                       ClientId = hp.ClientId
                   })
                   .OrderByDescending(x => x.RequestDate)
                   .ToList();
        }

        #endregion Methods
    }
}