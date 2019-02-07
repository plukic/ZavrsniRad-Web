using COAssistance.API.Resources;
using COAssistance.API.Services.EmailService;
using COAssistance.API.Services.Logger;
using COAssistance.API.Services.RoleService;
using COAssistance.API.Services.UserService;
using COAssistance.COMMONS.Models.Admin;
using COAssistance.COMMONS.Models.Staff;
using COAssistance.DATA.EF;
using COAssistance.DATA.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;

namespace COAssistance.API.Controllers
{
    [Authorize(Roles = EntityRoles.AdminRole)]
    [RoutePrefix("api/admin")]
    public class AdminController : BaseController
    {
        #region Fields

        private ApplicationUserManager _applicationUserManager;
        private COAssistanceDbContext _context;
        private IRoleService _roleService;
        private ILoggerService _loggerService;
        private IEmailService _emailService;
        private IUserService _userService;

        #endregion Fields

        #region Constructor

        public AdminController(
            ApplicationUserManager applicationUserManager,
            IRoleService roleService,
            ILoggerService loggerService,
            COAssistanceDbContext context,
            IEmailService emailService,
            IUserService userService)
        {
            _applicationUserManager = applicationUserManager;
            _roleService = roleService;
            _loggerService = loggerService;
            _context = context;
            _emailService = emailService;
            _userService = userService;
        }

        #endregion Constructor

        #region Methods

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAdminAccounts()
        {
            var adminRole = _roleService.GetAdminRole();

            var result = _context.Admin
                .Select(x => new StaffModel
                {
                    Id = x.AdminId,
                    Email=x.UserLoginData.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    IsActive = x.UserLoginData.IsActive,
                    Username = x.UserLoginData.UserName,
                    LastLoginTime=x.UserLoginData
                    .RefreshTokens
                    .OrderByDescending(y=>y.IssuedUtc)
                    .DefaultIfEmpty()
                    .FirstOrDefault().IssuedUtc
                }).ToList();

            return Ok(result);
        }

        //[HttpGet]
        //[Route("{id}")]
        //public IHttpActionResult GetAdminAccountDetails(string id)
        //{
        //    var adminRole = _roleService.GetAdminRole();
        //    var result = _context
        //         .Admin
        //         .Where(x => (x.AdminId == id))
        //         .Include(x=>x.UserLoginData)
        //         .FirstOrDefault();

        //    if (result == null)
        //        return NotFound();

        //    return Ok(new AdminAccountViewModel
        //    {
        //        Id = result.AdminId,
        //        FirstName = result.FirstName,
        //        LastName = result.LastName,
        //        Username = result.UserLoginData.UserName
        //    });
        //}

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> CreateAdminAccount([FromBody] AdminAccountCreateViewModel adminAccount)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, GetValidationErrors());

            if (_applicationUserManager.FindByName(adminAccount.UserName) != null)
            {
                ModelState.AddModelError("RegisterError", Resources.Resource.UserAccountExists);
            }

            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, GetValidationErrors());

            IdentityRole adminRole = _roleService.GetAdminRole();

            var currentAdminGroup = _userService.GetMaintenanceSettingsId();

            var user = new UserLoginData
            {
                UserName = adminAccount.UserName,
                IsActive = true,
                OriginUsername=adminAccount.UserName
            };

            var admin = new Admin
            {
                FirstName = adminAccount.FirstName,
                LastName = adminAccount.LastName,
                MaintenanceId = currentAdminGroup
            };
            var password = System.Web.Security.Membership.GeneratePassword(10, 4);
            var userCreateResult = _applicationUserManager.Create(user, password);
            var roleCreateResult = _applicationUserManager.AddToRole(user.Id, adminRole.Name);

            admin.AdminId = user.Id;
            _context.Admin.Add(admin);
            _context.SaveChanges();
            _loggerService.Log(UserId, ActionType.Create, $"Admin account created - {user.UserName}");

            try
            {
                await _emailService.Send(adminAccount.Email, "Account created", $"Username: {adminAccount.UserName} Password:{password}");
                return Created("/api/admin/" + user.Id, user.Id);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        [Route("password/reset")]
        public async Task<IHttpActionResult> ResetAdminPassword([FromBody] AdminResetPasswordViewModel adminResetPassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_applicationUserManager.FindById(adminResetPassword.Id) == null)
                return NotFound();

            var user = _context.Users.Where(x => x.Id.Equals(adminResetPassword.Id)).First();
            var password = System.Web.Security.Membership.GeneratePassword(adminResetPassword.Length, adminResetPassword.SpecialCharacters);

            user.PasswordHash = _applicationUserManager.PasswordHasher.HashPassword(password);
            _context.SaveChanges();

            _loggerService.Log(UserId, ActionType.Update, $"Admin password reset - {adminResetPassword.Id}");
            await _emailService.Send(user.Email, "Password changed", $"Username: {user.UserName} Password: {password}");

            return Ok();
        }

        [HttpPut]
        [Route("password/update")]
        public async Task<IHttpActionResult> ChangePassword([FromBody] AdminChangePasswordViewModel adminResetPassword)
        {
            if (!ModelState.IsValid)
                return Content(HttpStatusCode.BadRequest, GetValidationErrors());

            var result = await _applicationUserManager.ChangePasswordAsync(UserId, adminResetPassword.OldPassword, adminResetPassword.NewPassword);

            if (result.Succeeded)
            {
                _loggerService.Log(UserId, ActionType.Update, $"Admin password changed - {UserId}");

                return Ok();
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("password/change")]
        public async Task<IHttpActionResult> ChangeAdminPassword([FromBody] AdminChangePasswordViewModel adminChangePassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (_applicationUserManager.FindById(adminChangePassword.Id) == null)
                return NotFound();

            var result = _applicationUserManager.ChangePassword(adminChangePassword.Id, adminChangePassword.OldPassword, adminChangePassword.NewPassword);

            if (result.Succeeded)
            {
                var user = _context.Users.Where(x => x.Id.Equals(adminChangePassword.Id)).First();
                _loggerService.Log(UserId, ActionType.Update, $"Admin password changed - {adminChangePassword.Id}");

                await _emailService.Send(user.Email, "Password changed", $"Username: {user.UserName} Password: {adminChangePassword.NewPassword}");

                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("status")]
        public IHttpActionResult ChangeAdminStatus([FromBody] AdminStatusChangeViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = _context.Users.Find(model.Id);

            if (user == null)
                return NotFound();

            if (model.IsActive == user.IsActive)
            {
                ModelState.AddModelError(string.Empty, Resource.InvalidStatusChangeParameter);
                return Content(HttpStatusCode.BadRequest, GetValidationErrors());
            }

            user.IsActive = model.IsActive;
            _context.SaveChanges();

            string statusChange = model.IsActive ? "activated" : "deactivated";

            _loggerService.Log(UserId, ActionType.Update, $"Admin account {statusChange} - {user.Id}");

            return Ok();
        }

        #endregion Methods
    }
}