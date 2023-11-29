namespace JSandwiches.Models.Users
{
    public class ApplicationUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<string> Roles { get; set; }
    }
}
