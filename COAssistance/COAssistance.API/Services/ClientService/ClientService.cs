using COAssistance.API.Services.ClientConfiguration;
using COAssistance.API.Services.EmailService;
using COAssistance.API.Services.RoleService;
using COAssistance.COMMONS.Models.Clients;
using COAssistance.COMMONS.Models.Email;
using COAssistance.COMMONS.Models.FirebaseTokens;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model;
using COAssistance.DATA.Model.Clients;
using COAssistance.DATA.Model.FirebaseTokens;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace COAssistance.API.Services.ClientService
{
    public class ClientService : IClientService
    {
        #region Fields

        private readonly COAssistanceDbContext _context;
        private readonly ClientConfigurationService _clientConfigurationService;
        private readonly IRoleService _roleService;
        private readonly ApplicationUserManager _userManager;
        private readonly IEmailService _emailService;

        #endregion Fields

        #region Constructor

        public ClientService(
            COAssistanceDbContext context,
            ApplicationUserManager userManager,
            IRoleService roleService,
            ClientConfigurationService clientConfigurationService,
            IEmailService emailService)
        {
            _context = context;
            _userManager = userManager;
            _roleService = roleService;
            _clientConfigurationService = clientConfigurationService;
            _emailService = emailService;
        }

        #endregion Constructor

        #region Methods

        public async Task<bool> Create(ClientCreateModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var user = new UserLoginData
            {
                UserName = model.CardNumber.Replace(" ", string.Empty),
                Email = model.Email,
                IsActive = true,
                OriginUsername = model.CardNumber
            };

            var password = System.Web.Security.Membership.GeneratePassword(8, 2);
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user.Id, _roleService.GetClientRole().Name);

                var configuration = _clientConfigurationService.GetClientConfiguration(model.ConfigurationGroup);

                var client = new Client
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Address = model.Address,
                    City = model.City,
                    ClientId = user.Id,
                    ClientConfigurationGroupId = configuration.Id
                };

             

                _context.Clients.Add(client);

                await _context.SaveChangesAsync();
                await _emailService.SendTemplateEmail(model.Email, "Registracija", "RegistrationEmail", new UserRegisterEmailModel
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = password,
                    UserName = user.UserName
                });

                return true;
            }
            return false;
        }

        public async Task<Client> FindById(string clientId)
        {
            return await _context
                .Clients
                .Include(x => x.UserLoginData)
                .Include(x => x.ClientConfigurationGroup)
                .Include(x => x.ClientConfigurationGroup.Language)
                .FirstOrDefaultAsync(c => c.ClientId == clientId);
        }

        public async Task<Client> FindByUsername(string username)
        {
            return await _context.Clients
                .Include(u => u.UserLoginData)
                .FirstOrDefaultAsync(u => u.UserLoginData.UserName.Equals(username));
        }

        public IEnumerable<Client> Get(int size = 0)
        {
            var query = _context.Clients
              .AsNoTracking()
              .Include(c => c.UserLoginData);

            return query;
        }

        public IQueryable<Client> GetClients(string searchParameter)
        {
            return _context.Clients
              .AsNoTracking()
              .Include(c => c.UserLoginData)
              .Where(c => string.IsNullOrEmpty(searchParameter) ||
                    c.UserLoginData.UserName.Contains(searchParameter) ||
                    c.FirstName.Contains(searchParameter) ||
                    c.LastName.Contains(searchParameter));
        }

   
       
        public bool ChangeClientStatus(string clientId)
        {
            var userLoginData = _context.Users.Find(clientId);

            if (userLoginData == null)
                return false;

            userLoginData.IsActive = !userLoginData.IsActive;

            _context.SaveChanges();
            return true;
        }

        public void AddFirebaseToken(FirebaseTokenCreateModel vm, string userId)
        {
            var oldTokens = _context.FirebaseTokens
                .Where(x => x.IsActive)
                .Where(x => x.ClientId == userId)
                .Where(x => x.UniqueMobileId == vm.UniqueMobileId)
                .ToList();

            oldTokens.ForEach(x => x.IsActive = false);

            _context.FirebaseTokens.Add(new FirebaseToken
            {
                ClientId = userId,
                IsActive = true,
                Token = vm.Token,
                UniqueMobileId = vm.UniqueMobileId
            });

            _context.SaveChanges();
        }

        public void Edit(Client client)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            _context.SaveChanges();
        }

        public bool UpdateUserProfile(ClientAccountUpdateModel model, string userId)
        {
            var client = _context.Clients.Where(x => x.ClientId == userId).FirstOrDefault();
            if (client == null)
                return false;

            client.FirstName= string.IsNullOrEmpty(model.FirstName)?client.FirstName: model.FirstName; 
            client.LastName = string.IsNullOrEmpty(model.LastName) ? client.LastName : model.LastName;
            client.PhoneNumber = string.IsNullOrEmpty(model.PhoneNumber) ? client.PhoneNumber : model.PhoneNumber;
            client.Email = string.IsNullOrEmpty(model.Email) ? client.Email : model.Email;
            client.Address = string.IsNullOrEmpty(model.Address) ? client.Address : model.Address;
            client.City = string.IsNullOrEmpty(model.City) ? client.City : model.City;

            client.ChronicDiseases = string.IsNullOrEmpty(model.ChronicDiseases) ? client.ChronicDiseases : model.ChronicDiseases;
            client.Diagnose = string.IsNullOrEmpty(model.Diagnose) ? client.Diagnose : model.Diagnose;
            client.HistoryOfCriticalIllness = string.IsNullOrEmpty(model.HistoryOfCriticalIllness) ? client.HistoryOfCriticalIllness : model.HistoryOfCriticalIllness;

            client.BloodType = model.BloodType.HasValue? model.BloodType: client.BloodType;

            _context.SaveChanges();
            return true;
        }

        public async Task<bool> Create(MobileClientCreateModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var user = new UserLoginData
            {
                UserName = model.CardNumber.Replace(" ", string.Empty),
                Email = model.Email,
                IsActive = true,
                OriginUsername = model.CardNumber
            };

          
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user.Id, _roleService.GetClientRole().Name);

                var configuration = _clientConfigurationService.GetClientConfiguration(model.ConfigurationGroup);

                var client = new Client
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Address = model.Address,
                    City = model.City,
                    ClientId = user.Id,
                    ClientConfigurationGroupId = configuration.Id
                };

                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }

    #endregion Methods
}