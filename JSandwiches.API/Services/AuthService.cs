using JSandwiches.Models;
using JSandwiches.Models.Data;
using JSandwiches.Models.Users;
using JSandwiches.Models.UsersDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JSandwiches.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _db;
        private readonly ILogger<AuthService> _logger;

        public AuthService(UserManager<IdentityUser> userManager, IConfiguration config, ApplicationDbContext db,
            ILogger<AuthService> logger, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
            _db = db;
            _logger = logger;
        }

        public async Task<bool> Register(ApplicationUser user)
        {
            _logger.LogInformation($"Resgistration attempt for {user.UserName}");

            try
            {
                var identityUser = new IdentityUser()
                {
                    UserName = user.UserName,
                    Email = user.UserName,
                };

                var result = await _userManager.CreateAsync(identityUser, user.Password);

                return result.Succeeded;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in {nameof(Register)}");
                return false;
            }

        }

        public async Task<bool> LoginUser(LoginAppUserDTO user)
        {
            _logger.LogInformation($"Resgistration attempt for {user.UserName}");

            try
            {
                var identityUser = await _userManager.FindByEmailAsync(user.UserName);

                if (identityUser == null)
                {
                    return false;
                }
                else
                {


                    if (await _userManager.CheckPasswordAsync(identityUser, user.Password))
                    {
                        return true;
                    }
                    return false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in {nameof(LoginUser)}");
                return false;
            }
        }

        public async Task<bool> AssignRoles(string userName, IEnumerable<string> roles)
        {
            _logger.LogInformation($"Role assignment attempt for {userName}");

            try
            {
                var user = await _userManager.FindByNameAsync(userName);
                if (user == null)
                {
                    return false;
                }

                var result = await _userManager.AddToRolesAsync(user, roles);
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in {nameof(AssignRoles)}");
                return false;
            }
        }

        public async Task<string> GenerateToken(LoginAppUserDTO user)
        {
            _logger.LogInformation($"Generation of token attempt for {user.UserName}");

            try
            {
                var identityUser = await _userManager.FindByEmailAsync(user.UserName);
                if (identityUser == null)
                {
                    return null;
                }

                var userRoles = await _userManager.GetRolesAsync(identityUser);

                var claims = new List<Claim>
            {
                new Claim (ClaimTypes.Email, user.UserName),
            };

                if (userRoles.Any())
                {
                    claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
                }

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWTConfig:Key").Value));
                var signInCreds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

                var securityToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    issuer: _config.GetSection("JWTConfig:Issuer").Value,
                    audience: _config.GetSection("JWTConfig:Audience").Value,
                    signingCredentials: signInCreds
                    );
                string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
                return token;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in {nameof(GenerateToken)}");
                return null;
            }
        }

    }
}
