using JSandwiches.Models.Users;
using JSandwiches.Models.UsersDTO;

namespace JSandwiches.Services
{
    public interface IAuthService
    {
        Task<bool> Register(ApplicationUser user);
        Task<bool> LoginUser(LoginAppUserDTO user);
        Task<string> GenerateToken(LoginAppUserDTO user);
        Task<bool> AssignRoles(string userName, IEnumerable<string> roles);
    }
}
