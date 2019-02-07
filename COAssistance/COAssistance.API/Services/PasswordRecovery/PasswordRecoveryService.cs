using COAssistance.API.Resources;
using COAssistance.API.Services.EmailService;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using COAssistance.COMMONS.Models.Email;
namespace COAssistance.API.Services.PasswordRecovery
{
    public class PasswordRecoveryService : IPasswordRecoveryService
    {
        #region Fields

        private COAssistanceDbContext context;
        private IEmailService emailService;
        private ApplicationUserManager applicationUserManager;

        #endregion Fields

        #region Constructor

        public PasswordRecoveryService(
            COAssistanceDbContext context,
            IEmailService emailService,
            ApplicationUserManager applicationUserManager)
        {
            this.context = context;
            this.emailService = emailService;
            this.applicationUserManager = applicationUserManager;
        }

        #endregion Constructor

        #region Methods

        public async Task GenerateAndSendTokenAsync(Client client)
        {
            var clientToken =
                context.ClientTokens
                .FirstOrDefault(x => x.ClientId.Equals(client.ClientId) && !x.IsUsed && x.ValidUntil > DateTime.UtcNow);

            if (clientToken == null)
            {
                clientToken = new ClientToken
                {
                    ClientId = client.ClientId,
                    Token = GenerateRandomToken(),
                    ValidUntil = DateTime.UtcNow.AddMinutes(5),
                    IsUsed = false
                };

                context.ClientTokens.Add(clientToken);
                context.SaveChanges();
            }

            var model = new PasswordResetCodeEmailModel
            {
                Code = clientToken.Token,
                FirstName = client.FirstName,
                LastName = client.LastName
            };

            await emailService.SendTemplateEmail(client.Email, Resource.PasswordResetTokenEmailSubject, "ResetPasswordCodeEmail", model);

            //await emailService.Send(client.UserLoginData.Email, , clientToken.Token);
        }

        public ClientToken FindActiveClientToken(Client client, string token)
        {
            return context.ClientTokens
                    .FirstOrDefault(x => x.Token.Equals(token) && !x.IsUsed && x.ClientId.Equals(client.ClientId) && x.ValidUntil > DateTime.UtcNow);
        }

        public void ConfirmPasswordReset(ClientToken token, string newPassword)
        {
            var userToken = context.ClientTokens.Where(x => x.Id == token.Id).Include(x => x.Client.UserLoginData).First();

            userToken.Client.UserLoginData.PasswordHash = applicationUserManager.PasswordHasher.HashPassword(newPassword);
            userToken.IsUsed = true;

            context.SaveChanges();
        }

        private string GenerateRandomToken()
        {
            Random generator = new Random();
            return generator.Next(100000, 999999).ToString("D6");
        }

        public async Task ResetPasswordAndSendEmail(Client client, int passwordLength, int specialCharacters)
        {
            var user = context.Users.Find(client.ClientId);
            var newPassowrd = System.Web.Security.Membership.GeneratePassword(passwordLength, specialCharacters);

            user.PasswordHash = applicationUserManager.PasswordHasher.HashPassword(newPassowrd);
            var newPasswordEmailModel = new NewPasswordEmailModel
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                NewPassword = newPassowrd
            };
            await emailService.SendTemplateEmail(client.Email,
                Resource.NewPassword,
                "NewPasswordEmail",
                newPasswordEmailModel);

            //await emailService.Send(user.Email, Resource.NewPassword, newPassowrd);
            await context.SaveChangesAsync();
        }

        #endregion Methods
    }
}