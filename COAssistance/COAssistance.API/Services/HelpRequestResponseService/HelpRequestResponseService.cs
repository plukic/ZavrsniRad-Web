using COAssistance.COMMONS.Models.HelpRequestResponse;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model;
using COAssistance.DATA.Model.HelpRequests;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace COAssistance.API.Services.HelpRequestResponseService
{
    public class HelpRequestResponseService : IHelpRequestResponseService
    {
        #region Fields

        private readonly COAssistanceDbContext _context;

        #endregion Fields

        #region Constructor

        public HelpRequestResponseService(COAssistanceDbContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Methods

        public HelpRequestResponse CreateClientHelpRequestResponse(HelpRequestResponseCreateModel vm)
        {
            var helpRequest= _context.HelpRequests
                .Where(x => x.Id == vm.HelpRequestId)
                .FirstOrDefault();

            var oldClientHelpRequest = _context.HelpRequestsResponses.Where(x => x.HelpRequest.ClientId == helpRequest.ClientId).ToList();


            oldClientHelpRequest.ForEach(x => x.IsActive = false);

            var newHelpRequest = new HelpRequestResponse
            {
                HelpIncomingDateTimeUtc = vm.ExpirationDateUtc,
                HelpRequestId = vm.HelpRequestId,
                HelpRequestState = vm.HelpRequestState,
                IsActive = true,
                LanguageId = vm.LanguageId,
                ShortInstruction = vm.ShortInstruction
            };

            _context.HelpRequestsResponses.Add(newHelpRequest);
            _context.SaveChanges();

            return newHelpRequest;
        }

        public HelpRequestResponse GetClientHelpRequestResponse(string userId)
        {

            return _context.HelpRequestsResponses
                .Where(x => x.HelpRequest.ClientId == userId && x.LanguageId == x.HelpRequest.Client.ClientConfigurationGroup.LanguageId)
                .Where(x => x.IsActive && x.HelpIncomingDateTimeUtc > DateTime.UtcNow)
                .FirstOrDefault();
        }

        public HelpRequestResponseModel GetDetails(int httpRequestResponseId)
        {
            return _context.HelpRequestsResponses
                .AsNoTracking()
                .Select(hrr => new HelpRequestResponseModel
                {
                    Id = hrr.Id,
                    HelpIncomingDateTime = hrr.HelpIncomingDateTimeUtc,
                    HelpRequestState = hrr.HelpRequestState,
                    ShortInstructions = hrr.ShortInstruction,
                    IsActive = hrr.IsActive
                })
                .FirstOrDefault(hrr => hrr.Id == httpRequestResponseId);
        }

        public IEnumerable<HelpRequestResponseModel> GetHelpRequestResponses(int helpRequestId)
        {
            return _context.HelpRequestsResponses
                .AsNoTracking()
                .Where(hrr => hrr.HelpRequestId == helpRequestId)
                .Select(hrr => new HelpRequestResponseModel
                {
                    Id = hrr.Id,
                    HelpRequestId = hrr.HelpRequestId,
                    ClientId = hrr.HelpRequest.ClientId,
                    HelpIncomingDateTime = hrr.HelpIncomingDateTimeUtc,
                    HelpRequestState = hrr.HelpRequestState,
                    ShortInstructions = hrr.ShortInstruction,
                    IsActive = hrr.IsActive
                })
                .ToList();
        }

        public void Activate(HelpRequestActivationModel model)
        {
            var responses = _context.HelpRequestsResponses
                .Where(hrr => (hrr.HelpRequestId == model.HelpRequestId) && (hrr.IsActive || hrr.Id == model.HelpRequestResponseId))
                .ToList();

            if (responses == null)
            {
                throw new ArgumentNullException(nameof(responses));
            }

            responses.ForEach(hrr => hrr.IsActive = false);

            responses.FirstOrDefault(hrr => hrr.Id == model.HelpRequestResponseId).IsActive = true;

            _context.SaveChanges();
        }

        public Client GetHelpRequestResponseClient(int helpRequestResponseId)
        {
            var helpRequestResponse = _context.HelpRequestsResponses
                 .Where(hrr => (hrr.Id == helpRequestResponseId))
                 .Include(x => x.HelpRequest.Client)
                 .FirstOrDefault();

            return helpRequestResponse.HelpRequest.Client;
        }

        #endregion Methods
    }
}