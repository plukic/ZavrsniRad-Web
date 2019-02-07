using COAssistance.DATA.EF;
using COAssistance.DATA.Model.Clients;
using System;
using System.Collections.Generic;
using System.Linq;

namespace COAssistance.API.Services.ClientEmergencyNumbers
{
    public class ClientEmergencyNumberService : IClientEmergencyNumberService
    {
        #region Fields

        private readonly COAssistanceDbContext _context;

        #endregion Fields

        #region Constructor

        public ClientEmergencyNumberService(COAssistanceDbContext context)
        {
            _context = context;
        }

        #endregion Constructor

        #region Methods

        public void Create(EmergencyContactNumbers emergencyContactNumbers)
        {
            _context.EmergencyContactNumbers.Add(emergencyContactNumbers);
            _context.SaveChanges();
        }

        public void DeleteClientEmergencyNumber(int id)
        {
            var numbers = _context.EmergencyContactNumbers.Where(x => x.Id == id).ToList();
            _context.EmergencyContactNumbers.RemoveRange(numbers);
            _context.SaveChanges();
        }

        public void Edit(EmergencyContactNumbers emergencyContactNumbers)
        {
            var number = _context.EmergencyContactNumbers.Where(x => x.Id == emergencyContactNumbers.Id).FirstOrDefault();
            if (number == null)
                return;

            number.PhoneNumber = emergencyContactNumbers.PhoneNumber;
            number.ContactFullName= emergencyContactNumbers.ContactFullName;
            _context.SaveChanges();
        }

        public IEnumerable<EmergencyContactNumbers> GetClientNumbers(string clientId)
        {
            return _context.EmergencyContactNumbers
                .AsNoTracking()
                .Where(ecn => ecn.ClientId == clientId)
                .OrderByDescending(x=>x.ContactFullName)
                .ToList();
        }

        #endregion Methods
    }
}