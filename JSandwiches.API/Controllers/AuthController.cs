using JSandwiches.Models.Data;
using JSandwiches.Models.DTO.UsersDTO;
using JSandwiches.Models.Users;
using JSandwiches.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JSandwiches.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;

        public AuthController(IAuthService authService, UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _authService = authService;
            _userManager = userManager;
            _db = db;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(ApplicationUser user)
        {

            if (await _authService.Register(user))
            {
                await _authService.AssignRoles(user.UserName, user.Roles);

                return Ok(new { status = "success", message = "registration succesful" });
            }
            else
            {
                return Ok(new { status = "fail", message = "registration failed" });
            }
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginAppUserDTO user)
        {
            var result = await _authService.LoginUser(user);
            var lstRoles = GetRoles(user);


            if (result != false)
            {
                var token = _authService.GenerateToken(user);
                return Ok(new { status = "success", message = "login succesful", data = token, roles = lstRoles});
            }
            return Ok(new { status = "fail", message = "login failed" });

        }

        [HttpPost]
        public List<string> GetRoles(LoginAppUserDTO user)
        {
            var lstRole = new List<string>();
            var lUser =  _userManager.FindByEmailAsync(user.UserName).Result;
            if (lUser != null)
                lstRole = ((_userManager.GetRolesAsync(lUser))).Result.ToList();
            return lstRole;
        }

    }
}
