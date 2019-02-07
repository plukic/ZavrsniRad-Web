using COAssistance.DATA.Model;
using System.Threading.Tasks;

namespace COAssistance.API.Services.PasswordRecovery
{
    public interface IPasswordRecoveryService
    {
        #region Methods

        Task GenerateAndSendTokenAsync(Client client);

        void ConfirmPasswordReset(ClientToken token, string newPassword);

        ClientToken FindActiveClientToken(Client client, string token);

        Task ResetPasswordAndSendEmail(Client client, int passwordLength, int specialCharacters);

        #endregion Methods
    }
}