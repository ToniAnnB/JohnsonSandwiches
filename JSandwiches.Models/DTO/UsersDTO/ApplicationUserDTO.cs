using System.ComponentModel.DataAnnotations;

namespace JSandwiches.Models.UsersDTO
{
    public class ApplicationUserDTO : LoginAppUserDTO
    {

        public List<string> Roles { get; set; }
    }

    public class LoginAppUserDTO
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
