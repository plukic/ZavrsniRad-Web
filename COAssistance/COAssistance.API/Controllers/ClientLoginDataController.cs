using COAssistance.API.Services.TokenService;
using COAssistance.DATA.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace COAssistance.API.Controllers
{
    [Authorize(Roles =EntityRoles.AdminRole )]
        [RoutePrefix("api/clients/logindata")]
    public class ClientLoginDataController : BaseController
    {

        private readonly ITokenService _tokenService;

        public ClientLoginDataController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpGet]
        [Route("{clientId}")]
        public IHttpActionResult LoginData(string clientId)
        {
            var clientLoginData = _tokenService.GetLoginDataById(clientId);
            return Ok(clientLoginData);
        }


        [HttpPut]
        [Route("deactivate")]
        public IHttpActionResult Deactivate([FromBody]string id)
        {
            _tokenService.RemoveToken(id);
            return Ok();
        }
    }
}
