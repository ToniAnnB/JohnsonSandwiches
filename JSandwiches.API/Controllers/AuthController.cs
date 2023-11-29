﻿using JSandwiches.Models;
using JSandwiches.Models.Data;
using JSandwiches.Models.Users;
using JSandwiches.Models.UsersDTO;
using JSandwiches.Services;
using Microsoft.AspNetCore.Http;
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

            //var identityUser = await _userManager.FindByEmailAsync(user.UserName);


            if (result != false)
            {
                var token = _authService.GenerateToken(user);

                return Ok(new { status = "success", message = "login succesful", data = token});
            }
            return Ok(new { status = "fail", message = "login failed" });

        }

    }
}