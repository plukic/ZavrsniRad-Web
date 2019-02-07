using COAssistance.API.Resources;
using COAssistance.API.Services.ClientService;
using COAssistance.API.Services.PasswordRecovery;
using COAssistance.COMMONS.Models.ClientPassword;
using COAssistance.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace COAssistance.API.Controllers
{
    [RoutePrefix("api/user/maintenance")]
    public class UserPasswordMaintenanceController : BaseController
    {
        private IClientService _clientService;
        private IPasswordRecoveryService _passwordRecoveryService;

        public UserPasswordMaintenanceController(IClientService clientService, IPasswordRecoveryService passwordRecoveryService)
        {
            _clientService = clientService;
            _passwordRecoveryService = passwordRecoveryService;
        }
        [Authorize(Roles = EntityRoles.AdminRole)]
        [HttpPut]
        [Route("resetpassword/internal")]
        public async Task<IHttpActionResult> InternalPasswordReset([FromBody] ClientsResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content(HttpStatusCode.BadRequest, GetValidationErrors());
            }

            var client = await _clientService.FindById(model.Id);

            if (client == null)
            {
                return NotFound();
            }

            if (!client.UserLoginData.IsActive)
            {
                ModelState.AddModelError("reset_password", Resource.UserAccountNotActive);
                return Content(HttpStatusCode.BadRequest, GetValidationErrors());
            }

            if (string.IsNullOrEmpty(client.Email))
            {
                ModelState.AddModelError("reset_password", Resource.UserHasNotEnteredEmail);
                return Content(HttpStatusCode.BadRequest, GetValidationErrors());
            }
            try
            {
                await _passwordRecoveryService.ResetPasswordAndSendEmail(client, model.Length, model.SpecialCharacters);
            } catch (Exception e)
            {
                return InternalServerError(e);
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("resetpassword")]
        public async Task<IHttpActionResult> ResetUserPassword([FromBody] ClientRequestPasswordResetModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var client = await _clientService.FindByUsername(model.Username);

            if (client == null)
            {
                ModelState.AddModelError("reset_password","Korisničko ime nije validno.");
                return BadRequest(ModelState);
            }

            if (!client.UserLoginData.IsActive)
            {
                ModelState.AddModelError("reset_password", Resource.UserAccountNotActive);
                return BadRequest(ModelState);
            }

          
            if (string.IsNullOrEmpty(client.UserLoginData.Email))
            {
                ModelState.AddModelError("reset_password", Resource.UserHasNotEnteredEmail);
                return BadRequest(ModelState);
            }

            await _passwordRecoveryService.GenerateAndSendTokenAsync(client);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("resetpassword/confirm")]
        public async Task<IHttpActionResult> ConfirmResetUserPassword([FromBody] ClientConfirmResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var client = await _clientService.FindByUsername(model.Username);
            if (client == null)
            {
                ModelState.AddModelError("reset_password", "Korisničko ime nije validno.");
                return BadRequest(ModelState);
            }

            ClientToken clientToken = _passwordRecoveryService.FindActiveClientToken(client, model.Token);
            if (clientToken == null)
            {
                ModelState.AddModelError("confirm_password_reset", Resource.PasswordResetTokenNotValid);
                return BadRequest(ModelState);
            }

            _passwordRecoveryService.ConfirmPasswordReset(clientToken, model.NewPassword);
            return Ok();
        }
    }
}
